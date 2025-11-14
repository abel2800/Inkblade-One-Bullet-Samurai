using UnityEngine;
using System.Collections.Generic;

namespace Inkblade.Weapons
{
    /// <summary>
    /// Manages bullet spawning, pooling, and lifecycle.
    /// </summary>
    public class BulletManager : MonoBehaviour
    {
        [Header("Bullet Settings")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private int poolSize = 10;

        [Header("References")]
        [SerializeField] private Transform bulletContainer;

        private Queue<GameObject> _bulletPool = new Queue<GameObject>();
        private List<GameObject> _activeBullets = new List<GameObject>();
        private Player.PlayerController _playerController;

        // Singleton
        public static BulletManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializePool();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _playerController = FindObjectOfType<Player.PlayerController>();
            if (_playerController != null)
            {
                _playerController.OnBulletStateChanged += HandleBulletStateChanged;
            }
        }

        private void InitializePool()
        {
            if (bulletPrefab == null)
            {
                Debug.LogError("BulletManager: Bullet prefab is not assigned!");
                return;
            }

            if (bulletContainer == null)
            {
                GameObject container = new GameObject("BulletContainer");
                bulletContainer = container.transform;
            }

            for (int i = 0; i < poolSize; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletContainer);
                bullet.SetActive(false);
                _bulletPool.Enqueue(bullet);
            }
        }

        public GameObject SpawnBullet(Vector2 position, Vector2 direction)
        {
            GameObject bullet = GetPooledBullet();
            if (bullet == null)
            {
                // Pool exhausted, create new bullet
                bullet = Instantiate(bulletPrefab, bulletContainer);
            }

            bullet.SetActive(true);
            bullet.transform.position = position;

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Shoot(direction, position);
                bulletScript.OnRetrieved += OnBulletRetrieved;
                bulletScript.OnStuck += OnBulletStuck;
            }

            _activeBullets.Add(bullet);
            return bullet;
        }

        private GameObject GetPooledBullet()
        {
            if (_bulletPool.Count > 0)
            {
                return _bulletPool.Dequeue();
            }
            return null;
        }

        private void ReturnToPool(GameObject bullet)
        {
            if (bullet == null) return;

            bullet.SetActive(false);
            bullet.transform.SetParent(bulletContainer);
            bullet.transform.position = Vector3.zero;
            bullet.transform.rotation = Quaternion.identity;

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.OnRetrieved -= OnBulletRetrieved;
                bulletScript.OnStuck -= OnBulletStuck;
            }

            _activeBullets.Remove(bullet);
            _bulletPool.Enqueue(bullet);
        }

        private void OnBulletRetrieved(Bullet bullet)
        {
            if (bullet == null) return;
            
            if (_playerController != null)
            {
                _playerController.OnBulletRetrieved();
            }
            ReturnToPool(bullet.gameObject);
        }

        private void OnBulletStuck(Bullet bullet)
        {
            // Bullet is stuck, waiting for retrieval
        }

        private void HandleBulletStateChanged(bool hasBullet)
        {
            // Handle bullet state changes from player
        }

        public void ShootBullet(Vector2 spawnPosition, Vector2 direction)
        {
            GameObject bullet = SpawnBullet(spawnPosition, direction);
            if (_playerController != null && bullet != null)
            {
                _playerController.OnBulletShot(bullet);
            }
        }

        public void ClearAllBullets()
        {
            foreach (GameObject bullet in _activeBullets)
            {
                if (bullet != null)
                {
                    ReturnToPool(bullet);
                }
            }
            _activeBullets.Clear();
        }

        private void OnDestroy()
        {
            if (_playerController != null)
            {
                _playerController.OnBulletStateChanged -= HandleBulletStateChanged;
            }
        }
    }
}

