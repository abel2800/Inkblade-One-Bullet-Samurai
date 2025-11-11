using UnityEngine;
using System;

namespace Inkblade.Systems
{
    /// <summary>
    /// Manages user authentication and session.
    /// </summary>
    public class AuthManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool autoLogin = false;

        private string _currentToken;
        private string _currentUserId;
        private string _currentUsername;
        private bool _isAuthenticated = false;

        // Singleton
        public static AuthManager Instance { get; private set; }

        // Events
        public System.Action<string, string> OnLoginSuccess; // userId, username
        public System.Action OnLogout;
        public System.Action<string> OnLoginError;
        public System.Action<string> OnRegisterError;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                LoadToken();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (APIClient.Instance != null && !string.IsNullOrEmpty(_currentToken))
            {
                APIClient.Instance.SetAuthToken(_currentToken);
                _isAuthenticated = true;
            }
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        public void Register(string username, string email, string password, System.Action onSuccess = null)
        {
            if (APIClient.Instance == null)
            {
                OnRegisterError?.Invoke("API client not initialized");
                return;
            }

            var data = new RegisterRequest
            {
                username = username,
                email = email,
                password = password
            };

            APIClient.Instance.Post("auth/register", data, (response) =>
            {
                try
                {
                    var result = JsonUtility.FromJson<AuthResponse>(response);
                    HandleAuthSuccess(result);
                    onSuccess?.Invoke();
                }
                catch (Exception e)
                {
                    OnRegisterError?.Invoke($"Failed to parse response: {e.Message}");
                }
            }, (error) =>
            {
                OnRegisterError?.Invoke(error);
            });
        }

        /// <summary>
        /// Login user.
        /// </summary>
        public void Login(string email, string password, System.Action onSuccess = null)
        {
            if (APIClient.Instance == null)
            {
                OnLoginError?.Invoke("API client not initialized");
                return;
            }

            var data = new LoginRequest
            {
                email = email,
                password = password
            };

            APIClient.Instance.Post("auth/login", data, (response) =>
            {
                try
                {
                    var result = JsonUtility.FromJson<AuthResponse>(response);
                    HandleAuthSuccess(result);
                    onSuccess?.Invoke();
                }
                catch (Exception e)
                {
                    OnLoginError?.Invoke($"Failed to parse response: {e.Message}");
                }
            }, (error) =>
            {
                OnLoginError?.Invoke(error);
            });
        }

        /// <summary>
        /// Logout user.
        /// </summary>
        public void Logout()
        {
            _currentToken = null;
            _currentUserId = null;
            _currentUsername = null;
            _isAuthenticated = false;

            SaveToken();
            if (APIClient.Instance != null)
            {
                APIClient.Instance.SetAuthToken(null);
            }

            OnLogout?.Invoke();
        }

        private void HandleAuthSuccess(AuthResponse response)
        {
            _currentToken = response.token;
            _currentUserId = response.id;
            _currentUsername = response.username;
            _isAuthenticated = true;

            SaveToken();
            if (APIClient.Instance != null)
            {
                APIClient.Instance.SetAuthToken(_currentToken);
            }

            OnLoginSuccess?.Invoke(_currentUserId, _currentUsername);
        }

        private void SaveToken()
        {
            if (!string.IsNullOrEmpty(_currentToken))
            {
                PlayerPrefs.SetString("AuthToken", _currentToken);
                PlayerPrefs.SetString("UserId", _currentUserId ?? "");
                PlayerPrefs.SetString("Username", _currentUsername ?? "");
                PlayerPrefs.Save();
            }
            else
            {
                PlayerPrefs.DeleteKey("AuthToken");
                PlayerPrefs.DeleteKey("UserId");
                PlayerPrefs.DeleteKey("Username");
            }
        }

        private void LoadToken()
        {
            _currentToken = PlayerPrefs.GetString("AuthToken", "");
            _currentUserId = PlayerPrefs.GetString("UserId", "");
            _currentUsername = PlayerPrefs.GetString("Username", "");
            _isAuthenticated = !string.IsNullOrEmpty(_currentToken);
        }

        // Getters
        public bool IsAuthenticated => _isAuthenticated;
        public string CurrentUserId => _currentUserId;
        public string CurrentUsername => _currentUsername;
        public string CurrentToken => _currentToken;

        [Serializable]
        private class RegisterRequest
        {
            public string username;
            public string email;
            public string password;
        }

        [Serializable]
        private class LoginRequest
        {
            public string email;
            public string password;
        }

        [Serializable]
        private class AuthResponse
        {
            public string id;
            public string username;
            public string email;
            public string token;
        }
    }
}

