using UnityEngine;
using Inkblade.Player;
using Inkblade.Enemies;

namespace Inkblade.Systems
{
    /// <summary>
    /// Main game manager coordinating all systems.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Game Settings")]
        [SerializeField] private bool pauseOnStart = false;
        [SerializeField] private float slowMotionScale = 0.3f;
        [SerializeField] private float slowMotionDuration = 0.2f;

        private Coroutine _slowMotionCoroutine;

        [Header("References")]
        [SerializeField] private PlayerController player;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private CameraController cameraController;

        private bool _isPaused = false;
        private bool _isGameOver = false;
        private float _gameTime = 0f;
        private int _score = 0;
        private int _enemiesKilled = 0;

        // Events
        public System.Action<bool> OnPauseChanged;
        public System.Action OnGameOver;
        public System.Action OnGameStart;
        public System.Action<int> OnScoreChanged;
        public System.Action<float> OnGameTimeChanged;

        // Singleton
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            InitializeGame();
        }

        private void Update()
        {
            if (!_isPaused && !_isGameOver)
            {
                _gameTime += Time.deltaTime;
                OnGameTimeChanged?.Invoke(_gameTime);
            }

            // Pause input
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        private void InitializeGame()
        {
            // Find references if not assigned
            if (player == null)
            {
                player = FindObjectOfType<PlayerController>();
            }

            if (playerHealth == null)
            {
                playerHealth = FindObjectOfType<PlayerHealth>();
            }

            if (enemySpawner == null)
            {
                enemySpawner = FindObjectOfType<EnemySpawner>();
            }

            if (cameraController == null)
            {
                cameraController = FindObjectOfType<CameraController>();
            }

            // Subscribe to events
            if (playerHealth != null)
            {
                playerHealth.OnDeath += HandlePlayerDeath;
            }

            if (enemySpawner != null)
            {
                enemySpawner.OnWaveCompleted += OnWaveCompleted;
            }

            // Start game
            if (pauseOnStart)
            {
                PauseGame();
            }
            else
            {
                StartGame();
            }
        }

        public void StartGame()
        {
            _isGameOver = false;
            _isPaused = false;
            _gameTime = 0f;
            _score = 0;
            _enemiesKilled = 0;
            Time.timeScale = 1f;

            OnGameStart?.Invoke();
        }

        public void PauseGame()
        {
            _isPaused = true;
            Time.timeScale = 0f;
            OnPauseChanged?.Invoke(true);
        }

        public void ResumeGame()
        {
            _isPaused = false;
            Time.timeScale = 1f;
            OnPauseChanged?.Invoke(false);
        }

        public void TogglePause()
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        public void GameOver()
        {
            if (_isGameOver) return;

            _isGameOver = true;
            Time.timeScale = 1f; // Reset time scale
            
            // Submit score to leaderboard if authenticated
            if (LeaderboardManager.Instance != null && AuthManager.Instance != null && AuthManager.Instance.IsAuthenticated)
            {
                LeaderboardManager.Instance.SubmitScore(
                    _score,
                    levelId: 1,
                    timeElapsed: _gameTime,
                    enemiesKilled: _enemiesKilled,
                    deaths: 1
                );
            }
            
            // Save high score locally
            if (SaveManager.Instance != null)
            {
                SaveManager.Instance.SaveHighScore(_score);
            }
            
            OnGameOver?.Invoke();
        }

        public void RestartGame()
        {
            // Clean up before restart
            Cleanup();
            
            // Reload scene or reset game state
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
            );
        }
        
        private void Cleanup()
        {
            // Stop any running coroutines
            if (_slowMotionCoroutine != null)
            {
                StopCoroutine(_slowMotionCoroutine);
                _slowMotionCoroutine = null;
            }
            
            // Clear all bullets
            if (Weapons.BulletManager.Instance != null)
            {
                Weapons.BulletManager.Instance.ClearAllBullets();
            }
            
            // Clear all enemies
            if (enemySpawner != null)
            {
                enemySpawner.ClearAllEnemies();
            }
            
            // Reset time scale
            Time.timeScale = 1f;
        }
        
        private void OnDestroy()
        {
            // Unsubscribe from events
            if (playerHealth != null)
            {
                playerHealth.OnDeath -= HandlePlayerDeath;
            }
            
            if (enemySpawner != null)
            {
                enemySpawner.OnWaveCompleted -= OnWaveCompleted;
            }
        }

        public void AddScore(int points)
        {
            _score += points;
            OnScoreChanged?.Invoke(_score);
        }

        public void TriggerSlowMotion()
        {
            TriggerSlowMotion(slowMotionScale, slowMotionDuration);
        }

        public void TriggerSlowMotion(float customScale, float customDuration)
        {
            // Stop any existing slow motion coroutine
            if (_slowMotionCoroutine != null)
            {
                StopCoroutine(_slowMotionCoroutine);
            }
            
            _slowMotionCoroutine = StartCoroutine(SlowMotionCoroutine(customScale, customDuration));
        }

        private System.Collections.IEnumerator SlowMotionCoroutine(float customScale, float customDuration)
        {
            Time.timeScale = customScale;
            yield return new WaitForSecondsRealtime(customDuration);
            Time.timeScale = 1f;
            _slowMotionCoroutine = null;
        }

        public void TriggerCameraShake(float intensity = 0.1f, float duration = 0.2f)
        {
            if (cameraController != null)
            {
                cameraController.Shake(intensity, duration);
            }
        }

        private void HandlePlayerDeath()
        {
            GameOver();
        }

        private void OnWaveCompleted(int waveNumber)
        {
            // Add score for completing wave
            AddScore(waveNumber * 100);
        }

        public void OnEnemyKilled()
        {
            _enemiesKilled++;
        }

        // Setters for editor/initialization
        public void SetPlayer(PlayerController p) => player = p;
        public void SetPlayerHealth(PlayerHealth h) => playerHealth = h;
        public void SetEnemySpawner(EnemySpawner s) => enemySpawner = s;
        public void SetCameraController(CameraController c) => cameraController = c;

        // Getters
        public bool IsPaused => _isPaused;
        public bool IsGameOver => _isGameOver;
        public float GameTime => _gameTime;
        public int Score => _score;
    }
}

