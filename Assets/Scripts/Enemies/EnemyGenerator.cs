using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Enemies
{
	public class EnemyGenerator : MonoBehaviour
	{
		#region Serialized Private Fields

		[SerializeField] private GameObject enemyAxisPrefab;
		[SerializeField] private Vector3 startPos;

		#endregion

		#region Private Fields

		private readonly Stack<GameObject> _enemyAxes = new Stack<GameObject>();

		#endregion

		#region Function Events

		private void Awake()
		{
			EnemyAxis.SetStack(_enemyAxes);
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
				enemyAxis = Instantiate(enemyAxisPrefab, startPos, quaternion.identity, transform);
			}

			enemyAxis.GetComponent<EnemyAxis>().PositionEnemy(Random.Range(0f, 360f));
		}

		#endregion
	}
}