using Sirenix.OdinInspector;
using TPoD.UI;
using UnityEngine;
using System.Collections;
using Metamesa.MMUnity.ObjectPooling;
using UnityEngine.SceneManagement;
using UnityEditor;

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

		public float PlayerHealth => GameManager.Instance.Player.health.health;

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

		[SerializeField] public Player Player = null;
		[SerializeField] [ReadOnly] private GameState _gameState = null;
		[SerializeField] private WaveManager _waveManager;

		[SerializeField] public WaspProjectilePool WaspProjectilePool;
		[SerializeField] public DyingWaspObjectPool DyingWaspPool;
		[SerializeField] public ExplosionObjectPool ExplosionPool;

		public static GameState GameState
		{
			get
			{
				if (Instance == null)
					return null;
				return Instance._gameState;
			}
		}

		public Transform PlayerTransform => Player.gameObject.transform;

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
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;

			if (!MakeSingleton())
			{
				Debug.Log($"Singleton for {nameof(GameManager)} already exists. Destroying self", this);
				Destroy(this);
				return;
			}

			SetupPark();

			_waveManager.onEnemyCountChanged += _hud.SetEnemyCount;
			_waveManager.onWaveStarted += _hud.SetWaveCount;

			Player.health.onDeath += HandleGameOver;
			_hud.onReplay += HandleReplay;
			_hud.onQuit += HandleQuit;
			_hud.onStartGame += HandlePlayGame;

			_hud.ToggleMainMenu(true);
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
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			_gameState = new GameState();
			_gameState.IsPlaying = true;

			Player.transform.position = Vector3.up * 3;
			Player.health.ToggleInvincibility(true, 3f);

			_waveManager.Clear();
			StartCoroutine(StartWaveManagerAfterDelay(2f));

			_hud.ToggleGameOver(false);
			_hud.ToggleMainMenu(false);
		}

		private IEnumerator StartWaveManagerAfterDelay(float delaySeconds)
		{
			yield return new WaitForSeconds(delaySeconds);

			_waveManager.StartWave(0);
		}

		public void HandleGameOver(GameObject playerObject)
		{
			_gameState.IsPlaying = false;
			_hud.ToggleGameOver(true);
			_hud.ToggleMainMenu(false);
		}

		private void HandleReplay()
		{
			_hud.ToggleGameOver(false);
			_hud.ToggleMainMenu(false);
			//StartNewGame();

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
		}

		private void HandlePlayGame()
		{
			StartNewGame();
		}

		private void HandleQuit()
		{
			if (Application.isEditor)
				Debug.Log("Quitting");
			else
				Application.Quit();
		}

		#endregion
	}
}
