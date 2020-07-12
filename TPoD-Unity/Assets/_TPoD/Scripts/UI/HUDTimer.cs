using TMPro;
using UnityEngine;

namespace TPoD.UI
{
	public class HUDTimer : MonoBehaviour
	{
		[SerializeField] private TMP_Text _timerText = null;

		public void UpdateTimerText(float time)
		{
			int minutes = (int)(time / 60);
			int seconds = (int)(time - minutes * 60);
			_timerText.text = $"{minutes:D2}:{seconds:D2}";
		}
	}
}
