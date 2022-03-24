using UnityEngine;

namespace Enemies
{
	public class EnemyLight : MonoBehaviour
	{
		#region Serialized Private Field

		[SerializeField] private float minEnemyHeight = -14;
		[SerializeField] private float maxIntensity = 100;

		#endregion

		#region Private Fields

		private float _coefficient;
		private float _startingY;
		private Light _light;
		private Transform _transform;

		#endregion

		#region Function Events

		private void Awake()
		{
			_transform = transform;
			_light = GetComponent<Light>();
		}

		private void Start()
		{
			_startingY = _transform.position.y;
			_coefficient = 1 / (_startingY - minEnemyHeight);
		}

		private void Update()
		{
			_light.intensity = maxIntensity * Mathf.Pow((_startingY - _transform.position.y) * _coefficient, 2);
		}

		#endregion
	}
}