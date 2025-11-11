using UnityEngine;

namespace Inkblade.Enemies
{
    /// <summary>
    /// Enemy AI with state machine: Idle -> Pursue -> Attack -> Stagger -> Die
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class EnemyAI : MonoBehaviour
    {
        [Header("AI Settings")]
        [SerializeField] private float detectionRange = 5f;
        [SerializeField] private float attackRange = 1.5f;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private int attackDamage = 10;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float acceleration = 5f;
        [SerializeField] private float stoppingDistance = 1f;

        [Header("Stagger Settings")]
        [SerializeField] private float staggerDuration = 0.5f;

        [Header("References")]
        [SerializeField] private LayerMask playerLayer;

        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        private Transform _player;
        private EnemyHealth _health;

        // State
        private EnemyState _currentState = EnemyState.Idle;
        private float _attackCooldownTimer = 0f;
        private float _staggerTimer = 0f;
        private Vector2 _currentVelocity;

        // Events
        public System.Action<EnemyState> OnStateChanged;
        public System.Action OnPlayerDetected;
        public System.Action OnAttack;
        public System.Action OnDeath;

        private enum EnemyState
        {
            Idle,
            Pursue,
            Attack,
            Stagger,
            Dead
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _health = GetComponent<EnemyHealth>();

            if (_health != null)
            {
                _health.OnDeath += HandleDeath;
                _health.OnDamageTaken += HandleDamageTaken;
            }
        }

        private void Start()
        {
            FindPlayer();
        }

        private void Update()
        {
            UpdateState();
            UpdateTimers();
        }

        private void FixedUpdate()
        {
            if (_currentState == EnemyState.Pursue)
            {
                MoveTowardsPlayer();
            }
        }

        private void UpdateState()
        {
            if (_currentState == EnemyState.Dead) return;

            float distanceToPlayer = GetDistanceToPlayer();

            switch (_currentState)
            {
                case EnemyState.Idle:
                    if (distanceToPlayer <= detectionRange && _player != null)
                    {
                        ChangeState(EnemyState.Pursue);
                        OnPlayerDetected?.Invoke();
                    }
                    break;

                case EnemyState.Pursue:
                    if (distanceToPlayer <= attackRange)
                    {
                        ChangeState(EnemyState.Attack);
                    }
                    else if (distanceToPlayer > detectionRange * 1.5f)
                    {
                        ChangeState(EnemyState.Idle);
                    }
                    break;

                case EnemyState.Attack:
                    if (distanceToPlayer > attackRange * 1.2f)
                    {
                        ChangeState(EnemyState.Pursue);
                    }
                    else if (_attackCooldownTimer <= 0f)
                    {
                        PerformAttack();
                    }
                    break;

                case EnemyState.Stagger:
                    if (_staggerTimer <= 0f)
                    {
                        ChangeState(EnemyState.Pursue);
                    }
                    break;
            }
        }

        private void UpdateTimers()
        {
            if (_attackCooldownTimer > 0f)
            {
                _attackCooldownTimer -= Time.deltaTime;
            }

            if (_staggerTimer > 0f)
            {
                _staggerTimer -= Time.deltaTime;
            }
        }

        private void MoveTowardsPlayer()
        {
            if (_player == null) return;

            Vector2 direction = (_player.position - transform.position).normalized;
            float distance = Vector2.Distance(transform.position, _player.position);

            if (distance > stoppingDistance)
            {
                Vector2 targetVelocity = direction * moveSpeed;
                _currentVelocity = Vector2.MoveTowards(_currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
                _rigidbody.velocity = _currentVelocity;
            }
            else
            {
                _rigidbody.velocity = Vector2.zero;
            }

            // Face player
            if (direction.x != 0f)
            {
                transform.localScale = new Vector3(direction.x > 0 ? 1 : -1, 1, 1);
            }
        }

        private void PerformAttack()
        {
            if (_player == null) return;

            OnAttack?.Invoke();

            // Deal damage to player
            var playerHealth = _player.GetComponent<Player.PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }

            _attackCooldownTimer = attackCooldown;
        }

        private void ChangeState(EnemyState newState)
        {
            if (_currentState == newState) return;

            _currentState = newState;
            OnStateChanged?.Invoke(newState);

            // State-specific behavior
            switch (newState)
            {
                case EnemyState.Idle:
                    _rigidbody.velocity = Vector2.zero;
                    break;

                case EnemyState.Stagger:
                    _rigidbody.velocity = Vector2.zero;
                    _staggerTimer = staggerDuration;
                    break;

                case EnemyState.Dead:
                    _rigidbody.velocity = Vector2.zero;
                    _collider.enabled = false;
                    break;
            }
        }

        private void HandleDamageTaken()
        {
            if (_currentState != EnemyState.Dead && _currentState != EnemyState.Stagger)
            {
                ChangeState(EnemyState.Stagger);
            }
        }

        private void HandleDeath()
        {
            ChangeState(EnemyState.Dead);
            OnDeath?.Invoke();

            // Trigger slow motion and camera shake on kill
            if (Systems.GameManager.Instance != null)
            {
                Systems.GameManager.Instance.TriggerSlowMotion(0.2f, 0.15f);
                Systems.GameManager.Instance.TriggerCameraShake(0.15f, 0.2f);
            }

            Destroy(gameObject, 2f); // Destroy after death animation
        }

        private void FindPlayer()
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                _player = playerObj.transform;
            }
        }

        private float GetDistanceToPlayer()
        {
            if (_player == null) return float.MaxValue;
            return Vector2.Distance(transform.position, _player.position);
        }

        // Getters
        public EnemyState CurrentState => _currentState;
        public bool IsDead => _currentState == EnemyState.Dead;
        public float DistanceToPlayer => GetDistanceToPlayer();

        private void OnDrawGizmosSelected()
        {
            // Draw detection range
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCircle(transform.position, detectionRange);

            // Draw attack range
            Gizmos.color = Color.red;
            Gizmos.DrawWireCircle(transform.position, attackRange);
        }
    }
}

