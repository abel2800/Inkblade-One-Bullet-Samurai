using UnityEngine;
using UnityEngine.SceneManagement;

namespace Inkblade.Utils
{
    /// <summary>
    /// Utility for loading scenes with transitions.
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private bool useFade = true;

        private static SceneLoader _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Load scene by name.
        /// </summary>
        public static void LoadScene(string sceneName)
        {
            if (_instance != null && _instance.useFade)
            {
                _instance.StartCoroutine(_instance.LoadSceneWithFade(sceneName));
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
        }

        /// <summary>
        /// Load scene by build index.
        /// </summary>
        public static void LoadScene(int buildIndex)
        {
            if (_instance != null && _instance.useFade)
            {
                _instance.StartCoroutine(_instance.LoadSceneWithFade(buildIndex));
            }
            else
            {
                SceneManager.LoadScene(buildIndex);
            }
        }

        /// <summary>
        /// Reload current scene.
        /// </summary>
        public static void ReloadScene()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }

        private System.Collections.IEnumerator LoadSceneWithFade(string sceneName)
        {
            // Simple fade implementation - can be enhanced with UI image overlay
            yield return new WaitForSeconds(fadeDuration);
            SceneManager.LoadScene(sceneName);
        }

        private System.Collections.IEnumerator LoadSceneWithFade(int buildIndex)
        {
            // Simple fade implementation - can be enhanced with UI image overlay
            yield return new WaitForSeconds(fadeDuration);
            SceneManager.LoadScene(buildIndex);
        }
    }
}

