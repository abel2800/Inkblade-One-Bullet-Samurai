using UnityEngine;
using System.Collections;
using Inkblade.Utils;

namespace Inkblade.Systems
{
    /// <summary>
    /// Camera controller with smooth follow, shake effects, and slow motion support.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [Header("Follow Settings")]
        [SerializeField] private Transform target;
        [SerializeField] private float followSpeed = 5f;
        [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
        [SerializeField] private bool smoothFollow = true;

        [Header("Shake Settings")]
        [SerializeField] private float shakeIntensity = 0.1f;
        [SerializeField] private float shakeDuration = 0.2f;
        [SerializeField] private float shakeDecay = 2f;

        [Header("Bounds")]
        [SerializeField] private bool useBounds = false;
        [SerializeField] private Vector2 minBounds;
        [SerializeField] private Vector2 maxBounds;

        private Camera _camera;
        private Vector3 _originalPosition;
        private Vector3 _shakeOffset;
        private float _currentShakeIntensity = 0f;
        private float _currentShakeDuration = 0f;

        // Events
        public System.Action OnShakeStarted;
        public System.Action OnShakeEnded;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            if (_camera == null)
            {
                _camera = Camera.main;
            }

            _originalPosition = transform.position;

            // Find target if not assigned
            if (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER);
                if (player != null)
                {
                    target = player.transform;
                }
            }
        }

        private void LateUpdate()
        {
            if (target != null)
            {
                FollowTarget();
            }

            UpdateShake();
            ApplyShake();
        }

        private void FollowTarget()
        {
            Vector3 targetPosition = target.position + offset;

            // Apply bounds if enabled
            if (useBounds)
            {
                targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);
            }

            if (smoothFollow)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = targetPosition;
            }

            _originalPosition = transform.position;
        }

        private void UpdateShake()
        {
            if (_currentShakeDuration > 0f)
            {
                _currentShakeDuration -= Time.deltaTime;

                // Decay shake intensity
                _currentShakeIntensity = Mathf.Lerp(_currentShakeIntensity, 0f, shakeDecay * Time.deltaTime);

                if (_currentShakeDuration <= 0f)
                {
                    _currentShakeIntensity = 0f;
                    OnShakeEnded?.Invoke();
                }
            }
        }

        private void ApplyShake()
        {
            if (_currentShakeIntensity > 0f)
            {
                _shakeOffset = Random.insideUnitCircle * _currentShakeIntensity;
                transform.position = _originalPosition + _shakeOffset;
            }
            else
            {
                _shakeOffset = Vector3.zero;
            }
        }

        /// <summary>
        /// Trigger camera shake with default settings.
        /// </summary>
        public void Shake()
        {
            Shake(shakeIntensity, shakeDuration);
        }

        /// <summary>
        /// Trigger camera shake with custom intensity and duration.
        /// </summary>
        public void Shake(float intensity, float duration)
        {
            _currentShakeIntensity = intensity;
            _currentShakeDuration = duration;
            OnShakeStarted?.Invoke();
        }

        /// <summary>
        /// Set the camera target to follow.
        /// </summary>
        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        /// <summary>
        /// Set camera bounds for constrained movement.
        /// </summary>
        public void SetBounds(Vector2 min, Vector2 max)
        {
            minBounds = min;
            maxBounds = max;
            useBounds = true;
        }

        /// <summary>
        /// Disable camera bounds.
        /// </summary>
        public void DisableBounds()
        {
            useBounds = false;
        }

        // Getters
        public Transform Target => target;
        public bool IsShaking => _currentShakeDuration > 0f;
    }
}

