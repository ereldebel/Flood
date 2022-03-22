using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
	public class EnemyAxis : MonoBehaviour
	{
		#region Public Properties

		public int RemainingEnemies
		{
			get => _remainingEnemies;
			set
			{
				_remainingEnemies = value;
				if (_remainingEnemies == 0)
					gameObject.SetActive(false);
			}
		}

		#endregion

		#region Serialized Private Fields

		[SerializeField] private float radius;
		[SerializeField] private float dropSpeed = 1;
		[SerializeField] private GameObject[] enemies;
		[SerializeField] private Transform enemyBatchLocation;

		#endregion

		#region Private Fields

		private Transform _transform;
		private WaveManager _waveManager;
		private int _remainingEnemies;

		#endregion

		#region Private Static Fields

		private static Stack<GameObject> _enemyAxes;

		#endregion

		#region Function Events

		private void Awake()
		{
			_transform = transform;
			_waveManager = _transform.parent.GetComponent<WaveManager>();
			enemyBatchLocation.position = _transform.position + Vector3.right * radius;
		}

		private void OnEnable()
		{
			_remainingEnemies = enemies.Length;
		}

		private void FixedUpdate()
		{
			_transform.position += Vector3.down * Time.deltaTime * dropSpeed;
		}

		#endregion

		#region Public Methods

		public static void SetStack(Stack<GameObject> stack)
		{
			_enemyAxes = stack;
		}

		public void PositionEnemy(float degree)
		{
			_transform.rotation = Quaternion.Euler(0, degree, 0);
		}

		#endregion

		#region Private Methods

		private void OnDisable()
		{
			--_waveManager.NumberOfLivingBatches;
			_transform.rotation = Quaternion.identity;
			foreach (var enemy in enemies)
				enemy.SetActive(true);
			_enemyAxes.Push(gameObject);
		}

		#endregion
	}
}