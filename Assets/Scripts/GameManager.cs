using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	#region Serialized Private Fields

	[SerializeField] private EnemyGenerator enemyGenerator;

	[SerializeField] private float _waveInterval;

	#endregion

	#region Private Fields

	private int _points;
	private int _columns = 4;
	private float _nextEnemyWave;

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
		if (Time.time >= _nextEnemyWave)
		{
			enemyGenerator.GenerateEnemy();
			_nextEnemyWave += _waveInterval;
		}

		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		}
	}

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
		PointsUpdated?.Invoke(_shared._points += _shared._columns);
	}

	public static void GameOver()
	{
		SceneManager.LoadScene(0);
	}

	public static void ColumnDrowned()
	{
		if (--_shared._columns <= 0)
			GameOver();
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