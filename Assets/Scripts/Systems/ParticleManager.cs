using UnityEngine;
using System.Collections.Generic;

namespace Inkblade.Systems
{
    /// <summary>
    /// Manages particle effects with pooling for performance.
    /// </summary>
    public class ParticleManager : MonoBehaviour
    {
        [Header("Particle Prefabs")]
        [SerializeField] private GameObject hitParticlePrefab;
        [SerializeField] private GameObject deathParticlePrefab;
        [SerializeField] private GameObject impactParticlePrefab;
        [SerializeField] private GameObject dashTrailPrefab;
        [SerializeField] private GameObject inkSplashPrefab;

        [Header("Pool Settings")]
        [SerializeField] private int poolSize = 20;

        private Dictionary<string, Queue<GameObject>> _particlePools = new Dictionary<string, Queue<GameObject>>();
        private Dictionary<string, GameObject> _particlePrefabs = new Dictionary<string, GameObject>();
        private Transform _particleContainer;

        // Singleton
        public static ParticleManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializePools();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializePools()
        {
            // Create container
            GameObject container = new GameObject("ParticleContainer");
            container.transform.SetParent(transform);
            _particleContainer = container.transform;

            // Register particle prefabs
            if (hitParticlePrefab != null)
            {
                RegisterParticle("Hit", hitParticlePrefab);
            }

            if (deathParticlePrefab != null)
            {
                RegisterParticle("Death", deathParticlePrefab);
            }

            if (impactParticlePrefab != null)
            {
                RegisterParticle("Impact", impactParticlePrefab);
            }

            if (dashTrailPrefab != null)
            {
                RegisterParticle("DashTrail", dashTrailPrefab);
            }

            if (inkSplashPrefab != null)
            {
                RegisterParticle("InkSplash", inkSplashPrefab);
            }
        }

        private void RegisterParticle(string name, GameObject prefab)
        {
            _particlePrefabs[name] = prefab;
            _particlePools[name] = new Queue<GameObject>();

            // Pre-populate pool
            for (int i = 0; i < poolSize; i++)
            {
                GameObject particle = Instantiate(prefab, _particleContainer);
                particle.SetActive(false);
                _particlePools[name].Enqueue(particle);
            }
        }

        /// <summary>
        /// Spawn a particle effect at the specified position.
        /// </summary>
        public GameObject SpawnParticle(string particleName, Vector3 position, Quaternion rotation = default)
        {
            if (!_particlePools.ContainsKey(particleName))
            {
                Debug.LogWarning($"ParticleManager: Particle '{particleName}' not registered!");
                return null;
            }

            GameObject particle = GetPooledParticle(particleName);
            if (particle == null)
            {
                // Pool exhausted, create new
                if (_particlePrefabs.ContainsKey(particleName))
                {
                    particle = Instantiate(_particlePrefabs[particleName], _particleContainer);
                }
                else
                {
                    return null;
                }
            }

            particle.transform.position = position;
            particle.transform.rotation = rotation == default ? Quaternion.identity : rotation;
            particle.SetActive(true);

            // Auto-return to pool after particle system finishes
            ParticleSystem ps = particle.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                StartCoroutine(ReturnToPoolAfterPlay(particleName, particle, ps.main.duration + ps.main.startLifetime.constantMax));
            }

            return particle;
        }

        private GameObject GetPooledParticle(string particleName)
        {
            if (_particlePools[particleName].Count > 0)
            {
                return _particlePools[particleName].Dequeue();
            }
            return null;
        }

        private System.Collections.IEnumerator ReturnToPoolAfterPlay(string particleName, GameObject particle, float duration)
        {
            yield return new WaitForSeconds(duration);
            ReturnToPool(particleName, particle);
        }

        private void ReturnToPool(string particleName, GameObject particle)
        {
            if (particle == null) return;

            particle.SetActive(false);
            particle.transform.SetParent(_particleContainer);
            particle.transform.position = Vector3.zero;
            particle.transform.rotation = Quaternion.identity;

            _particlePools[particleName].Enqueue(particle);
        }

        // Convenience methods
        public void SpawnHitEffect(Vector3 position) => SpawnParticle("Hit", position);
        public void SpawnDeathEffect(Vector3 position) => SpawnParticle("Death", position);
        public void SpawnImpactEffect(Vector3 position) => SpawnParticle("Impact", position);
        public void SpawnDashTrail(Vector3 position) => SpawnParticle("DashTrail", position);
        public void SpawnInkSplash(Vector3 position) => SpawnParticle("InkSplash", position);
    }
}

