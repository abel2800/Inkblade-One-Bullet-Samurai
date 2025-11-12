using UnityEngine;
using TMPro;

namespace Inkblade.Utils
{
    /// <summary>
    /// Performance monitoring utility for development.
    /// </summary>
    public class PerformanceMonitor : MonoBehaviour
    {
        [Header("Display Settings")]
        [SerializeField] private bool showFPS = true;
        [SerializeField] private bool showMemory = false;
        [SerializeField] private float updateInterval = 0.5f;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI fpsText;
        [SerializeField] private TextMeshProUGUI memoryText;

        private float _deltaTime = 0f;
        private float _fps = 0f;
        private float _updateTimer = 0f;

        private void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
            _updateTimer += Time.unscaledDeltaTime;

            if (_updateTimer >= updateInterval)
            {
                UpdateDisplay();
                _updateTimer = 0f;
            }
        }

        private void UpdateDisplay()
        {
            _fps = 1.0f / _deltaTime;

            if (showFPS && fpsText != null)
            {
                fpsText.text = $"FPS: {Mathf.RoundToInt(_fps)}";
                
                // Color based on FPS
                if (_fps >= 60f)
                {
                    fpsText.color = Color.green;
                }
                else if (_fps >= 30f)
                {
                    fpsText.color = Color.yellow;
                }
                else
                {
                    fpsText.color = Color.red;
                }
            }

            if (showMemory && memoryText != null)
            {
                long memoryUsed = System.GC.GetTotalMemory(false) / 1024 / 1024; // MB
                memoryText.text = $"Memory: {memoryUsed} MB";
            }
        }

        /// <summary>
        /// Get current FPS.
        /// </summary>
        public float GetFPS()
        {
            return _fps;
        }

        /// <summary>
        /// Enable/disable FPS display.
        /// </summary>
        public void SetShowFPS(bool show)
        {
            showFPS = show;
            if (fpsText != null)
            {
                fpsText.gameObject.SetActive(show);
            }
        }

        /// <summary>
        /// Enable/disable memory display.
        /// </summary>
        public void SetShowMemory(bool show)
        {
            showMemory = show;
            if (memoryText != null)
            {
                memoryText.gameObject.SetActive(show);
            }
        }
    }
}

