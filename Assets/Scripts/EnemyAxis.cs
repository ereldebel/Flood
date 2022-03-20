using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyAxis : MonoBehaviour
{
	public float radius;

	[SerializeField] private GameObject[] enemies;

	[SerializeField] private GameObject enemyRotation;

	public float dropSpeed = 1;

	private static Stack<GameObject> _enemyAxes;

	private void Update()
	{
		if (!(transform.position.y < -5)) return;
		// PositionEnemy(Random.Range(0, 360));
		transform.rotation = Quaternion.identity;
		gameObject.SetActive(false);
		enemyRotation.transform.position = transform.position;
		foreach (var enemy in enemies)
		{
			enemy.SetActive(true);
		}
	}

	private void FixedUpdate()
	{
		transform.position += Vector3.down * Time.deltaTime * dropSpeed;
	}

	public void PositionEnemy(float degree)
	{
		enemyRotation.transform.position += Vector3.right * radius;
		foreach (var enemy in enemies)
		{
			enemy.SetActive(true);
		}
		transform.rotation = Quaternion.Euler(0, degree, 0);
		
	}

	private void OnDisable()
	{
		_enemyAxes.Push(gameObject);
	}

	public static void setStack(Stack<GameObject> stack)
	{
		_enemyAxes = stack;
	}
}