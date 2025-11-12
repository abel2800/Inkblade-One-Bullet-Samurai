using UnityEngine;
using Inkblade.Player;
using Inkblade.Systems;

namespace Inkblade.Utils
{
    /// <summary>
    /// Cheat codes and debug commands for development.
    /// </summary>
    public class CheatManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool enableCheats = true;

        private PlayerController _player;
        private PlayerHealth _playerHealth;
        private GameManager _gameManager;

        private void Awake()
        {
            // Only enable in development builds
#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
            enableCheats = false;
#endif
        }

        private void Start()
        {
            _player = FindObjectOfType<PlayerController>();
            _playerHealth = FindObjectOfType<PlayerHealth>();
            _gameManager = GameManager.Instance;
        }

        private void Update()
        {
            if (!enableCheats) return;

            // God mode (G key)
            if (Input.GetKeyDown(KeyCode.G))
            {
                ToggleGodMode();
            }

            // Infinite bullet (B key)
            if (Input.GetKeyDown(KeyCode.B))
            {
                ToggleInfiniteBullet();
            }

            // Add score (S key)
            if (Input.GetKeyDown(KeyCode.S))
            {
                AddScore(1000);
            }

            // Kill all enemies (K key)
            if (Input.GetKeyDown(KeyCode.K))
            {
                KillAllEnemies();
            }

            // Heal player (H key)
            if (Input.GetKeyDown(KeyCode.H))
            {
                HealPlayer();
            }
        }

        private void ToggleGodMode()
        {
            if (_playerHealth != null)
            {
                // Toggle invulnerability
                _playerHealth.SetInvulnerable(999999f);
                Debug.Log("God mode toggled");
            }
        }

        private void ToggleInfiniteBullet()
        {
            if (_player != null)
            {
                // Force bullet available
                _player.OnBulletRetrieved();
                Debug.Log("Infinite bullet toggled");
            }
        }

        private void AddScore(int points)
        {
            if (_gameManager != null)
            {
                _gameManager.AddScore(points);
                Debug.Log($"Added {points} points");
            }
        }

        private void KillAllEnemies()
        {
            var enemies = FindObjectsOfType<Enemies.EnemyAI>();
            foreach (var enemy in enemies)
            {
                var health = enemy.GetComponent<Enemies.EnemyHealth>();
                if (health != null)
                {
                    health.TakeDamage(9999);
                }
            }
            Debug.Log($"Killed {enemies.Length} enemies");
        }

        private void HealPlayer()
        {
            if (_playerHealth != null)
            {
                _playerHealth.Heal(999);
                Debug.Log("Player healed");
            }
        }
    }
}

