using UnityEngine;
using UnityEngine.SceneManagement;
using Inkblade.Systems;

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
            // Fetch and display leaderboard
            if (LeaderboardManager.Instance != null)
            {
                LeaderboardManager.Instance.FetchLeaderboard();
                LeaderboardManager.Instance.OnLeaderboardUpdated += OnLeaderboardFetched;
                Debug.Log("Fetching leaderboard...");
            }
            else
            {
                Debug.LogWarning("LeaderboardManager not found. Leaderboard feature requires backend connection.");
            }
        }
        
        private void OnLeaderboardFetched(System.Collections.Generic.List<LeaderboardManager.LeaderboardEntry> entries)
        {
            LeaderboardManager.Instance.OnLeaderboardUpdated -= OnLeaderboardFetched;
            
            if (entries == null || entries.Count == 0)
            {
                Debug.Log("No leaderboard entries found.");
                return;
            }
            
            Debug.Log($"=== LEADERBOARD ({entries.Count} entries) ===");
            foreach (var entry in entries)
            {
                Debug.Log($"#{entry.rank}: {entry.username} - {entry.score} points");
            }
            Debug.Log("================================");
            
            // Note: In a full implementation, this would display in a UI panel
            // For now, we log to console. UI panel can be added later.
        }
        
        private void OnDestroy()
        {
            // Clean up event subscription
            if (LeaderboardManager.Instance != null)
            {
                LeaderboardManager.Instance.OnLeaderboardUpdated -= OnLeaderboardFetched;
            }
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

