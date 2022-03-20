using System.Collections;
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

		#endregion

		#region Function Events

		private void Awake()
		{
			_transform = transform;
			waterRiseSpeed *= Time.fixedDeltaTime;
		}

		private void Update()
		{
			if (_transform.position.y >= 0)
				GameManager.GameOver();
		}

		private void OnTriggerEnter(Collider other)
		{
			other.gameObject.SetActive(false);
			if (Time.time >= _currentRiseFinishTime)
			{
				_currentRiseFinishTime = Time.time + waterRiseTime;
				StartCoroutine(RaiseWater());
				return;
			}

			_currentRiseFinishTime += waterRiseTime;
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

		#endregion
	}
}