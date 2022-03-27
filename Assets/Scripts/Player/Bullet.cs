using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Player
{
	public class Bullet : MonoBehaviour
	{
		#region Serialized Private Fields

		[SerializeField] private float maxDistance = 30;

		#endregion

		#region Private Static Fields

		private static  Stack<GameObject> _bullets;

		#endregion

		#region Private Fields

		private Rigidbody _rigidbody;

		#endregion

		#region Function Events

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		private void OnCollisionEnter(Collision collision)
		{
			collision.gameObject.GetComponentInParent<Enemy>()?.TakeHit(true);
			GameManager.SetExplosion(transform.position);
			gameObject.SetActive(false);
		}

		private void Update()
		{
			if (transform.position.magnitude > maxDistance)
				gameObject.SetActive(false);
		}

		private void OnDisable()
		{
			_rigidbody.velocity = Vector3.zero;
			_bullets?.Push(gameObject);
		}

		#endregion

		#region Public Methods

		public static void SetBulletStack(Stack<GameObject> stack)
		{
			_bullets = stack;
		}

		#endregion
	}
}