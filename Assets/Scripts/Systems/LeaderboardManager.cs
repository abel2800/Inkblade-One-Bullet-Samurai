using UnityEngine;
using System;
using System.Collections.Generic;

namespace Inkblade.Systems
{
    /// <summary>
    /// Manages leaderboard data and API communication.
    /// </summary>
    public class LeaderboardManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int leaderboardLimit = 10;

        private List<LeaderboardEntry> _currentLeaderboard = new List<LeaderboardEntry>();
        private int _userBestRank = -1;
        private int _userBestScore = 0;

        // Singleton
        public static LeaderboardManager Instance { get; private set; }

        // Events
        public System.Action<List<LeaderboardEntry>> OnLeaderboardUpdated;
        public System.Action<int, int> OnBestScoreUpdated; // rank, score
        public System.Action<string> OnSubmitError;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Submit a score to the leaderboard.
        /// </summary>
        public void SubmitScore(int score, int levelId = 1, float timeElapsed = 0f, int enemiesKilled = 0, int deaths = 0)
        {
            if (APIClient.Instance == null)
            {
                OnSubmitError?.Invoke("API client not initialized");
                return;
            }
            
            if (AuthManager.Instance == null || !AuthManager.Instance.IsAuthenticated)
            {
                OnSubmitError?.Invoke("Not authenticated");
                return;
            }

            var data = new ScoreRequest
            {
                score = score,
                levelId = levelId,
                timeElapsed = timeElapsed,
                enemiesKilled = enemiesKilled,
                deaths = deaths
            };

            APIClient.Instance.Post("score", data, (response) =>
            {
                try
                {
                    var result = JsonUtility.FromJson<ScoreResponse>(response);
                    _userBestRank = result.rank;
                    _userBestScore = result.score;
                    OnBestScoreUpdated?.Invoke(result.rank, result.score);

                    // Refresh leaderboard
                    FetchLeaderboard();
                }
                catch (Exception e)
                {
                    OnSubmitError?.Invoke($"Failed to parse response: {e.Message}");
                }
            }, (error) =>
            {
                OnSubmitError?.Invoke(error);
            });
        }

        /// <summary>
        /// Fetch leaderboard from API.
        /// </summary>
        public void FetchLeaderboard(int levelId = 0)
        {
            if (APIClient.Instance == null)
            {
                return;
            }

            string endpoint = levelId > 0 
                ? $"leaderboard?limit={leaderboardLimit}&levelId={levelId}"
                : $"leaderboard?limit={leaderboardLimit}";

            APIClient.Instance.Get(endpoint, (response) =>
            {
                try
                {
                    var result = JsonUtility.FromJson<LeaderboardResponse>(response);
                    _currentLeaderboard = result.leaderboard ?? new List<LeaderboardEntry>();
                    OnLeaderboardUpdated?.Invoke(_currentLeaderboard);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to parse leaderboard: {e.Message}");
                }
            });
        }

        /// <summary>
        /// Get user's best score.
        /// </summary>
        public void FetchBestScore(int levelId = 1)
        {
            if (APIClient.Instance == null)
            {
                Debug.LogWarning("LeaderboardManager: API client not initialized");
                return;
            }
            
            if (AuthManager.Instance == null || !AuthManager.Instance.IsAuthenticated)
            {
                Debug.LogWarning("LeaderboardManager: Not authenticated");
                return;
            }

            APIClient.Instance.Get($"score/best?levelId={levelId}", (response) =>
            {
                try
                {
                    var result = JsonUtility.FromJson<BestScoreResponse>(response);
                    _userBestRank = result.rank;
                    _userBestScore = result.score;
                    OnBestScoreUpdated?.Invoke(result.rank, result.score);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to parse best score: {e.Message}");
                }
            });
        }

        // Getters
        public List<LeaderboardEntry> CurrentLeaderboard => _currentLeaderboard;
        public int UserBestRank => _userBestRank;
        public int UserBestScore => _userBestScore;

        [Serializable]
        public class LeaderboardEntry
        {
            public int rank;
            public string username;
            public int score;
            public int levelId;
            public string createdAt;
        }

        [Serializable]
        private class ScoreRequest
        {
            public int score;
            public int levelId;
            public float timeElapsed;
            public int enemiesKilled;
            public int deaths;
        }

        [Serializable]
        private class ScoreResponse
        {
            public string id;
            public string userId;
            public int score;
            public int levelId;
            public int rank;
            public string createdAt;
        }

        [Serializable]
        private class BestScoreResponse
        {
            public int score;
            public int levelId;
            public int rank;
            public string createdAt;
        }

        [Serializable]
        private class LeaderboardResponse
        {
            public List<LeaderboardEntry> leaderboard;
            public int total;
            public int limit;
            public int offset;
        }
    }
}

