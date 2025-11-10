using UnityEngine;
using System.Collections.Generic;

namespace Inkblade.Weapons
{
    /// <summary>
    /// Generic object pool for reusing GameObjects.
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        [Header("Pool Settings")]
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialSize = 10;
        [SerializeField] private int maxSize = 50;
        [SerializeField] private bool expandable = true;

        private Queue<GameObject> _pool = new Queue<GameObject>();
        private List<GameObject> _activeObjects = new List<GameObject>();

        private void Awake()
        {
            InitializePool();
        }

        private void InitializePool()
        {
            if (prefab == null)
            {
                Debug.LogError("ObjectPool: Prefab is not assigned!");
                return;
            }

            for (int i = 0; i < initialSize; i++)
            {
                CreatePooledObject();
            }
        }

        private GameObject CreatePooledObject()
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            _pool.Enqueue(obj);
            return obj;
        }

        public GameObject Get()
        {
            GameObject obj;

            if (_pool.Count > 0)
            {
                obj = _pool.Dequeue();
            }
            else if (expandable)
            {
                obj = CreatePooledObject();
            }
            else
            {
                Debug.LogWarning("ObjectPool: Pool exhausted and not expandable!");
                return null;
            }

            obj.SetActive(true);
            _activeObjects.Add(obj);
            return obj;
        }

        public void Return(GameObject obj)
        {
            if (obj == null) return;

            obj.SetActive(false);
            obj.transform.SetParent(transform);
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;

            _activeObjects.Remove(obj);

            if (_pool.Count < maxSize)
            {
                _pool.Enqueue(obj);
            }
            else
            {
                Destroy(obj);
            }
        }

        public void Clear()
        {
            foreach (GameObject obj in _activeObjects)
            {
                if (obj != null)
                {
                    Return(obj);
                }
            }
            _activeObjects.Clear();
        }

        public int ActiveCount => _activeObjects.Count;
        public int PooledCount => _pool.Count;
    }
}

