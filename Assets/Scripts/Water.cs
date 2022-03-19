using System;
using System.Collections;
using UnityEngine;

public class Water : MonoBehaviour
{
	[SerializeField] private float waterRiseTime = 5;
	[SerializeField] private float waterRiseSpeed = 5;

	private float _currentRiseFinishTime;
	private Transform _transform;

	private void Awake()
	{
		_transform = transform;
		waterRiseSpeed /= Time.fixedDeltaTime;
	}

	private void OnCollisionEnter(Collision collision)
	{
		collision.gameObject.SetActive(false);
		if (Time.time >= _currentRiseFinishTime)
		{
			_currentRiseFinishTime = Time.time + waterRiseTime;
			StartCoroutine(RaiseWater());
			return;
		}
		_currentRiseFinishTime += waterRiseTime;
	}

	private IEnumerator RaiseWater()
	{
		while (Time.time < _currentRiseFinishTime)
		{
			var pos = transform.position;
			pos.y += waterRiseSpeed;
			_transform.position = pos;
			yield return new WaitForFixedUpdate();
		}
	}
}