using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Player
{
	public class Gun : MonoBehaviour
	{
		#region Serialized Private Fields

		[SerializeField] private GameObject bulletPrefab;
		[SerializeField] private float shotVelocity = 10, minAngle = 90, maxAngle = 57;

		#endregion

		#region Private Fields

		private Transform _transform;
		private float _gunTip;
		private Stack<GameObject> bullets = new Stack<GameObject>();

		#endregion

		#region Function Events

		private void Awake()
		{
			_transform = transform;
			_gunTip = _transform.localScale.y / 2;
			Bullet.SetStack(bullets);
			// if (Physics.Raycast(transform.position, shootingDirection, out var hit, 100, LayerMask.GetMask("Reticle Projection")))
			// 	hit.point;
		}

		private void Update()
		{
			if (!Input.GetMouseButtonDown(0)) return;
			var shootingDirection = _transform.up;
			var shootingPosition = _transform.position + shootingDirection * _gunTip;
			Shoot(shootingPosition, shootingDirection);
		}

		#endregion

		#region Public Methods

		public void ChangeAngleOfElevation(float change)
		{
			var rotation = _transform.rotation.eulerAngles;
			rotation.x = Mathf.Max(Mathf.Min(rotation.x + change, minAngle), maxAngle);
			transform.rotation = Quaternion.Euler(rotation);
		}

		#endregion

		#region Private Methods

		private void Shoot(Vector3 shootingPosition, Vector3 shootingDirection)
		{
			GameObject bullet;
			try
			{
				bullet = bullets.Pop();
				bullet.SetActive(true);
			}
			catch (InvalidOperationException)
			{
				bullet = Instantiate(bulletPrefab, shootingPosition, quaternion.identity);
			}

			bullet.transform.position = shootingPosition;
			bullet.GetComponent<Rigidbody>().AddForce(shotVelocity * shootingDirection, ForceMode.Impulse);
		}

		#endregion
	}
}