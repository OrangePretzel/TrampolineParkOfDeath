using Sirenix.OdinInspector;
using UnityEngine;

namespace TPoD
{
	[System.Serializable]
	public class GameState
	{
		public bool IsPlaying;
		public bool IsGameOver;

		public float Time;
		public int Score;

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
		[SerializeField] private ParkBuilder _parkBuilder = null;

		[SerializeField] private GameObject _playerObject = null;
		[SerializeField] [ReadOnly] private GameState _gameState = null;
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

		[Header("Object Pools")]
		[SerializeField] private WaspObjectPool _waspObjectPool = null;
		public WaspObjectPool WaspObjectPool => _waspObjectPool;

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
		}

		#endregion
	}
}
