using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Inkblade.Enemies
{
    /// <summary>
    /// Manages enemy spawning and wave system.
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int maxEnemies = 10;
        [SerializeField] private float spawnInterval = 3f;
        [SerializeField] private float spawnRadius = 10f;

        [Header("Wave Settings")]
        [SerializeField] private int enemiesPerWave = 5;
        [SerializeField] private float waveDelay = 5f;
        [SerializeField] private bool autoStart = true;

        [Header("Spawn Points")]
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private bool useRandomSpawnPoints = true;

        private List<GameObject> _activeEnemies = new List<GameObject>();
        private int _currentWave = 0;
        private bool _isSpawning = false;
        private Coroutine _spawnCoroutine;

        // Events
        public System.Action<int> OnWaveStarted; // wave number
        public System.Action<int> OnWaveCompleted; // wave number
        public System.Action OnAllWavesCompleted;

        private void Start()
        {
            if (autoStart)
            {
                StartWave(1);
            }
        }

        public void StartWave(int waveNumber)
        {
            if (_isSpawning) return;

            _currentWave = waveNumber;
            _isSpawning = true;
            OnWaveStarted?.Invoke(waveNumber);

            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }

            _spawnCoroutine = StartCoroutine(SpawnWaveCoroutine());
        }

        private IEnumerator SpawnWaveCoroutine()
        {
            int enemiesSpawned = 0;

            while (enemiesSpawned < enemiesPerWave && _activeEnemies.Count < maxEnemies)
            {
                SpawnEnemy();
                enemiesSpawned++;

                yield return new WaitForSeconds(spawnInterval);
            }

            // Wait for all enemies to be defeated
            yield return StartCoroutine(WaitForWaveCompletion());

            OnWaveCompleted?.Invoke(_currentWave);
            _isSpawning = false;

            // Start next wave after delay
            yield return new WaitForSeconds(waveDelay);
            StartWave(_currentWave + 1);
        }

        private IEnumerator WaitForWaveCompletion()
        {
            while (_activeEnemies.Count > 0)
            {
                // Remove null references (destroyed enemies)
                _activeEnemies.RemoveAll(e => e == null);
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void SpawnEnemy()
        {
            if (enemyPrefab == null)
            {
                Debug.LogError("EnemySpawner: Enemy prefab is not assigned!");
                return;
            }

            Vector3 spawnPosition = GetSpawnPosition();
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Subscribe to enemy death
            var enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.OnDeath += () => OnEnemyDeath(enemy);
            }

            _activeEnemies.Add(enemy);
        }

        private Vector3 GetSpawnPosition()
        {
            if (useRandomSpawnPoints && spawnPoints != null && spawnPoints.Length > 0)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                return spawnPoint.position;
            }

            // Random position around spawner
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            return transform.position + new Vector3(randomCircle.x, randomCircle.y, 0f);
        }

        private void OnEnemyDeath(GameObject enemy)
        {
            _activeEnemies.Remove(enemy);
        }

        public void StopSpawning()
        {
            _isSpawning = false;
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }
        }

        public void ClearAllEnemies()
        {
            foreach (GameObject enemy in _activeEnemies)
            {
                if (enemy != null)
                {
                    Destroy(enemy);
                }
            }
            _activeEnemies.Clear();
        }

        // Getters
        public int ActiveEnemyCount => _activeEnemies.Count;
        public int CurrentWave => _currentWave;
        public bool IsSpawning => _isSpawning;

        private void OnDrawGizmosSelected()
        {
            // Draw spawn radius
            Gizmos.color = Color.green;
            Gizmos.DrawWireCircle(transform.position, spawnRadius);

            // Draw spawn points
            if (spawnPoints != null)
            {
                Gizmos.color = Color.blue;
                foreach (Transform point in spawnPoints)
                {
                    if (point != null)
                    {
                        Gizmos.DrawWireSphere(point.position, 0.5f);
                    }
                }
            }
        }
    }
}

