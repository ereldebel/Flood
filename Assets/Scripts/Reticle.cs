using Player;
using UnityEngine;

public class Reticle : MonoBehaviour
{
	[SerializeField] private Gun gun;
	private RectTransform _rectTransform;

	private void Awake()
	{
		gun.ReticleChangedPosition += ChangePosition;
		_rectTransform = GetComponent<RectTransform>();
	}

	private void ChangePosition(Vector3 newPos)
	{
		transform.position = newPos;
	}

	private static Vector3 WorldToScreenSpace(Vector3 worldPos, Camera cam, RectTransform area)
	{
		Vector3 screenPoint = cam.WorldToScreenPoint(worldPos);
		screenPoint.z = 0;

		Vector2 screenPos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(area, screenPoint, cam, out screenPos))
		{
			return screenPos;
		}

		return screenPoint;
	}
}