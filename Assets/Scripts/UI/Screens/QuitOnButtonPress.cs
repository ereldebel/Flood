using UnityEngine;

namespace UI.Screens
{
	public class QuitOnButtonPress : MonoBehaviour
	{

		[SerializeField] private KeyCode quitButton = KeyCode.Escape;
		private void Update()
		{
			if (Input.GetKeyDown(quitButton))
			{
				Application.Quit();
#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
#endif
			}
		}
	}
}