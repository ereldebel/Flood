using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Screens
{
	public class StartOnClick : MonoBehaviour
	{

		private const int GameScene = 1;

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				SceneManager.LoadScene(GameScene);
			}
		}
	}
}