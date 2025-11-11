using UnityEngine;
using Inkblade.Systems;

namespace Inkblade.UI
{
    /// <summary>
    /// Handles pause menu UI and interactions.
    /// </summary>
    public class PauseMenu : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private UnityEngine.UI.Button resumeButton;
        [SerializeField] private UnityEngine.UI.Button settingsButton;
        [SerializeField] private UnityEngine.UI.Button mainMenuButton;
        [SerializeField] private UnityEngine.UI.Button quitButton;

        private UIManager _uiManager;
        private GameManager _gameManager;

        private void Awake()
        {
            _uiManager = FindObjectOfType<UIManager>();
            _gameManager = GameManager.Instance;
        }

        private void Start()
        {
            SetupButtons();
        }

        private void SetupButtons()
        {
            if (resumeButton != null)
            {
                resumeButton.onClick.AddListener(ResumeGame);
            }

            if (settingsButton != null)
            {
                settingsButton.onClick.AddListener(ShowSettings);
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

        private void ResumeGame()
        {
            if (_gameManager != null)
            {
                _gameManager.ResumeGame();
            }

            if (_uiManager != null)
            {
                _uiManager.HidePauseMenu();
            }
        }

        private void ShowSettings()
        {
            if (_uiManager != null)
            {
                _uiManager.ShowSettings();
            }
        }

        private void GoToMainMenu()
        {
            if (_gameManager != null)
            {
                _gameManager.ResumeGame(); // Unpause before leaving
            }

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

        private void OnEnable()
        {
            // Pause game when menu is shown
            if (_gameManager != null && !_gameManager.IsPaused)
            {
                _gameManager.PauseGame();
            }
        }

        private void OnDestroy()
        {
            // Clean up button listeners
            if (resumeButton != null)
            {
                resumeButton.onClick.RemoveAllListeners();
            }

            if (settingsButton != null)
            {
                settingsButton.onClick.RemoveAllListeners();
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

