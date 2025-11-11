using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Inkblade.Systems;

namespace Inkblade.UI
{
    /// <summary>
    /// Handles game over menu UI and displays final stats.
    /// </summary>
    public class GameOverMenu : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI finalScoreText;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI timeSurvivedText;
        [SerializeField] private TextMeshProUGUI waveReachedText;

        [Header("Buttons")]
        [SerializeField] private Button retryButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button quitButton;

        private UIManager _uiManager;
        private GameManager _gameManager;
        private SaveManager _saveManager;

        private void Awake()
        {
            _uiManager = FindObjectOfType<UIManager>();
            _gameManager = GameManager.Instance;
            _saveManager = SaveManager.Instance;
        }

        private void Start()
        {
            SetupButtons();
        }

        private void OnEnable()
        {
            UpdateStats();
        }

        private void SetupButtons()
        {
            if (retryButton != null)
            {
                retryButton.onClick.AddListener(RetryGame);
            }

            if (mainMenuButton != null)
            {
                mainMenuButton.onClick.AddListener(GoToMainMenu);
            }

            if (quitButton != null)
            {
                quitButton.onClick.AddListener(QuitGame);
            }
        }

        private void UpdateStats()
        {
            if (_gameManager == null) return;

            int finalScore = _gameManager.Score;
            float timeSurvived = _gameManager.GameTime;

            // Update final score
            if (finalScoreText != null)
            {
                finalScoreText.text = $"Final Score: {finalScore}";
            }

            // Update high score
            if (_saveManager != null)
            {
                int highScore = _saveManager.GetHighScore();
                if (finalScore > highScore)
                {
                    _saveManager.SaveHighScore(finalScore);
                    highScore = finalScore;
                }

                if (highScoreText != null)
                {
                    highScoreText.text = $"High Score: {highScore}";
                }
            }

            // Update time survived
            if (timeSurvivedText != null)
            {
                int minutes = Mathf.FloorToInt(timeSurvived / 60f);
                int seconds = Mathf.FloorToInt(timeSurvived % 60f);
                timeSurvivedText.text = $"Time Survived: {minutes:00}:{seconds:00}";
            }

            // Update wave reached (if available)
            if (waveReachedText != null)
            {
                // TODO: Get wave number from EnemySpawner or GameManager
                waveReachedText.text = "Wave: --";
            }
        }

        private void RetryGame()
        {
            if (_uiManager != null)
            {
                _uiManager.RestartGame();
            }
            else if (_gameManager != null)
            {
                _gameManager.RestartGame();
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level_Play");
            }
        }

        private void GoToMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        private void QuitGame()
        {
            if (_uiManager != null)
            {
                _uiManager.QuitGame();
            }
            else
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
        }

        private void OnDestroy()
        {
            // Clean up button listeners
            if (retryButton != null)
            {
                retryButton.onClick.RemoveAllListeners();
            }

            if (mainMenuButton != null)
            {
                mainMenuButton.onClick.RemoveAllListeners();
            }

            if (quitButton != null)
            {
                quitButton.onClick.RemoveAllListeners();
            }
        }
    }
}

