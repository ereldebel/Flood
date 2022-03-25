using System;
using System.Collections;
using Enemies;
using Environment;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	#region Serialized Private Fields

	[SerializeField] private WaveManager waveManager;
	[SerializeField] private float timeBetweenWaves = 3;

	#endregion

	#region Private Fields

	private int _points;
	private int _columns = 4;

	#endregion

	#region Private Static Fields

	private static int _highScore;
	private static GameManager _shared;

	#endregion

	#region Constants

	private const string HighScorePref = "High Score";

	#endregion

	#region Public C# Events

	public static event Action<int> PointsUpdated;
	public static event Action<int> HighScoreUpdated;

	public static event Action<int> WaveCleared;

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

	private void Start()
	{
		_shared = this;
		RestoreHighScore();
		waveManager.StartNextWave();
	}


	private void OnDisable()
	{
		UpdateHighScore();
	}

	#endregion

	#region Public Static Methods

	public static void EnemyKilled(int type)
	{
		PointsUpdated?.Invoke(_shared._points += (_shared._columns * type));
	}

	public static void ColumnDrowned()
	{
		if (--_shared._columns <= 0)
			GameOver();
	}

	public static void ClearedWave(int waveNumber)
	{
		WaveCleared?.Invoke(waveNumber);
		_shared.StartCoroutine(StartNextWaveIn(_shared.timeBetweenWaves));
	}

	#endregion

	#region Private Static Methods
	
	private static void GameOver()
	{
		SceneManager.LoadScene(0);
	}

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
		HighScoreUpdated?.Invoke(_highScore);
	}

	private static IEnumerator StartNextWaveIn(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		_shared.waveManager.StartNextWave();
	}

	#endregion
}