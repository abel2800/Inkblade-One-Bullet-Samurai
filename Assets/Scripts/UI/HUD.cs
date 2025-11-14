using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Inkblade.Player;
using Inkblade.Systems;
using Inkblade.Enemies;

namespace Inkblade.UI
{
    /// <summary>
    /// In-game HUD displaying health, bullet status, score, etc.
    /// </summary>
    public class HUD : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private Slider healthBar;
        [SerializeField] private TextMeshProUGUI healthText;

        [Header("Bullet")]
        [SerializeField] private Image bulletIcon;
        [SerializeField] private TextMeshProUGUI bulletStatusText;
        [SerializeField] private Color bulletAvailableColor = Color.white;
        [SerializeField] private Color bulletUnavailableColor = Color.gray;

        [Header("Dash")]
        [SerializeField] private Image dashCooldownIcon;
        [SerializeField] private Image dashCooldownFill;
        [SerializeField] private TextMeshProUGUI dashCooldownText;

        [Header("Score")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private TextMeshProUGUI timeText;

        [Header("References")]
        [SerializeField] private PlayerController player;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private EnemySpawner enemySpawner;

        private void Awake()
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

            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }

            if (enemySpawner == null)
            {
                enemySpawner = FindObjectOfType<EnemySpawner>();
            }
        }

        private void Start()
        {
            SubscribeToEvents();
            UpdateAll();
        }

        private void SubscribeToEvents()
        {
            if (player != null)
            {
                player.OnBulletStateChanged += UpdateBulletStatus;
                player.OnDashCooldownChanged += UpdateDashCooldown;
            }

            if (playerHealth != null)
            {
                playerHealth.OnHealthChanged += UpdateHealth;
            }

            if (gameManager != null)
            {
                gameManager.OnScoreChanged += UpdateScore;
                gameManager.OnGameTimeChanged += UpdateTime;
            }

            if (enemySpawner != null)
            {
                enemySpawner.OnWaveStarted += UpdateWave;
            }
        }

        private void Update()
        {
            // Update time every frame
            if (gameManager != null)
            {
                UpdateTime(gameManager.GameTime);
            }
        }

        private void UpdateHealth(int current, int max)
        {
            if (healthBar != null)
            {
                healthBar.value = (float)current / max;
            }

            if (healthText != null)
            {
                healthText.text = $"{current} / {max}";
            }
        }

        private void UpdateBulletStatus(bool hasBullet)
        {
            if (bulletIcon != null)
            {
                bulletIcon.color = hasBullet ? bulletAvailableColor : bulletUnavailableColor;
            }

            if (bulletStatusText != null)
            {
                bulletStatusText.text = hasBullet ? "READY" : "RETRIEVE";
            }
        }

        private void UpdateDashCooldown(float progress)
        {
            if (dashCooldownFill != null)
            {
                dashCooldownFill.fillAmount = 1f - progress;
            }

            if (dashCooldownIcon != null)
            {
                dashCooldownIcon.color = progress > 0f ? Color.gray : Color.white;
            }

            if (dashCooldownText != null)
            {
                if (progress > 0f)
                {
                    dashCooldownText.text = $"{progress:F1}s";
                }
                else
                {
                    dashCooldownText.text = "READY";
                }
            }
        }

        private void UpdateScore(int score)
        {
            if (scoreText != null)
            {
                scoreText.text = $"Score: {score}";
            }
        }

        private void UpdateTime(float time)
        {
            if (timeText != null)
            {
                int minutes = Mathf.FloorToInt(time / 60f);
                int seconds = Mathf.FloorToInt(time % 60f);
                timeText.text = $"{minutes:00}:{seconds:00}";
            }
        }

        public void UpdateWave(int waveNumber)
        {
            if (waveText != null)
            {
                waveText.text = $"Wave: {waveNumber}";
            }
        }

        private void UpdateAll()
        {
            if (playerHealth != null)
            {
                UpdateHealth(playerHealth.CurrentHealth, playerHealth.MaxHealth);
            }

            if (player != null)
            {
                UpdateBulletStatus(player.HasBullet);
                UpdateDashCooldown(player.DashCooldownProgress);
            }

            if (gameManager != null)
            {
                UpdateScore(gameManager.Score);
                UpdateTime(gameManager.GameTime);
            }

            if (enemySpawner != null)
            {
                UpdateWave(enemySpawner.CurrentWave);
            }
        }

        private void OnDestroy()
        {
            if (player != null)
            {
                player.OnBulletStateChanged -= UpdateBulletStatus;
                player.OnDashCooldownChanged -= UpdateDashCooldown;
            }

            if (playerHealth != null)
            {
                playerHealth.OnHealthChanged -= UpdateHealth;
            }

            if (gameManager != null)
            {
                gameManager.OnScoreChanged -= UpdateScore;
                gameManager.OnGameTimeChanged -= UpdateTime;
            }

            if (enemySpawner != null)
            {
                enemySpawner.OnWaveStarted -= UpdateWave;
            }
        }
    }
}

