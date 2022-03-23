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

		#endregion

		#region Private Fields

		private Transform _transform;
		private float _currentRiseFinishTime = 0;
		private float _originalWaterHeight;

		#endregion

		#region Function Events

		private void Awake()
		{
			_transform = transform;
			_originalWaterHeight = _transform.position.y;
			waterRiseSpeed *= Time.fixedDeltaTime;
			GameManager.WaveCleared += ResetHeight;
		}

		private void OnTriggerEnter(Collider other)
		{
			other.GetComponent<IHittable>()?.TakeHit(false);
			if (Time.time >= _currentRiseFinishTime)
			{
				_currentRiseFinishTime = Time.time + waterRiseTime;
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

		private void ResetHeight(int waveNumber)
		{
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
				pos.y -= waterRiseSpeed;
				_transform.position = pos;
				yield return new WaitForFixedUpdate();
			}
		}

		#endregion
	}
}