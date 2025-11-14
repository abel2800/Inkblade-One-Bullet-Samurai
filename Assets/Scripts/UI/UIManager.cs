using UnityEngine;
using Inkblade.Systems;

namespace Inkblade.UI
{
    /// <summary>
    /// Main UI manager coordinating all UI panels.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("UI Panels")]
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject gameHUD;
        [SerializeField] private GameObject pauseMenuPanel;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject settingsPanel;

        [Header("References")]
        [SerializeField] private HUD hud;

        private GameManager _gameManager;

        // Events
        public System.Action OnMainMenu;
        public System.Action OnGameStart;
        public System.Action OnPause;
        public System.Action OnResume;
        public System.Action OnRestart;
        public System.Action OnSettings;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
            if (_gameManager != null)
            {
                _gameManager.OnPauseChanged += HandlePauseChanged;
                _gameManager.OnGameOver += HandleGameOver;
                _gameManager.OnGameStart += HandleGameStart;
            }
        }

        private void Start()
        {
            ShowMainMenu();
        }

        public void ShowMainMenu()
        {
            SetPanelActive(mainMenuPanel, true);
            SetPanelActive(gameHUD, false);
            SetPanelActive(pauseMenuPanel, false);
            SetPanelActive(gameOverPanel, false);
            SetPanelActive(settingsPanel, false);
            OnMainMenu?.Invoke();
        }

        public void StartGame()
        {
            SetPanelActive(mainMenuPanel, false);
            SetPanelActive(gameHUD, true);
            SetPanelActive(pauseMenuPanel, false);
            SetPanelActive(gameOverPanel, false);
            SetPanelActive(settingsPanel, false);

            if (_gameManager != null)
            {
                _gameManager.StartGame();
            }

            OnGameStart?.Invoke();
        }

        public void ShowPauseMenu()
        {
            SetPanelActive(pauseMenuPanel, true);
            OnPause?.Invoke();
        }

        public void HidePauseMenu()
        {
            SetPanelActive(pauseMenuPanel, false);
            OnResume?.Invoke();
        }

        public void ShowGameOver()
        {
            SetPanelActive(gameHUD, false);
            SetPanelActive(gameOverPanel, true);
        }

        public void ShowSettings()
        {
            SetPanelActive(settingsPanel, true);
            OnSettings?.Invoke();
        }

        public void HideSettings()
        {
            SetPanelActive(settingsPanel, false);
        }

        public void RestartGame()
        {
            if (_gameManager != null)
            {
                _gameManager.RestartGame();
            }
            OnRestart?.Invoke();
        }

        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void HandlePauseChanged(bool isPaused)
        {
            if (isPaused)
            {
                ShowPauseMenu();
            }
            else
            {
                HidePauseMenu();
            }
        }

        private void HandleGameOver()
        {
            ShowGameOver();
        }

        private void HandleGameStart()
        {
            SetPanelActive(gameHUD, true);
        }

        private void SetPanelActive(GameObject panel, bool active)
        {
            if (panel != null)
            {
                panel.SetActive(active);
            }
        }

        private void OnDestroy()
        {
            // Unsubscribe from events
            if (_gameManager != null)
            {
                _gameManager.OnPauseChanged -= HandlePauseChanged;
                _gameManager.OnGameOver -= HandleGameOver;
                _gameManager.OnGameStart -= HandleGameStart;
            }
        }

        // Getters
        public HUD HUD => hud;
    }
}

