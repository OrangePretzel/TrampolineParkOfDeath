using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace TPoD
{
    [System.Serializable]
    public struct WaveData
    {
        public int numEnemies;
    }

    public class WaveManager : MonoBehaviour
    {
        /******* Events *******/

        public delegate void OnWaveStarted(int waveIndex);
        public event OnWaveStarted onWaveStarted;

        public delegate void OnEnemyCountChanged(int newEnemyCount);
        public event OnEnemyCountChanged onEnemyCountChanged;

        /******* Variables & Properties*******/

        [SerializeField] private List<WaveData> _waveDatas;
        [SerializeField] private Vector3 _minSpawnPoint;
        [SerializeField] private Vector3 _maxSpawnPoint;
        [SerializeField] private float _playerSafeAreaRadius;

        [SerializeField] private WaspObjectPool _waspObjectPool = null;
        public WaspObjectPool WaspObjectPool => _waspObjectPool;

        private List<Wasp> _wasps = new List<Wasp>();

        public WaveData waveData
        {
            get
            {
                int currentIndex = GameManager.GameState.CurrentWaveIndex;
                return currentIndex < _waveDatas.Count ? _waveDatas[currentIndex] : _waveDatas[_waveDatas.Count - 1];
            }
        }

        /******* Monobehavior Methods *******/

        // TODO : THIS IS A CHEAT, REMOVE
        public void Update()
        {
            if (Input.GetButtonDown(TrampolineConstants.InputConstants.CHEAT_DEAL_DAMAGE))
            {
                int randomEnemyIndex = Random.Range(0, _wasps.Count);
                _wasps[randomEnemyIndex].health.DealDamage(1);
            }
        }

        /******* Methods *******/

        public void Clear()
        {
            foreach (var wasp in _wasps)
            {
                _waspObjectPool.ReturnObjectToPool(wasp);
            }
            _wasps.Clear();
            GameManager.GameState.CurrentWaveIndex = 0;
            GameManager.GameState.NumEnemiesRemaining = 0;
        }

        public void StartWave(int waveIndex)
        {
            GameManager.GameState.CurrentWaveIndex = waveIndex;
            GameManager.GameState.NumEnemiesRemaining = waveData.numEnemies;

            // Create Wasps for the next wave
            for (int i = 0; i < waveData.numEnemies; i++)
            {
                Wasp wasp = _waspObjectPool.GetObjectFromPool();
                wasp.health.onDeath += HandleEnemyDeath;
                wasp.transform.position = GetRandomSpawnPoint();
                _wasps.Add(wasp);
            }

            onEnemyCountChanged?.Invoke(waveData.numEnemies);
            onWaveStarted?.Invoke(GameManager.GameState.CurrentWaveIndex);
        }

        private Vector3 GetRandomSpawnPoint()
        {
            while (true)
            {
                Vector3 randomPoint = new Vector3(Random.Range(_minSpawnPoint.x, _maxSpawnPoint.x), Random.Range(_minSpawnPoint.y, _maxSpawnPoint.y), Random.Range(_minSpawnPoint.z, _maxSpawnPoint.z));
                if (!CheckWhetherPointInsideSphere(randomPoint, GameManager.Instance.PlayerTransform.position, _playerSafeAreaRadius))
                {
                    return randomPoint;
                }
            }
        }

        private bool CheckWhetherPointInsideSphere(Vector3 point, Vector3 sphereOrigin, float sphereRadius)
        {
            return Math.Pow(point.x - sphereOrigin.x, 2) + Math.Pow(point.y - sphereOrigin.y, 2) + Math.Pow(point.z - sphereOrigin.z, 2) < Math.Pow(sphereRadius, 2);
        }

        private void HandleEnemyDeath(GameObject enemy)
        {
            Wasp wasp = enemy.GetComponent<Wasp>();
            wasp.health.onDeath -= HandleEnemyDeath;
            _waspObjectPool.ReturnObjectToPool(wasp);
            _wasps.Remove(wasp);
            GameManager.GameState.NumEnemiesRemaining -= 1;

            onEnemyCountChanged?.Invoke(GameManager.GameState.NumEnemiesRemaining);

            // Wave Ends If No Enemies!
            if (GameManager.GameState.NumEnemiesRemaining <= 0)
            {
                StartWave(GameManager.GameState.CurrentWaveIndex + 1);
            }
        }
    }
}
