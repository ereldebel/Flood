using UnityEngine;

namespace Player
{
	public class PlayerController : MonoBehaviour
	{
		#region Nested Classes

		private class CameraState
		{
			public float yaw;

			public void SetFromTransform(Transform t)
			{
				yaw = t.eulerAngles.y;
			}

			public void LerpTowards(CameraState target, float rotationLerpPct)
			{
				yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
			}

			public void UpdateTransform(Transform t)
			{
				t.eulerAngles = yaw * Vector3.up;
			}
		}

		#endregion

		#region Serialized Private Fields

		[Header("Rotation Settings")] [Tooltip("Multiplier for the sensitivity of the rotation.")] [SerializeField]
		private float mouseSensitivity = 0.6f;

		[Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")] [SerializeField]
		private AnimationCurve mouseSensitivityCurve =
			new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

		[Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
		[SerializeField]
		private float rotationLerpTime = 0.01f;

		[Tooltip("Whether or not to invert our Y axis for mouse input to rotation.")] [SerializeField]
		private bool invertY = false;

		[SerializeField] private Gun gun;

		#endregion

		#region Private Fields

		private readonly CameraState _targetCameraState = new CameraState();
		private readonly CameraState _interpolatingCameraState = new CameraState();

		#endregion

		#region Function Events

		private void Awake()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		private void OnEnable()
		{
			_targetCameraState.SetFromTransform(transform);
			_interpolatingCameraState.SetFromTransform(transform);
		}

		private void Update()
		{
			var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity;
			if (invertY)
				mouseMovement.y = -mouseMovement.y;

			var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

			_targetCameraState.yaw += mouseMovement.x * mouseSensitivityFactor;
			gun.ChangeAngleOfElevation(mouseMovement.y * mouseSensitivityFactor);

			var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
			_interpolatingCameraState.LerpTowards(_targetCameraState, rotationLerpPct);

			_interpolatingCameraState.UpdateTransform(transform);
		}

		#endregion
	}
}