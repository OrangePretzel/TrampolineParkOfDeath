using UnityEngine;

namespace TPoD
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField] private ParkBuilder _parkBuilder = null;

		[SerializeField] private GameObject _playerObject = null;
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
		}

		private void SetupPark()
		{
			if (_parkBuilder == null)
				return; // Nothing to setup

			_parkBuilder.BuildPark();
		}
	}
}
