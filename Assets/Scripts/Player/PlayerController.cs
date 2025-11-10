using UnityEngine;

namespace Inkblade.Player
{
    /// <summary>
    /// Main player controller handling movement, dash, and bullet interaction.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float acceleration = 10f;
        [SerializeField] private float deceleration = 10f;

        [Header("Dash Settings")]
        [SerializeField] private float dashDistance = 3f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private float dashCooldown = 1f;
        [SerializeField] private bool dashInvulnerable = true;

        [Header("Bullet Interaction")]
        [SerializeField] private float bulletRetrieveRange = 1.5f;
        [SerializeField] private KeyCode retrieveKey = KeyCode.E;

        [Header("References")]
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private LayerMask bulletLayer;

        // Components
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;

        // Movement
        private Vector2 _moveInput;
        private Vector2 _currentVelocity;

        // Dash
        private bool _canDash = true;
        private bool _isDashing = false;
        private float _dashCooldownTimer = 0f;
        private bool _isInvulnerable = false;

        // Bullet
        private bool _hasBullet = true;
        private GameObject _activeBullet;

        // Events
        public System.Action<bool> OnBulletStateChanged;
        public System.Action OnDashUsed;
        public System.Action<float> OnDashCooldownChanged;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();

            if (bulletSpawnPoint == null)
            {
                bulletSpawnPoint = transform;
            }
        }

        private void Update()
        {
            HandleInput();
            UpdateDashCooldown();
        }

        private void FixedUpdate()
        {
            if (!_isDashing)
            {
                HandleMovement();
            }
        }

        private void HandleInput()
        {
            // Movement input
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");
            _moveInput.Normalize();

            // Dash input
            if (Input.GetKeyDown(KeyCode.Space) && _canDash && !_isDashing)
            {
                StartDash();
            }

            // Bullet retrieval
            if (Input.GetKeyDown(retrieveKey) && !_hasBullet)
            {
                TryRetrieveBullet();
            }

            // Shooting (will be handled by BulletManager)
            if (Input.GetMouseButtonDown(0) && _hasBullet)
            {
                ShootBullet();
            }
        }

        private void HandleMovement()
        {
            Vector2 targetVelocity = _moveInput * moveSpeed;

            // Smooth acceleration/deceleration
            if (_moveInput.magnitude > 0.1f)
            {
                _currentVelocity = Vector2.MoveTowards(_currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            }
            else
            {
                _currentVelocity = Vector2.MoveTowards(_currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            }

            _rigidbody.velocity = _currentVelocity;
        }

        private void StartDash()
        {
            if (_isDashing || !_canDash) return;

            _isDashing = true;
            _canDash = false;
            _isInvulnerable = dashInvulnerable;

            Vector2 dashDirection = _moveInput.magnitude > 0.1f ? _moveInput.normalized : Vector2.right;
            StartCoroutine(DashCoroutine(dashDirection));

            OnDashUsed?.Invoke();
        }

        private System.Collections.IEnumerator DashCoroutine(Vector2 direction)
        {
            float elapsed = 0f;
            Vector2 startPos = transform.position;
            Vector2 endPos = startPos + direction * dashDistance;

            // Disable collision during dash if invulnerable
            if (_isInvulnerable)
            {
                _collider.enabled = false;
            }

            while (elapsed < dashDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / dashDuration;
                
                // Smooth dash curve
                t = 1f - (1f - t) * (1f - t); // Ease out

                transform.position = Vector2.Lerp(startPos, endPos, t);
                yield return null;
            }

            transform.position = endPos;
            _isDashing = false;
            _isInvulnerable = false;

            if (_collider != null)
            {
                _collider.enabled = true;
            }

            _dashCooldownTimer = dashCooldown;
        }

        private void UpdateDashCooldown()
        {
            if (_dashCooldownTimer > 0f)
            {
                _dashCooldownTimer -= Time.deltaTime;
                OnDashCooldownChanged?.Invoke(_dashCooldownTimer / dashCooldown);

                if (_dashCooldownTimer <= 0f)
                {
                    _canDash = true;
                }
            }
        }

        private void ShootBullet()
        {
            if (!_hasBullet) return;

            // Get mouse position in world space
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            Vector2 shootDirection = (mouseWorldPos - bulletSpawnPoint.position).normalized;

            // This will be handled by BulletManager
            // For now, we'll just set the state
            _hasBullet = false;
            OnBulletStateChanged?.Invoke(_hasBullet);
        }

        private void TryRetrieveBullet()
        {
            // Find nearby bullet
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bulletRetrieveRange, bulletLayer);

            foreach (Collider2D col in colliders)
            {
                var bullet = col.GetComponent<Weapons.Bullet>();
                if (bullet != null && bullet.IsRetrievable)
                {
                    RetrieveBullet(bullet.gameObject);
                    break;
                }
            }
        }

        private void RetrieveBullet(GameObject bullet)
        {
            if (bullet == null) return;

            _hasBullet = true;
            _activeBullet = null;
            OnBulletStateChanged?.Invoke(_hasBullet);

            // Destroy or return bullet to pool
            if (bullet != null)
            {
                Destroy(bullet);
            }
        }

        public void OnBulletRetrieved()
        {
            _hasBullet = true;
            _activeBullet = null;
            OnBulletStateChanged?.Invoke(_hasBullet);
        }

        public void OnBulletShot(GameObject bullet)
        {
            _hasBullet = false;
            _activeBullet = bullet;
            OnBulletStateChanged?.Invoke(_hasBullet);
        }

        // Getters
        public bool HasBullet => _hasBullet;
        public bool IsDashing => _isDashing;
        public bool IsInvulnerable => _isInvulnerable;
        public bool CanDash => _canDash;
        public float DashCooldownProgress => _dashCooldownTimer / dashCooldown;

        private void OnDrawGizmosSelected()
        {
            // Draw bullet retrieve range
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCircle(transform.position, bulletRetrieveRange);
        }
    }
}

