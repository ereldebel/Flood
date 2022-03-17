using Player;
using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
	private class CameraState
	{
		public float yaw;
		public float pitch;
		private float _roll;
		private float _x;
		private float _y;
		private float _z;

		public void SetFromTransform(Transform t)
		{
			var eulerAngles = t.eulerAngles;
			var position = t.position;
			pitch = eulerAngles.x;
			yaw = eulerAngles.y;
			_roll = eulerAngles.z;
			_x = position.x;
			_y = position.y;
			_z = position.z;
		}

		public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
		{
			yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
			pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
			_roll = Mathf.Lerp(_roll, target._roll, rotationLerpPct);

			_x = Mathf.Lerp(_x, target._x, positionLerpPct);
			_y = Mathf.Lerp(_y, target._y, positionLerpPct);
			_z = Mathf.Lerp(_z, target._z, positionLerpPct);
		}

		public void UpdateTransform(Transform t)
		{
			t.eulerAngles = new Vector3(pitch, yaw, _roll);
			t.position = new Vector3(_x, _y, _z);
		}
	}

	private const float KMouseSensitivityMultiplier = 0.01f;

	readonly CameraState m_TargetCameraState = new CameraState();
	readonly CameraState m_InterpolatingCameraState = new CameraState();

	[Tooltip("Time it takes to interpolate camera position 99% of the way to the target."), Range(0.001f, 1f)]
	public float positionLerpTime = 0.2f;

	[Header("Rotation Settings")] [Tooltip("Multiplier for the sensitivity of the rotation.")]
	public float mouseSensitivity = 60.0f;

	[Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
	public AnimationCurve mouseSensitivityCurve =
		new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

	[Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
	public float rotationLerpTime = 0.01f;

	[Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")]
	public bool invertY = false;

	[SerializeField] private Gun gun;

	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void OnEnable()
	{
		m_TargetCameraState.SetFromTransform(transform);
		m_InterpolatingCameraState.SetFromTransform(transform);
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		}

		var mouseMovement = GetInputLookRotation() * KMouseSensitivityMultiplier * mouseSensitivity;
		if (invertY)
			mouseMovement.y = -mouseMovement.y;

		var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

		m_TargetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor;
		gun.AngleOfElevation(mouseMovement.y * mouseSensitivityFactor);

		// Framerate-independent interpolation
		// Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
		var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * Time.deltaTime);
		var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
		m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct, rotationLerpPct);

		m_InterpolatingCameraState.UpdateTransform(transform);
	}


	private static Vector2 GetInputLookRotation()
	{
		return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
	}
}