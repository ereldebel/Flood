using UnityEngine;

namespace Player
{
	public class Shoot : MonoBehaviour
	{
		[SerializeField] private GameObject bullet;

		private void Awake()
		{
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				var newBullet = Instantiate(bullet, transform);
				newBullet.GetComponent<Rigidbody>().AddForce(transform.up, ForceMode.Impulse);
			}
		}
	}
}