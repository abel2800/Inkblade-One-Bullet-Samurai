using UnityEngine;
using UnityEditor;
using Inkblade.Player;
using Inkblade.Weapons;
using Inkblade.Enemies;
using Inkblade.Utils;

namespace Inkblade.Editor
{
    /// <summary>
    /// Editor script to create prefabs with all required components.
    /// </summary>
    public class PrefabCreator : EditorWindow
    {
        [MenuItem("Inkblade/Create Prefabs")]
        public static void ShowWindow()
        {
            GetWindow<PrefabCreator>("Prefab Creator");
        }

        private void OnGUI()
        {
            GUILayout.Label("Create Game Prefabs", EditorStyles.boldLabel);
            GUILayout.Space(10);

            if (GUILayout.Button("Create Player Prefab", GUILayout.Height(30)))
            {
                CreatePlayerPrefab();
            }

            if (GUILayout.Button("Create Bullet Prefab", GUILayout.Height(30)))
            {
                CreateBulletPrefab();
            }

            if (GUILayout.Button("Create Enemy Prefab", GUILayout.Height(30)))
            {
                CreateEnemyPrefab();
            }

            if (GUILayout.Button("Create All Prefabs", GUILayout.Height(40)))
            {
                CreatePlayerPrefab();
                CreateBulletPrefab();
                CreateEnemyPrefab();
                EditorUtility.DisplayDialog("Prefabs Created", "All prefabs created successfully!", "OK");
            }

            GUILayout.Space(20);
            GUILayout.Label("Note: You may need to assign sprites and configure settings manually.", EditorStyles.helpBox);
        }

        private static void CreatePlayerPrefab()
        {
            // Create GameObject
            GameObject player = new GameObject("Player");
            
            // Add components
            SpriteRenderer sr = player.AddComponent<SpriteRenderer>();
            Rigidbody2D rb = player.AddComponent<Rigidbody2D>();
            CircleCollider2D col = player.AddComponent<CircleCollider2D>();
            PlayerController controller = player.AddComponent<PlayerController>();
            PlayerHealth health = player.AddComponent<PlayerHealth>();

            // Configure Rigidbody2D
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb.freezeRotation = true;

            // Configure Collider
            col.radius = 0.5f;

            // Set tag and layer
            player.tag = Constants.TAG_PLAYER;
            player.layer = LayerMask.NameToLayer(Constants.LAYER_PLAYER);

            // Create bullet spawn point
            GameObject spawnPoint = new GameObject("BulletSpawnPoint");
            spawnPoint.transform.SetParent(player.transform);
            spawnPoint.transform.localPosition = new Vector3(0.5f, 0, 0);

            // Save as prefab
            string path = "Assets/Prefabs/Player.prefab";
            EnsureDirectoryExists("Assets/Prefabs");
            PrefabUtility.SaveAsPrefabAsset(player, path);
            DestroyImmediate(player);

            Debug.Log($"Created Player prefab at {path}");
        }

        private static void CreateBulletPrefab()
        {
            // Create GameObject
            GameObject bullet = new GameObject("Bullet");

            // Add components
            SpriteRenderer sr = bullet.AddComponent<SpriteRenderer>();
            Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>();
            CircleCollider2D col = bullet.AddComponent<CircleCollider2D>();
            Bullet bulletScript = bullet.AddComponent<Bullet>();

            // Configure Rigidbody2D
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 0;
            rb.freezeRotation = true;

            // Configure Collider
            col.radius = 0.1f;
            col.isTrigger = true;

            // Set tag and layer
            bullet.tag = Constants.TAG_BULLET;
            bullet.layer = LayerMask.NameToLayer(Constants.LAYER_BULLET);

            // Save as prefab
            string path = "Assets/Prefabs/Bullet.prefab";
            EnsureDirectoryExists("Assets/Prefabs");
            PrefabUtility.SaveAsPrefabAsset(bullet, path);
            DestroyImmediate(bullet);

            Debug.Log($"Created Bullet prefab at {path}");
        }

        private static void CreateEnemyPrefab()
        {
            // Create GameObject
            GameObject enemy = new GameObject("Enemy_Basic");

            // Add components
            SpriteRenderer sr = enemy.AddComponent<SpriteRenderer>();
            Rigidbody2D rb = enemy.AddComponent<Rigidbody2D>();
            CircleCollider2D col = enemy.AddComponent<CircleCollider2D>();
            EnemyAI ai = enemy.AddComponent<EnemyAI>();
            EnemyHealth health = enemy.AddComponent<EnemyHealth>();

            // Configure Rigidbody2D
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb.freezeRotation = true;

            // Configure Collider
            col.radius = 0.4f;

            // Set tag and layer
            enemy.tag = Constants.TAG_ENEMY;
            enemy.layer = LayerMask.NameToLayer(Constants.LAYER_ENEMY);

            // Save as prefab
            string path = "Assets/Prefabs/Enemy_Basic.prefab";
            EnsureDirectoryExists("Assets/Prefabs");
            PrefabUtility.SaveAsPrefabAsset(enemy, path);
            DestroyImmediate(enemy);

            Debug.Log($"Created Enemy prefab at {path}");
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                string parentPath = System.IO.Path.GetDirectoryName(path).Replace('\\', '/');
                string folderName = System.IO.Path.GetFileName(path);
                AssetDatabase.CreateFolder(parentPath, folderName);
            }
        }
    }
}

