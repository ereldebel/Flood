using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyGenerator : MonoBehaviour
{
	[SerializeField] private GameObject enemyAxisPrefab;

	[SerializeField] private Vector3 startPos;

	private readonly Stack<GameObject> _enemyAxes = new Stack<GameObject>();

	private void Awake()
	{
		EnemyAxis.setStack(_enemyAxes);
	}

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
			enemyAxis = Instantiate(enemyAxisPrefab, startPos, quaternion.identity);
		}

		enemyAxis.GetComponent<EnemyAxis>().PositionEnemy(Random.Range(0f, 360f));
	}
}