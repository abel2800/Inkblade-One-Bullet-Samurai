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

        [Header("References")]
        [SerializeField] private PlayerController player;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private CameraController cameraController;

        private bool _isPaused = false;
        private bool _isGameOver = false;
        private float _gameTime = 0f;
        private int _score = 0;

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
            OnGameOver?.Invoke();
        }

        public void RestartGame()
        {
            // Reload scene or reset game state
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
            );
        }

        public void AddScore(int points)
        {
            _score += points;
            OnScoreChanged?.Invoke(_score);
        }

        public void TriggerSlowMotion()
        {
            StartCoroutine(SlowMotionCoroutine());
        }

        public void TriggerSlowMotion(float customScale, float customDuration)
        {
            StartCoroutine(SlowMotionCoroutine(customScale, customDuration));
        }

        private System.Collections.IEnumerator SlowMotionCoroutine()
        {
            Time.timeScale = slowMotionScale;
            yield return new WaitForSecondsRealtime(slowMotionDuration);
            Time.timeScale = 1f;
        }

        private System.Collections.IEnumerator SlowMotionCoroutine(float customScale, float customDuration)
        {
            Time.timeScale = customScale;
            yield return new WaitForSecondsRealtime(customDuration);
            Time.timeScale = 1f;
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

        // Getters
        public bool IsPaused => _isPaused;
        public bool IsGameOver => _isGameOver;
        public float GameTime => _gameTime;
        public int Score => _score;
    }
}

