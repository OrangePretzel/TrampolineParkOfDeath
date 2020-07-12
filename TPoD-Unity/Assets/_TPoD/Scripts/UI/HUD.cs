using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TPoD.UI
{
	public class HUD : MonoBehaviour
	{
		private const string WAVE_COUNT_FORMAT = "Wave {0}";
		private const string ENEMY_COUNT_FORMAT = "Enemy {0}";


		[SerializeField] private HUDTimer _hudTimer = null;
		[SerializeField] private TextMeshProUGUI _waveCountText;
		[SerializeField] private TextMeshProUGUI _enemyCountText;

		private void Update()
		{
			var gameState = GameManager.GameState;
			if (gameState == null)
				return;

			if (!gameState.IsPlaying)
				return;

			_hudTimer.UpdateTimerText(gameState.Time);
		}

		public void SetEnemyCount(int enemyCount)
		{
			_enemyCountText.SetText(string.Format(ENEMY_COUNT_FORMAT, enemyCount.ToString()));
		}

		public void SetWaveCount(int waveCount)
		{
			_waveCountText.SetText(string.Format(WAVE_COUNT_FORMAT, waveCount.ToString()));
		}
	}
}
