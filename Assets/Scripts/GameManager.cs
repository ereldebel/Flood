using UnityEngine;

public class GameManager : MonoBehaviour
{
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

	#endregion
}