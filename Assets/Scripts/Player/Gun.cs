using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
	public class Gun : MonoBehaviour
	{
		[SerializeField] private GameObject bullet;
		[SerializeField] private float shotVelocity = 10;

		private float _gunTip;
		private Vector3 reticlePosition;

		public void AngleOfElevation(float change)
		{
			var rotation = transform.rotation.eulerAngles;
			rotation.x = Mathf.Max(Mathf.Min(rotation.x + change, 90), 57);
			transform.rotation = Quaternion.Euler(rotation);
		}

		public event Action<Vector3> ReticleChangedPosition;

		private void Awake()
		{
			_gunTip = transform.localScale.y / 2;
		}

		private void Update()
		{
			var shootingDirection = transform.up;
			if (Input.GetMouseButtonDown(0))
			{
				bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
				bullet.transform.position = transform.position + shootingDirection * _gunTip;
				bullet.GetComponent<Rigidbody>().AddForce(shotVelocity * shootingDirection, ForceMode.Impulse);
			}

			ReticleChangedPosition?.Invoke(transform.position + 20 * shootingDirection);
			// if (Physics.Raycast(transform.position, shootingDirection, out var hit, 100, LayerMask.GetMask("Reticle Projection")))
			// {
			// 	if (reticlePosition != hit.point)
			// 	{
			// 		reticlePosition = hit.point;
			// 		ReticleChangedPosition?.Invoke(reticlePosition, hit.normal);
			// 	}
			// }
		}
	}
}