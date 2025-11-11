using UnityEngine;
using UnityEngine.SceneManagement;

namespace Inkblade.UI
{
    /// <summary>
    /// Handles main menu button interactions.
    /// </summary>
    public class MainMenuButton : MonoBehaviour
    {
        [Header("Button Type")]
        [SerializeField] private ButtonType buttonType;

        private UIManager _uiManager;

        private enum ButtonType
        {
            Play,
            Settings,
            Quit,
            Leaderboard
        }

        private void Awake()
        {
            _uiManager = FindObjectOfType<UIManager>();
        }

        public void OnButtonClick()
        {
            switch (buttonType)
            {
                case ButtonType.Play:
                    PlayGame();
                    break;
                case ButtonType.Settings:
                    ShowSettings();
                    break;
                case ButtonType.Quit:
                    QuitGame();
                    break;
                case ButtonType.Leaderboard:
                    ShowLeaderboard();
                    break;
            }
        }

        private void PlayGame()
        {
            if (_uiManager != null)
            {
                _uiManager.StartGame();
            }
            else
            {
                // Fallback: Load game scene directly
                SceneManager.LoadScene("Level_Play");
            }
        }

        private void ShowSettings()
        {
            if (_uiManager != null)
            {
                _uiManager.ShowSettings();
            }
        }

        private void ShowLeaderboard()
        {
            // TODO: Implement leaderboard display
            Debug.Log("Leaderboard not yet implemented");
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
    }
}

