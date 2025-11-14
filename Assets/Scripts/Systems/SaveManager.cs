using UnityEngine;
using Inkblade.Utils;

namespace Inkblade.Systems
{
    /// <summary>
    /// Handles saving and loading game data.
    /// </summary>
    public class SaveManager : MonoBehaviour
    {
        private const string SETTINGS_KEY_PREFIX = "Settings_";

        // Singleton
        public static SaveManager Instance { get; private set; }

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

        // High Score
        public void SaveHighScore(int score)
        {
            int currentHighScore = GetHighScore();
            if (score > currentHighScore)
            {
                PlayerPrefs.SetInt(Constants.PREF_HIGH_SCORE, score);
                PlayerPrefs.Save();
            }
        }

        public int GetHighScore()
        {
            return PlayerPrefs.GetInt(Constants.PREF_HIGH_SCORE, 0);
        }

        // Level Progress
        public void SaveLevelProgress(int level)
        {
            int currentProgress = GetLevelProgress();
            if (level > currentProgress)
            {
                PlayerPrefs.SetInt(Constants.PREF_LEVEL_PROGRESS, level);
                PlayerPrefs.Save();
            }
        }

        public int GetLevelProgress()
        {
            return PlayerPrefs.GetInt(Constants.PREF_LEVEL_PROGRESS, 0);
        }

        // Settings
        public void SaveSetting(string key, float value)
        {
            PlayerPrefs.SetFloat(SETTINGS_KEY_PREFIX + key, value);
            PlayerPrefs.Save();
        }

        public void SaveSetting(string key, int value)
        {
            PlayerPrefs.SetInt(SETTINGS_KEY_PREFIX + key, value);
            PlayerPrefs.Save();
        }

        public void SaveSetting(string key, string value)
        {
            PlayerPrefs.SetString(SETTINGS_KEY_PREFIX + key, value);
            PlayerPrefs.Save();
        }

        public float GetSettingFloat(string key, float defaultValue = 0f)
        {
            return PlayerPrefs.GetFloat(SETTINGS_KEY_PREFIX + key, defaultValue);
        }

        public int GetSettingInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(SETTINGS_KEY_PREFIX + key, defaultValue);
        }

        public string GetSettingString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(SETTINGS_KEY_PREFIX + key, defaultValue);
        }

        // Clear all data
        public void ClearAllData()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        // Clear specific data
        public void ClearHighScore()
        {
            PlayerPrefs.DeleteKey(Constants.PREF_HIGH_SCORE);
            PlayerPrefs.Save();
        }

        public void ClearLevelProgress()
        {
            PlayerPrefs.DeleteKey(Constants.PREF_LEVEL_PROGRESS);
            PlayerPrefs.Save();
        }
    }
}

