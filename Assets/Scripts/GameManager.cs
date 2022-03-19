using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Serialized Private Fields

	[SerializeField] private Transform water;
	
	#endregion
	
	#region Function Events

	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		}
	}

	private void RaiseWater()
	{
		var y = water.position.y;
	}

	#endregion
}