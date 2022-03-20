using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyAxis : MonoBehaviour
{
	public float radius;

	[SerializeField] private GameObject[] enemies;

	public float dropSpeed = 1;

	private static Stack<GameObject> _enemyAxes;

	private void Update()
	{
		if (!(transform.position.y < -5)) return;
		// PositionEnemy(Random.Range(0, 360));
		gameObject.SetActive(false);
		foreach (var enemy in enemies)
		{
			enemy.transform.position = transform.position + Vector3.down;
			enemy.SetActive(true);
		}
	}

	private void FixedUpdate()
	{
		transform.position += Vector3.down * Time.deltaTime * dropSpeed;
	}

	public void PositionEnemy(float degree)
	{
		foreach (var enemy in enemies)
		{
			enemy.transform.position += Vector3.right * radius;
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