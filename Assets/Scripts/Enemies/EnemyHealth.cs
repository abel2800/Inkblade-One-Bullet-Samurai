using UnityEngine;

namespace Inkblade.Enemies
{
    /// <summary>
    /// Handles enemy health and death.
    /// </summary>
    public class EnemyHealth : MonoBehaviour
    {
        [Header("Health Settings")]
        [SerializeField] private int maxHealth = 50;
        [SerializeField] private float deathDelay = 0.5f;

        private int _currentHealth;

        // Events
        public System.Action<int, int> OnHealthChanged; // current, max
        public System.Action OnDeath;
        public System.Action OnDamageTaken;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (_currentHealth <= 0) return;

            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
            OnDamageTaken?.Invoke();

            // Spawn hit effect
            if (Systems.ParticleManager.Instance != null)
            {
                Systems.ParticleManager.Instance.SpawnHitEffect(transform.position);
            }

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            _currentHealth = Mathf.Min(maxHealth, _currentHealth + amount);
            OnHealthChanged?.Invoke(_currentHealth, maxHealth);
        }

        private void Die()
        {
            OnDeath?.Invoke();
            
            // Spawn death particle effect
            if (Systems.ParticleManager.Instance != null)
            {
                Systems.ParticleManager.Instance.SpawnDeathEffect(transform.position);
            }

            Invoke(nameof(HandleDeath), deathDelay);
        }

        private void HandleDeath()
        {
            // Death logic handled by EnemyAI
            // This component just triggers the event
        }

        // Getters
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => maxHealth;
        public bool IsDead => _currentHealth <= 0;
    }
}

