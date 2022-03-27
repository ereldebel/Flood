using UnityEngine;

public class AudioManager : MonoBehaviour
{
	#region Serialized fields

	[SerializeField, Tooltip("Clips by index:\n0\tGun Shot\n1\tEnemy Hit\n2\tEnemy Reached Water\n3\tGame Over\n4\tGame Started\n5\tWave Cleared")]
	private AudioClip[] clips;

	#endregion

	#region Private fields

	private static AudioManager _shared;
	private AudioSource _audio;

	#endregion

	#region Function events

	private void Awake()
	{
		if (_shared != null)
		{
			Destroy(gameObject);
			return;
		}

		_shared = this;
		DontDestroyOnLoad(gameObject);
		_audio = GetComponent<AudioSource>();
	}

	#endregion

	#region Public methods

	public static void GunShot() => PlayClipByIndex(0);

	public static void EnemyHit() => PlayClipByIndex(1);

	public static void EnemyReachedWater() => PlayClipByIndex(2);

	public static void GameOver() => PlayClipByIndex(3);
	
	public static void GameStarted() => PlayClipByIndex(4);

	public static void WaveCleared() => PlayClipByIndex(5);

	#endregion

	#region Private Methods

	private static void PlayClipByIndex(int clipIndex)
	{
		if (clipIndex < _shared.clips.Length && _shared.clips[clipIndex] != null)
			_shared._audio.PlayOneShot(_shared.clips[clipIndex]);
	}

	#endregion
}