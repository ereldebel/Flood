using TMPro;
using UnityEngine;

namespace UI
{
	public class WaveClearedMessage : MonoBehaviour
	{
		#region Serialized Private Fields

		[SerializeField] private string messageStart = "Wave ";
		[SerializeField] private string messageEnd = " cleared";

		#endregion

		#region Private Fields

		private Animator _animator;
		private TextMeshProUGUI _message;
		private static readonly int ShowMessage = Animator.StringToHash("Show Message");

		#endregion

		#region Function Events

		private void Awake()
		{
			_animator = GetComponent<Animator>();
			_message = GetComponent<TextMeshProUGUI>();
			GameManager.WaveCleared += ShowWaveClearedMessage;
		}

		private void OnDestroy()
		{
			GameManager.WaveCleared -= ShowWaveClearedMessage;
		}

		#endregion

		#region Private Methods

		private void ShowWaveClearedMessage(int waveNumber)
		{
			_message.text = messageStart + waveNumber + messageEnd;
			_animator.SetTrigger(ShowMessage);
		}

		#endregion
	}
}