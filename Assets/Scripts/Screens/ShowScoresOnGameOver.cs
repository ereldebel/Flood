using TMPro;
using UnityEngine;

namespace Screens
{
	public class ShowScoresOnGameOver : MonoBehaviour
	{
		private void Start()
		{
			var score = GetComponent<TextMeshProUGUI>(); 
			var text = score.text;
			var currentScore = PlayerPrefs.GetInt(GameManager.CurrentScorePref).ToString();
			var highScore = PlayerPrefs.GetInt(GameManager.HighScorePref).ToString();
			score.text = text.Insert(text.IndexOf('\n'), currentScore) + highScore;
		}
	}
}
