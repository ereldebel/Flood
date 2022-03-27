using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace Enemies
{
	public class EnemyGenerator : MonoBehaviour
	{
		private const int WholeCircle = 360;

		#region Serialized Private Fields

		[SerializeField] private GameObject[] enemyAxisPrefab;
		[SerializeField] private Vector3 startPos;
		[SerializeField] private float angleBufferBetweenConsecutiveBatches = 40;
		
		#endregion

		#region Private Fields

		private readonly Stack<GameObject> _enemyAxes = new Stack<GameObject>();
		private float _angleBuffer;
		private float _prevEnemyAngle = 0;

		#endregion

		#region Function Events

		private void Awake()
		{
			EnemyAxis.SetStack(_enemyAxes);
			_angleBuffer = angleBufferBetweenConsecutiveBatches / 2;
		}

		#endregion

		#region Public Methods

		public void GenerateEnemy()
		{
			GameObject enemyAxis;
			try
			{
				enemyAxis = _enemyAxes.Pop();
				enemyAxis.SetActive(true);
				enemyAxis.transform.position = startPos;
			}
			catch (InvalidOperationException)
			{
				var enemyTypeIndex = Random.Range(0, enemyAxisPrefab.Length);
				var newEnemy = enemyAxisPrefab[enemyTypeIndex];
				enemyAxis = Instantiate(newEnemy, startPos, quaternion.identity, transform);
			}

			var enemyAngle =
				Random.Range(_prevEnemyAngle + _angleBuffer, _prevEnemyAngle + WholeCircle - _angleBuffer) %
				WholeCircle;
			enemyAxis.GetComponent<EnemyAxis>().PositionEnemy(enemyAngle);
			_prevEnemyAngle = enemyAngle;
		}

		#endregion
	}
}