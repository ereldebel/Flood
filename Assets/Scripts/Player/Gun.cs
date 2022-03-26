using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
	public class Gun : MonoBehaviour
	{
		#region Serialized Private Fields

		[SerializeField] private GameObject bulletPrefab;
		[SerializeField] private float shotVelocity = 10;
		[SerializeField] private float lowestAngle = 90;
		[SerializeField] private float highestAngle = 57;
		[SerializeField] private float shotVariance = 0.1f;
		[SerializeField] private Animator[] animators;

		#endregion

		#region Private Fields

		private Transform _transform;
		private float _gunTip;
		private Stack<GameObject> _bullets = new Stack<GameObject>();
		private Vector3 _rotation;
		private static readonly int ShootAnimation = Animator.StringToHash("Shoot");

		#endregion

		#region Function Events

		private void Awake()
		{
			_transform = transform;
			_gunTip = _transform.localScale.y / 2;
			Bullet.SetBulletStack(_bullets);
			_rotation = _transform.localEulerAngles;
		}

		private void Update()
		{
			if (!Input.GetMouseButtonDown(0)) return;
			var shootingDirection = _transform.up;
			var shootingPosition = _transform.position + shootingDirection * _gunTip;
			ProjectBullet(shootingPosition, shootingDirection, _transform.right, transform.forward);
			foreach (var animator in animators)
				animator.SetTrigger(ShootAnimation);
			AudioManager.GunShot();
		}

		#endregion

		#region Public Methods

		public void ChangeAngleOfElevation(float change)
		{
			if (_rotation.x + change > lowestAngle)
				_rotation.x = lowestAngle;
			else if (_rotation.x + change < highestAngle)
				_rotation.x = highestAngle;
			else
				_rotation.x += change;
			transform.localEulerAngles = _rotation;
		}

		#endregion

		#region Private Methods

		private void ProjectBullet(Vector3 shootingPosition, Vector3 shootingDirection, Vector3 right, Vector3 down)
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
			var shootingVelocityVectorWithNoise =
				shotVelocity * shootingDirection + right * RandomGaussian(shotVariance) +
				down * RandomGaussian(shotVariance);
			bullet.GetComponent<Rigidbody>().AddForce(shootingVelocityVectorWithNoise, ForceMode.Impulse);
		}

		private static float RandomGaussian(float variance)
		{
			float uniform1 = Random.value, uniform2 = Random.value;
			var randStandardNormal = Mathf.Sqrt(-2f * Mathf.Log(uniform1)) * Mathf.Sin(2f * Mathf.PI * uniform2);
			return randStandardNormal * variance;
		}

		#endregion
	}
}