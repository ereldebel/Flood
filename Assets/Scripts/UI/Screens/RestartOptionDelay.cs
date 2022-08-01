using TMPro;
using UnityEngine;

namespace UI.Screens
{
	public class RestartOptionDelay : MonoBehaviour
	{
		[SerializeField] private float delayTime = 1;
		
		private StartOnClick _startOnClick;
		private TextMeshProUGUI _replayText;
		private float _startTime;

		private void Awake()
		{
			_startTime = Time.time;
			_startOnClick = GetComponent<StartOnClick>();
			_replayText = GetComponent<TextMeshProUGUI>();
		}

		private void Update()
		{
			if (Time.time - _startTime < delayTime) return;
			_startOnClick.enabled = true;
			_replayText.enabled = true;
			Destroy(this);
		}
	}
}