using Sirenix.OdinInspector;
using UnityEngine;
using TPoD.UI;

namespace TPoD
{
	[System.Serializable]
	public class GameState
	{
		public bool IsPlaying;
		public bool IsGameOver;

		public float Time;
		public int Score;

		// Wave Stuff
		public int CurrentWaveIndex;
		public int NumEnemiesRemaining;

		public void UpdateGameState(float deltaTime)
		{
			Time += deltaTime;
		}

		public void AddScore(int amount)
		{
			Score += amount;
		}

		public void Reset()
		{
			Score = 0;
			Time = 0;
			IsGameOver = false;
			IsPlaying = false;
		}
	}

	public class GameManager : MonoBehaviour
	{
		[SerializeField] private HUD _hud;
		[SerializeField] private ParkBuilder _parkBuilder = null;

		[SerializeField] private GameObject _playerObject = null;
		[SerializeField] [ReadOnly] private GameState _gameState = null;
		[SerializeField] private WaveManager _waveManager;

		public static GameState GameState
		{
			get
			{
				if (Instance == null)
					return null;
				return Instance._gameState;
			}
		}

		public Transform PlayerTransform => _playerObject.transform;

		#region Singleton

		public static GameManager Instance { get; private set; }

		private bool MakeSingleton()
		{
			if (Instance != null && Instance != this)
				return false;

			Instance = this;
			return true;
		}

		#endregion

		private void OnEnable()
		{
			if (!MakeSingleton())
			{
				Debug.Log($"Singleton for {nameof(GameManager)} already exists. Destroying self", this);
				Destroy(this);
				return;
			}

			SetupPark();

			_waveManager.onEnemyCountChanged += _hud.SetEnemyCount;
			_waveManager.onWaveStarted += _hud.SetWaveCount;

			StartNewGame();
		}

		private void Update()
		{
			UpdateGame();
		}

		private void SetupPark()
		{
			if (_parkBuilder == null)
				return; // Nothing to setup

			_parkBuilder.BuildPark();
		}

		private void UpdateGame()
		{
			if (_gameState == null || !_gameState.IsPlaying)
				return;

			_gameState.UpdateGameState(Time.deltaTime);
		}

		#region Public API

		public void StartNewGame()
		{
			_gameState = new GameState();
			_gameState.IsPlaying = true;

			_waveManager.StartWave(0);
		}

		#endregion
	}
}
