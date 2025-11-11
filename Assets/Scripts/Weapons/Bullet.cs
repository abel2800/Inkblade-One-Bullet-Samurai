using UnityEngine;

namespace Inkblade.Weapons
{
    /// <summary>
    /// Bullet behavior: shooting, sticking to surfaces/enemies, and retrieval.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        [Header("Bullet Settings")]
        [SerializeField] private float speed = 15f;
        [SerializeField] private float lifetime = 10f;
        [SerializeField] private int damage = 10;

        [Header("Stick Settings")]
        [SerializeField] private LayerMask stickableLayers;
        [SerializeField] private bool stickToEnemies = true;
        [SerializeField] private bool stickToWalls = true;

        [Header("Visual")]
        [SerializeField] private GameObject impactEffect;

        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        private Vector2 _direction;
        private bool _isStuck = false;
        private bool _isRetrievable = false;
        private float _lifetimeTimer = 0f;

        // Events
        public System.Action<Bullet> OnStuck;
        public System.Action<Bullet> OnRetrieved;
        public System.Action<GameObject, int> OnEnemyHit; // enemy, damage

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (!_isStuck)
            {
                UpdateLifetime();
            }
        }

        private void FixedUpdate()
        {
            if (!_isStuck && _rigidbody != null)
            {
                _rigidbody.velocity = _direction * speed;
            }
        }

        private void UpdateLifetime()
        {
            _lifetimeTimer += Time.deltaTime;
            if (_lifetimeTimer >= lifetime)
            {
                DestroyBullet();
            }
        }

        public void Shoot(Vector2 direction, Vector2 position)
        {
            transform.position = position;
            _direction = direction.normalized;
            _isStuck = false;
            _isRetrievable = false;
            _lifetimeTimer = 0f;

            // Enable physics
            if (_rigidbody != null)
            {
                _rigidbody.velocity = _direction * speed;
                _rigidbody.isKinematic = false;
            }

            if (_collider != null)
            {
                _collider.enabled = true;
            }

            // Rotate bullet to face direction
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isStuck) return;

            // Check if hit enemy
            var enemy = other.GetComponent<Enemies.EnemyAI>();
            if (enemy != null && stickToEnemies)
            {
                HitEnemy(enemy.gameObject);
                StickToObject(other.transform);
                return;
            }

            // Check if hit wall/ground
            if (stickToWalls && IsStickableLayer(other.gameObject.layer))
            {
                StickToObject(other.transform);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_isStuck) return;

            // Check if hit enemy
            var enemy = collision.gameObject.GetComponent<Enemies.EnemyAI>();
            if (enemy != null && stickToEnemies)
            {
                HitEnemy(enemy.gameObject);
                StickToObject(collision.transform);
                return;
            }

            // Check if hit wall/ground
            if (stickToWalls && IsStickableLayer(collision.gameObject.layer))
            {
                StickToObject(collision.transform);
            }
        }

        private void HitEnemy(GameObject enemy)
        {
            // Deal damage to enemy
            var enemyHealth = enemy.GetComponent<Enemies.EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            OnEnemyHit?.Invoke(enemy, damage);
        }

        private void StickToObject(Transform target)
        {
            _isStuck = true;
            _isRetrievable = true;

            // Stop physics
            if (_rigidbody != null)
            {
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.isKinematic = true;
            }

            // Parent to target if it's an enemy, otherwise just stop
            if (target != null)
            {
                transform.SetParent(target);
            }

            OnStuck?.Invoke(this);

            // Spawn impact effect
            if (Systems.ParticleManager.Instance != null)
            {
                Systems.ParticleManager.Instance.SpawnImpactEffect(transform.position);
            }
            else if (impactEffect != null)
            {
                Instantiate(impactEffect, transform.position, Quaternion.identity);
            }
        }

        public void Retrieve()
        {
            OnRetrieved?.Invoke(this);
            DestroyBullet();
        }

        private void DestroyBullet()
        {
            // Return to pool or destroy
            // For now, just destroy
            Destroy(gameObject);
        }

        private bool IsStickableLayer(int layer)
        {
            return (stickableLayers.value & (1 << layer)) != 0;
        }

        // Getters
        public bool IsStuck => _isStuck;
        public bool IsRetrievable => _isRetrievable;
        public int Damage => damage;
    }
}

