using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Inkblade.Systems
{
    /// <summary>
    /// HTTP client for backend API communication.
    /// </summary>
    public class APIClient : MonoBehaviour
    {
        [Header("API Configuration")]
        [SerializeField] private string baseURL = "http://localhost:3000/api";
        [SerializeField] private float timeout = 10f;

        private string _authToken;
        private Dictionary<string, string> _headers = new Dictionary<string, string>();

        // Singleton
        public static APIClient Instance { get; private set; }

        // Events
        public System.Action<string> OnError;
        public System.Action OnConnectionError;

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
        /// Set authentication token.
        /// </summary>
        public void SetAuthToken(string token)
        {
            _authToken = token;
            if (!string.IsNullOrEmpty(token))
            {
                _headers["Authorization"] = $"Bearer {token}";
            }
            else
            {
                _headers.Remove("Authorization");
            }
        }

        /// <summary>
        /// GET request.
        /// </summary>
        public void Get(string endpoint, System.Action<string> onSuccess, System.Action<string> onError = null)
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                onError?.Invoke("Endpoint cannot be null or empty");
                return;
            }
            
            StartCoroutine(RequestCoroutine("GET", endpoint, null, onSuccess, onError));
        }

        /// <summary>
        /// POST request.
        /// </summary>
        public void Post(string endpoint, object data, System.Action<string> onSuccess, System.Action<string> onError = null)
        {
            if (string.IsNullOrEmpty(endpoint))
            {
                onError?.Invoke("Endpoint cannot be null or empty");
                return;
            }
            
            StartCoroutine(RequestCoroutine("POST", endpoint, data, onSuccess, onError));
        }

        private IEnumerator RequestCoroutine(string method, string endpoint, object data, System.Action<string> onSuccess, System.Action<string> onError)
        {
            string url = $"{baseURL}/{endpoint.TrimStart('/')}";
            string jsonData = data != null ? JsonUtility.ToJson(data) : null;

            using (UnityEngine.Networking.UnityWebRequest request = CreateRequest(method, url, jsonData))
            {
                request.timeout = (int)timeout;
                yield return request.SendWebRequest();

                if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                {
                    onSuccess?.Invoke(request.downloadHandler.text);
                }
                else
                {
                    string error = request.error;
                    if (!string.IsNullOrEmpty(request.downloadHandler.text))
                    {
                        try
                        {
                            var errorData = JsonUtility.FromJson<ErrorResponse>(request.downloadHandler.text);
                            error = errorData.error ?? error;
                        }
                        catch { }
                    }

                    onError?.Invoke(error);
                    OnError?.Invoke(error);

                    if (request.result == UnityEngine.Networking.UnityWebRequest.Result.ConnectionError)
                    {
                        OnConnectionError?.Invoke();
                    }
                }
            }
        }

        private UnityEngine.Networking.UnityWebRequest CreateRequest(string method, string url, string jsonData)
        {
            UnityEngine.Networking.UnityWebRequest request;

            if (method == "GET")
            {
                request = UnityEngine.Networking.UnityWebRequest.Get(url);
            }
            else if (method == "POST")
            {
                request = new UnityEngine.Networking.UnityWebRequest(url, "POST");
                byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData ?? "{}");
                request.uploadHandler = new UnityEngine.Networking.UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
            }
            else
            {
                throw new ArgumentException($"Unsupported HTTP method: {method}");
            }

            // Add headers
            foreach (var header in _headers)
            {
                request.SetRequestHeader(header.Key, header.Value);
            }

            return request;
        }

        [Serializable]
        private class ErrorResponse
        {
            public string error;
            public string message;
        }
    }
}

