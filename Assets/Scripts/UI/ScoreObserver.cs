using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
	public class ScoreObserver : MonoBehaviour
	{
		#region Serialized Fields

		private TextMeshProUGUI _scoreUI;

		#endregion

		#region Function Events

		private void Awake()
		{
			_scoreUI = GetComponent<TextMeshProUGUI>();
			GameManager.ScoreUpdated += UpdateScore;
		}

		private void OnDestroy()
		{
			GameManager.ScoreUpdated -= UpdateScore;
		}

		#endregion

		#region Private Methods

		private void UpdateScore(int score)
		{
			_scoreUI.text = score.ToString();
		}

		#endregion
	}
}