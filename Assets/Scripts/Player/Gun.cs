using UnityEngine;

namespace Player
{
	public class Gun : MonoBehaviour
	{
		[SerializeField] private GameObject bullet;
		[SerializeField] private float shotVelocity = 10;

		private float _gunTip;

		private void Awake()
		{
			_gunTip = transform.localScale.y / 2;
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				bullet.GetComponent<Rigidbody>().velocity=Vector3.zero;
				var shootingDirection = transform.up;
				bullet.transform.position = transform.position + shootingDirection * _gunTip;
				bullet.GetComponent<Rigidbody>().AddForce(shotVelocity * shootingDirection, ForceMode.Impulse);
			}
		}
	}
}