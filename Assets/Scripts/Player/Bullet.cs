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

		private static readonly Stack<GameObject> Bullets = new Stack<GameObject>();

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
			Bullets.Push(gameObject);
		}

		#endregion

		#region Public Methods

		public static Stack<GameObject> GetBulletStack()
		{
			return Bullets;
		}

		#endregion
	}
}