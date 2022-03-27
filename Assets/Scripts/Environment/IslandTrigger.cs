using UnityEngine;

namespace Environment
{
	public class IslandTrigger : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			GameManager.ColumnDrowned();
			gameObject.SetActive(false);
		}
	}
}
