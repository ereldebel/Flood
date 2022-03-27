using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	#region Serialized Private Fields

	[SerializeField] private WaveManager waveManager;
	[SerializeField] private float timeBetweenWaves = 3;
	[SerializeField] private int columns = 5;
	
	[SerializeField] private GameObject explode;

	#endregion

	#region Private Fields

	private int _score;

	private readonly Stack<GameObject> _explosions = new Stack<GameObject>();

	#endregion

	#region Private Static Fields

	private static int _highScore;
	private static GameManager _shared;

	#endregion

	#region Constants

	public const string HighScorePref = "High Score";
	public const string CurrentScorePref = "Current Score";
	private const int GameOverScene = 2;

	#endregion

	#region Public C# Events

	public static event Action<int> ScoreUpdated;
	public static event Action<int> WaveCleared;

	#endregion

	#region Function Events

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			GameOver();
	}

	private void Start()
	{
		_shared = this;
		_highScore = PlayerPrefs.GetInt(HighScorePref, 0);
		waveManager.StartNextWave();
		AudioManager.GameStarted();
	}


	private void OnDisable()
	{
		UpdateScores();
	}

	#endregion

	#region Public Static Methods

	public static void EnemyKilled(int type)
	{
		ScoreUpdated?.Invoke(_shared._score += (_shared.columns * type));
	}

	public static void ColumnDrowned()
	{
		if (--_shared.columns <= 0)
			GameOver();
	}

	public static void ClearedWave(int waveNumber)
	{
		WaveCleared?.Invoke(waveNumber);
		AudioManager.WaveCleared();
		_shared.StartCoroutine(StartNextWaveIn(_shared.timeBetweenWaves));
	}

	public static void SetExplosion(Vector3 pos)
	{
		try
		{
			var exp = _shared._explosions.Pop();
			exp.transform.position = pos;
			exp.SetActive(true);
		}
		catch (InvalidOperationException)
		{
			Instantiate(_shared.explode, pos, Quaternion.identity);
		}
	}

	public static void AddExplosion(GameObject exp)
	{
		_shared._explosions.Push(exp);
	}

	#endregion

	#region Private Static Methods
	
	private static void GameOver()
	{
		AudioManager.GameOver();
		SceneManager.LoadScene(GameOverScene);
	}

	private static void UpdateScores()
	{
		PlayerPrefs.SetInt(CurrentScorePref, _shared._score);
		if (_shared._score > _highScore)
		{
			_highScore = _shared._score;
			PlayerPrefs.SetInt(HighScorePref, _highScore);
		}
		PlayerPrefs.Save();
	}

	private static IEnumerator StartNextWaveIn(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		_shared.waveManager.StartNextWave();
	}

	#endregion
}