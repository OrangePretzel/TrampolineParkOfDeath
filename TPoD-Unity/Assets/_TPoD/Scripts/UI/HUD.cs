using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace TPoD.UI
{
	public class HUD : MonoBehaviour
	{
		public Action onReplay;

		public Action onStartGame;
		public Action onQuit;

		private const string WAVE_COUNT_FORMAT = "Wave {0}";
		private const string ENEMY_COUNT_FORMAT = "Enemies Left : {0}";

		[SerializeField] private HUDTimer _hudTimer = null;
		[SerializeField] private TextMeshProUGUI _waveCountText;
		[SerializeField] private TextMeshProUGUI _enemyCountText;

		[SerializeField] private GameObject _gameOverObject;
		[SerializeField] private GameObject _mainMenuObject;

		private void Update()
		{
			var gameState = GameManager.GameState;
			if (gameState == null)
				return;

			if (gameState.IsPlaying)
			{
				_hudTimer.UpdateTimerText(gameState.Time);
			}


			if (_mainMenuObject.activeSelf)
			{
				if (Input.GetButtonDown(TrampolineConstants.InputConstants.REPLAY))
				{
					onStartGame?.Invoke();
				}

				if (Input.GetButtonDown(TrampolineConstants.InputConstants.QUIT))
				{
					onQuit?.Invoke();
				}
			}

			if (_gameOverObject.activeSelf && Input.GetButtonDown(TrampolineConstants.InputConstants.REPLAY))
			{
				onReplay?.Invoke();
			}
		}

		public void SetEnemyCount(int enemyCount)
		{
			_enemyCountText.SetText(string.Format(ENEMY_COUNT_FORMAT, enemyCount.ToString()));
		}

		public void SetWaveCount(int waveCount)
		{
			_waveCountText.SetText(string.Format(WAVE_COUNT_FORMAT, waveCount.ToString()));
		}

		public void ToggleGameOver(bool isGameOver)
		{
			_gameOverObject.SetActive(isGameOver);
		}
	}
}
