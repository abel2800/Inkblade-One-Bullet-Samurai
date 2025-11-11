using UnityEngine;

namespace Inkblade.Utils
{
    /// <summary>
    /// Global game events system for decoupled communication.
    /// </summary>
    public static class GameEvents
    {
        // Player Events
        public static System.Action OnPlayerSpawned;
        public static System.Action OnPlayerDeath;
        public static System.Action OnPlayerDash;
        public static System.Action OnPlayerShoot;
        public static System.Action OnPlayerRetrieveBullet;

        // Enemy Events
        public static System.Action<GameObject> OnEnemySpawned;
        public static System.Action<GameObject> OnEnemyDeath;
        public static System.Action<GameObject> OnEnemyHit;

        // Bullet Events
        public static System.Action<GameObject> OnBulletShot;
        public static System.Action<GameObject> OnBulletStuck;
        public static System.Action<GameObject> OnBulletRetrieved;

        // Game Events
        public static System.Action OnGameStart;
        public static System.Action OnGamePause;
        public static System.Action OnGameResume;
        public static System.Action OnGameOver;
        public static System.Action<int> OnWaveStart;
        public static System.Action<int> OnWaveComplete;
        public static System.Action<int> OnScoreChanged;

        // Audio Events
        public static System.Action<string> OnPlaySFX;
        public static System.Action<string> OnPlayMusic;

        /// <summary>
        /// Clear all event subscriptions (useful for scene transitions).
        /// </summary>
        public static void ClearAll()
        {
            OnPlayerSpawned = null;
            OnPlayerDeath = null;
            OnPlayerDash = null;
            OnPlayerShoot = null;
            OnPlayerRetrieveBullet = null;

            OnEnemySpawned = null;
            OnEnemyDeath = null;
            OnEnemyHit = null;

            OnBulletShot = null;
            OnBulletStuck = null;
            OnBulletRetrieved = null;

            OnGameStart = null;
            OnGamePause = null;
            OnGameResume = null;
            OnGameOver = null;
            OnWaveStart = null;
            OnWaveComplete = null;
            OnScoreChanged = null;

            OnPlaySFX = null;
            OnPlayMusic = null;
        }
    }
}

