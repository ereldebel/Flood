using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class Bullet : MonoBehaviour
	{
		#region Private Static Fields

		private const float MaxDistance = 20;
		private static Stack<GameObject> _bullets;

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
			collision.gameObject.GetComponent<IHittable>()?.TakeHit();
			gameObject.SetActive(false);
		}

		private void Update()
		{
			if (transform.position.magnitude > MaxDistance)
				gameObject.SetActive(false);
		}

		private void OnDisable()
		{
			_rigidbody.velocity = Vector3.zero;
			_bullets.Push(gameObject);
		}

		#endregion

		#region Public Methods

		public static void SetStack(Stack<GameObject> stack)
		{
			_bullets = stack;
		}

		#endregion
	}
}