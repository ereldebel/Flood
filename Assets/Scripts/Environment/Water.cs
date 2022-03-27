using System.Collections;
using Enemies;
using UnityEngine;

namespace Environment
{
	public class Water : MonoBehaviour
	{
		#region Serialized Private Fields

		[SerializeField] private float waterRiseTime = 5;
		[SerializeField] private float waterRiseSpeed = 5;
		[SerializeField] private float waterResetSpeed = 5;
		[SerializeField] private GameObject floatingObjectPrefab;
		[SerializeField] private Transform floatingObjectsParent;
		[SerializeField] private int numberOfFloatingObjects = 20;
		[SerializeField] private float minFloatingObjectsDistance = 60;
		[SerializeField] private float maxFloatingObjectsDistance = 80;
		[SerializeField] private float floatHeight = 1;

		#endregion

		#region Private Fields

		private Transform _transform;
		private float _currentRiseFinishTime = 0;
		private float _originalWaterHeight;

		#endregion

		#region Function Events

		private void Awake()
		{
			InitializeFields();
			AddFloatingObjects();
			GameManager.WaveCleared += ResetHeight;
		}

		private void OnTriggerEnter(Collider other)
		{
			var enemyID = other.GetComponentInParent<Enemy>()?.TakeHit(false);
			if (Time.time >= _currentRiseFinishTime && enemyID != null)
			{
				_currentRiseFinishTime = Time.time + (int) enemyID * waterRiseTime;
				StartCoroutine(RaiseWater());
				return;
			}

			_currentRiseFinishTime += waterRiseTime;
		}

		private void OnDestroy()
		{
			GameManager.WaveCleared -= ResetHeight;
		}

		#endregion

		#region Private Methods

		private void AddFloatingObjects()
		{
			for (var i = 0; i < numberOfFloatingObjects; ++i)
			{
				var direction2D = Random.insideUnitCircle *
				                  Random.Range(minFloatingObjectsDistance, maxFloatingObjectsDistance);
				Instantiate(floatingObjectPrefab,
					new Vector3(direction2D.x, _transform.position.y + floatHeight, direction2D.y),
					Random.rotation, floatingObjectsParent);
			}
		}

		private void InitializeFields()
		{
			_transform = transform;
			_originalWaterHeight = _transform.position.y;
			waterRiseSpeed *= Time.fixedDeltaTime;
			waterResetSpeed *= Time.fixedDeltaTime;
		}

		private void ResetHeight(int waveNumber)
		{
			_currentRiseFinishTime = Time.time;
			StartCoroutine(ResetWaterHeight());
		}

		#endregion

		#region Private Coroutines

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

		private IEnumerator ResetWaterHeight()
		{
			while (_transform.position.y > _originalWaterHeight)
			{
				var pos = transform.position;
				pos.y -= waterResetSpeed;
				_transform.position = pos;
				yield return new WaitForFixedUpdate();
			}
		}

		#endregion
	}
}