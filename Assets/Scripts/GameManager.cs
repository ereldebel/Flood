using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Serialized Private Fields

	[SerializeField] private Transform water;

	#endregion

	#region Private Fields

	private int _points;

	#endregion

	#region Private Static Fields

	private static int _highScore;
	private static GameManager _shared;

	#endregion

	#region Constants

	private const string HighScorePref = "HighScore";

	#endregion

	#region Public C# Events

	public static event Action<int> PointsUpdated;
	public static event Action<int> HighScoreUpdated;

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

	#endregion


	#region Function Events

	private void Awake()
	{
		_shared = this;
	}

	private void Start()
	{
		RestoreHighScore();
	}


	private void OnDestroy()
	{
		UpdateHighScore();
	}

	#endregion


	#region Public Static Methods

	public static void EnemyKilled()
	{
		PointsUpdated?.Invoke(++_shared._points);
	}

	#endregion

	#region Private Static Methods

	private static void UpdateHighScore()
	{
		if (_shared._points <= _highScore) return;
		_highScore = _shared._points;
		HighScoreUpdated?.Invoke(_highScore);
		PlayerPrefs.SetInt(HighScorePref, _highScore);
		PlayerPrefs.Save();
	}

	private static void RestoreHighScore()
	{
		_highScore = PlayerPrefs.GetInt(HighScorePref, 0);
		;
		HighScoreUpdated?.Invoke(_highScore);
	}

	#endregion
}