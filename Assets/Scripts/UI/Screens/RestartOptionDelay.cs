using TMPro;
using UnityEngine;

namespace UI.Screens
{
	public class RestartOptionDelay : MonoBehaviour
	{
		private StartOnClick _startOnClick;
		private TextMeshProUGUI _replayText;
		[SerializeField] private float delayTime = 1;

		private void Awake()
		{
			_startOnClick = GetComponent<StartOnClick>();
			_replayText = GetComponent<TextMeshProUGUI>();
		}

		private void Update()
		{
			if (Time.time < delayTime) return;
			_startOnClick.enabled = true;
			_replayText.enabled = true;
			Destroy(this);
		}
	}
}