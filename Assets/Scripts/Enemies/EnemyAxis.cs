using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
	public class EnemyAxis : MonoBehaviour
	{
		#region Serialized Private Fields

		[SerializeField] private float radius;
		[SerializeField] private float dropSpeed = 1;
		[SerializeField] private GameObject[] enemies;
		[SerializeField] private GameObject enemyRotation;

		#endregion

		#region Private Static Fields

		private static Stack<GameObject> _enemyAxes;

		#endregion

		#region Function Events

		private void Update()
		{
			if (!(transform.position.y < -5)) return;
			// PositionEnemy(Random.Range(0, 360));
			transform.rotation = Quaternion.identity;
			gameObject.SetActive(false);
			enemyRotation.transform.position = transform.position;
			foreach (var enemy in enemies)
				enemy.SetActive(true);
		}

		private void FixedUpdate()
		{
			transform.position += Vector3.down * Time.deltaTime * dropSpeed;
		}

		#endregion

		#region Public Methods

		public static void SetStack(Stack<GameObject> stack)
		{
			_enemyAxes = stack;
		}

		public void PositionEnemy(float degree)
		{
			enemyRotation.transform.position += Vector3.right * radius;
			foreach (var enemy in enemies)
				enemy.SetActive(true);
			transform.rotation = Quaternion.Euler(0, degree, 0);
		}

		#endregion

		#region Private Methods

		private void OnDisable()
		{
			_enemyAxes.Push(gameObject);
		}

		#endregion
	}
}