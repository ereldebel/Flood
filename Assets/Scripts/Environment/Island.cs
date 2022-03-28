using System.Collections;
using UnityEngine;

namespace Environment
{
	public class Island : MonoBehaviour
	{
		[SerializeField] private float drownSpeed = 0.7f;

		private void Awake()
		{
			drownSpeed *= Time.fixedDeltaTime;
		}

		private void OnTriggerEnter(Collider other)
		{
			GameManager.ColumnDrowned();
			StartCoroutine(DrownIsland());
		}

		private IEnumerator DrownIsland()
		{
			var t = transform;
			while (t.position.y > -42)
			{
				var pos = t.position;
				pos.y -= drownSpeed;
				t.position = pos;
				yield return new WaitForFixedUpdate();
			}

			gameObject.SetActive(false);
		}
	}
}