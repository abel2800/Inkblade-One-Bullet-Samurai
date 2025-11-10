using UnityEngine;

namespace Inkblade.Player
{
    /// <summary>
    /// Handles player health, damage, and death.
    /// </summary>
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Health Settings")]
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private float invulnerabilityDuration = 1f;

        [Header("Death Settings")]
        [SerializeField] private float deathDelay = 1f;

        private int _currentHealth;
        private bool _isInvulnerable = false;
        private float _invulnerabilityTimer = 0f;

        // Events
        public System.Action<int, int> OnHealthChanged; // current, max
        public System.Action OnDeath;
        public System.Action OnDamageTaken;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        private void Update()
        {
            UpdateInvulnerability();
        }

        private void UpdateInvulnerability()
        {
            if (_isInvulnerable)
            {
                _invulnerabilityTimer -= Time.deltaTime;
                if (_invulnerabilityTimer <= 0f)
                {
                    _isInvulnerable = false;
                }
            }
        }

        public void TakeDamage(int damage)
        {
            if (_isInvulnerable || _currentHealth <= 0) return;

            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
            OnDamageTaken?.Invoke();

            if (_currentHealth <= 0)
            {
                Die();
            }
            else
            {
                // Grant temporary invulnerability
                SetInvulnerable(invulnerabilityDuration);
            }
        }

        public void Heal(int amount)
        {
            _currentHealth = Mathf.Min(maxHealth, _currentHealth + amount);
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
        }

        public void SetInvulnerable(float duration)
        {
            _isInvulnerable = true;
            _invulnerabilityTimer = duration;
        }

        private void Die()
        {
            OnDeath?.Invoke();
            // Death logic will be handled by GameManager
            Invoke(nameof(HandleDeath), deathDelay);
        }

        private void HandleDeath()
        {
            // This will be handled by GameManager
            // For now, just disable the player
            gameObject.SetActive(false);
        }

        public void Respawn()
        {
            _currentHealth = maxHealth;
            _isInvulnerable = false;
            _invulnerabilityTimer = 0f;
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
            gameObject.SetActive(true);
        }

        // Getters
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;
        public bool IsInvulnerable => _isInvulnerable;
        public bool IsDead => _currentHealth <= 0;
    }
}

