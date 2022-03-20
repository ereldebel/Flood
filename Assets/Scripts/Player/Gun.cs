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
		[SerializeField] private float shotVelocity = 10;
		[SerializeField] private float lowestAngle = 90;
		[SerializeField] private float highestAngle = 57;

		#endregion

		#region Private Fields

		private Transform _transform;
		private float _gunTip;
		private readonly Stack<GameObject> _bullets = new Stack<GameObject>();

		#endregion

		#region Function Events

		private void Awake()
		{
			_transform = transform;
			_gunTip = _transform.localScale.y / 2;
			Bullet.SetStack(_bullets);
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

		public void ChangeAngleOfElevation(float changeAngle)
		{
			changeAngle /= 2;
			print(_transform.rotation);
			// var xRotation = 2 * Mathf.Acos(_transform.rotation.x);
			var xRotation = _transform.localEulerAngles.x;
			if (xRotation + changeAngle > lowestAngle)
				// change = xRotation - lowestAngle;
				return;
			else if (xRotation + changeAngle < highestAngle)
				// change = highestAngle - xRotation;
				return;
			transform.Rotate(Vector3.right, changeAngle);
		}

		#endregion

		#region Private Methods

		private void Shoot(Vector3 shootingPosition, Vector3 shootingDirection)
		{
			GameObject bullet;
			try
			{
				bullet = _bullets.Pop();
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