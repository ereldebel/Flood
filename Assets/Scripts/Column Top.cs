using UnityEngine;

public class ColumnTop : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		GameManager.ColumnDrowned();
		gameObject.SetActive(false);
	}
}
