using UnityEngine;

public class Water : MonoBehaviour
{
	// [SerializeField] private float waterRiseTime = 5;
	// [SerializeField] private float waterRiseSpeed = 5;
	private void OnCollisionEnter(Collision collision)
	{
		collision.gameObject.SetActive(false);
		// StartCoroutine(RaiseWater());
	}

	// private IEnumerator RaiseWater()
	// {
	// 	
	// 	yield return new WaitForUpdate();
	// }
}
