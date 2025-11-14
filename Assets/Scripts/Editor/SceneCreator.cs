using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Inkblade.Systems;
using Inkblade.Player;
using Inkblade.UI;
using Inkblade.Weapons;
using Inkblade.Enemies;
using Inkblade.Utils;

namespace Inkblade.Editor
{
    /// <summary>
    /// Editor script to create game scenes with all required objects.
    /// </summary>
    public class SceneCreator : EditorWindow
    {
        [MenuItem("Inkblade/Create Scenes")]
        public static void ShowWindow()
        {
            GetWindow<SceneCreator>("Scene Creator");
        }

        private void OnGUI()
        {
            GUILayout.Label("Create Game Scenes", EditorStyles.boldLabel);
            GUILayout.Space(10);

            if (GUILayout.Button("Create MainMenu Scene", GUILayout.Height(30)))
            {
                CreateMainMenuScene();
            }

            if (GUILayout.Button("Create Level_Play Scene", GUILayout.Height(30)))
            {
                CreateLevelPlayScene();
            }

            if (GUILayout.Button("Create All Scenes", GUILayout.Height(40)))
            {
                CreateMainMenuScene();
                CreateLevelPlayScene();
                EditorUtility.DisplayDialog("Scenes Created", "All scenes created successfully!", "OK");
            }

            GUILayout.Space(20);
            GUILayout.Label("Note: Make sure prefabs are created first (Inkblade > Create Prefabs)", EditorStyles.helpBox);
        }

        private static void CreateMainMenuScene()
        {
            // Create new scene
            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            
            // Remove default camera and light (we'll add our own)
            var mainCamera = GameObject.Find("Main Camera");
            if (mainCamera != null) DestroyImmediate(mainCamera);
            var light = GameObject.Find("Directional Light");
            if (light != null) DestroyImmediate(light);

            // Create Camera
            GameObject cameraObj = new GameObject("Main Camera");
            Camera cam = cameraObj.AddComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = 5f;
            cameraObj.transform.position = new Vector3(0, 0, -10);
            cameraObj.tag = "MainCamera";

            // Create Canvas
            GameObject canvasObj = new GameObject("Canvas");
            Canvas canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
            canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();

            // Create EventSystem
            GameObject eventSystemObj = new GameObject("EventSystem");
            eventSystemObj.AddComponent<UnityEngine.EventSystems.EventSystem>();
            eventSystemObj.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();

            // Create UI Manager
            GameObject uiManagerObj = new GameObject("UIManager");
            UIManager uiManager = uiManagerObj.AddComponent<UIManager>();

            // Create Main Menu Panel
            GameObject menuPanel = new GameObject("MainMenuPanel");
            menuPanel.transform.SetParent(canvasObj.transform, false);
            RectTransform menuRect = menuPanel.AddComponent<RectTransform>();
            menuRect.anchorMin = Vector2.zero;
            menuRect.anchorMax = Vector2.one;
            menuRect.sizeDelta = Vector2.zero;

            // Create Title
            GameObject titleObj = new GameObject("Title");
            titleObj.transform.SetParent(menuPanel.transform, false);
            var titleText = titleObj.AddComponent<UnityEngine.UI.Text>();
            titleText.text = "INKBLADE";
            titleText.fontSize = 72;
            titleText.alignment = TextAnchor.MiddleCenter;
            titleText.color = Color.white;
            RectTransform titleRect = titleObj.GetComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 0.8f);
            titleRect.anchorMax = new Vector2(0.5f, 0.8f);
            titleRect.sizeDelta = new Vector2(400, 100);
            titleRect.anchoredPosition = Vector2.zero;

            // Create Play Button
            CreateMenuButton(menuPanel, "PlayButton", "PLAY", new Vector2(0.5f, 0.5f), ButtonType.Play);
            
            // Create Settings Button
            CreateMenuButton(menuPanel, "SettingsButton", "SETTINGS", new Vector2(0.5f, 0.4f), ButtonType.Settings);
            
            // Create Quit Button
            CreateMenuButton(menuPanel, "QuitButton", "QUIT", new Vector2(0.5f, 0.3f), ButtonType.Quit);

            // Save scene
            string scenePath = "Assets/Scenes/MainMenu.unity";
            EnsureDirectoryExists("Assets/Scenes");
            EditorSceneManager.SaveScene(scene, scenePath);
            
            Debug.Log($"Created MainMenu scene at {scenePath}");
        }

        private static void CreateLevelPlayScene()
        {
            // Create new scene
            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            
            // Remove default camera and light
            var mainCamera = GameObject.Find("Main Camera");
            if (mainCamera != null) DestroyImmediate(mainCamera);
            var light = GameObject.Find("Directional Light");
            if (light != null) DestroyImmediate(light);

            // Create Camera with CameraController
            GameObject cameraObj = new GameObject("Main Camera");
            Camera cam = cameraObj.AddComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = 5f;
            cameraObj.transform.position = new Vector3(0, 0, -10);
            cameraObj.tag = "MainCamera";
            CameraController cameraController = cameraObj.AddComponent<CameraController>();

            // Create Game Manager
            GameObject gameManagerObj = new GameObject("GameManager");
            GameManager gameManager = gameManagerObj.AddComponent<GameManager>();

            // Create Player (from prefab if exists, otherwise create new)
            GameObject playerObj = null;
            var playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player.prefab");
            if (playerPrefab != null)
            {
                playerObj = PrefabUtility.InstantiatePrefab(playerPrefab) as GameObject;
            }
            else
            {
                playerObj = new GameObject("Player");
                playerObj.AddComponent<PlayerController>();
                playerObj.AddComponent<PlayerHealth>();
                Debug.LogWarning("Player prefab not found, created basic GameObject");
            }
            playerObj.transform.position = Vector3.zero;

            // Set GameManager references
            var playerController = playerObj.GetComponent<PlayerController>();
            var playerHealth = playerObj.GetComponent<PlayerHealth>();
            if (playerController != null) gameManager.SetPlayer(playerController);
            if (playerHealth != null) gameManager.SetPlayerHealth(playerHealth);
            gameManager.SetCameraController(cameraController);

            // Create Bullet Manager
            GameObject bulletManagerObj = new GameObject("BulletManager");
            BulletManager bulletManager = bulletManagerObj.AddComponent<BulletManager>();
            
            // Assign bullet prefab to BulletManager if it exists
            var bulletPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Bullet.prefab");
            if (bulletPrefab != null)
            {
                SerializedObject serializedBulletManager = new SerializedObject(bulletManager);
                serializedBulletManager.FindProperty("bulletPrefab").objectReferenceValue = bulletPrefab;
                serializedBulletManager.ApplyModifiedProperties();
            }

            // Create Enemy Spawner
            GameObject enemySpawnerObj = new GameObject("EnemySpawner");
            EnemySpawner enemySpawner = enemySpawnerObj.AddComponent<EnemySpawner>();
            
            // Assign enemy prefab to EnemySpawner if it exists
            var enemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemy_Basic.prefab");
            if (enemyPrefab != null)
            {
                SerializedObject serializedEnemySpawner = new SerializedObject(enemySpawner);
                serializedEnemySpawner.FindProperty("enemyPrefab").objectReferenceValue = enemyPrefab;
                serializedEnemySpawner.ApplyModifiedProperties();
            }
            
            gameManager.SetEnemySpawner(enemySpawner);

            // Create Particle Manager
            GameObject particleManagerObj = new GameObject("ParticleManager");
            particleManagerObj.AddComponent<ParticleManager>();

            // Create Audio Manager
            GameObject audioManagerObj = new GameObject("AudioManager");
            audioManagerObj.AddComponent<AudioManager>();

            // Create Save Manager
            GameObject saveManagerObj = new GameObject("SaveManager");
            saveManagerObj.AddComponent<SaveManager>();

            // Create Ground (simple platform)
            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ground.name = "Ground";
            ground.transform.position = new Vector3(0, -5, 0);
            ground.transform.localScale = new Vector3(20, 1, 1);
            ground.tag = Constants.TAG_GROUND;
            ground.layer = LayerMask.NameToLayer(Constants.LAYER_GROUND);
            var groundRenderer = ground.GetComponent<Renderer>();
            if (groundRenderer != null) groundRenderer.material.color = Color.gray;

            // Create Walls
            CreateWall(new Vector3(-10, 0, 0), new Vector3(1, 10, 1));
            CreateWall(new Vector3(10, 0, 0), new Vector3(1, 10, 1));

            // Create Canvas for HUD
            GameObject canvasObj = new GameObject("Canvas");
            Canvas canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
            canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();

            // Create EventSystem
            GameObject eventSystemObj = new GameObject("EventSystem");
            eventSystemObj.AddComponent<UnityEngine.EventSystems.EventSystem>();
            eventSystemObj.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();

            // Create HUD
            GameObject hudObj = new GameObject("HUD");
            hudObj.transform.SetParent(canvasObj.transform, false);
            RectTransform hudRect = hudObj.AddComponent<RectTransform>();
            hudRect.anchorMin = Vector2.zero;
            hudRect.anchorMax = Vector2.one;
            hudRect.sizeDelta = Vector2.zero;
            HUD hud = hudObj.AddComponent<HUD>();
            
            // Set HUD references using SerializedObject
            SerializedObject serializedHUD = new SerializedObject(hud);
            serializedHUD.FindProperty("enemySpawner").objectReferenceValue = enemySpawner;
            serializedHUD.FindProperty("player").objectReferenceValue = playerController;
            serializedHUD.FindProperty("playerHealth").objectReferenceValue = playerHealth;
            serializedHUD.FindProperty("gameManager").objectReferenceValue = gameManager;
            serializedHUD.ApplyModifiedProperties();

            // Create Pause Menu
            GameObject pauseMenuObj = new GameObject("PauseMenu");
            pauseMenuObj.transform.SetParent(canvasObj.transform, false);
            RectTransform pauseRect = pauseMenuObj.AddComponent<RectTransform>();
            pauseRect.anchorMin = Vector2.zero;
            pauseRect.anchorMax = Vector2.one;
            pauseRect.sizeDelta = Vector2.zero;
            PauseMenu pauseMenu = pauseMenuObj.AddComponent<PauseMenu>();
            pauseMenuObj.SetActive(false);

            // Create Game Over Menu
            GameObject gameOverMenuObj = new GameObject("GameOverMenu");
            gameOverMenuObj.transform.SetParent(canvasObj.transform, false);
            RectTransform gameOverRect = gameOverMenuObj.AddComponent<RectTransform>();
            gameOverRect.anchorMin = Vector2.zero;
            gameOverRect.anchorMax = Vector2.one;
            gameOverRect.sizeDelta = Vector2.zero;
            GameOverMenu gameOverMenu = gameOverMenuObj.AddComponent<GameOverMenu>();
            
            // Set GameOverMenu references using SerializedObject
            SerializedObject serializedGameOverMenu = new SerializedObject(gameOverMenu);
            serializedGameOverMenu.FindProperty("_enemySpawner").objectReferenceValue = enemySpawner;
            serializedGameOverMenu.ApplyModifiedProperties();
            
            gameOverMenuObj.SetActive(false);

            // Save scene
            string scenePath = "Assets/Scenes/Level_Play.unity";
            EnsureDirectoryExists("Assets/Scenes");
            EditorSceneManager.SaveScene(scene, scenePath);
            
            Debug.Log($"Created Level_Play scene at {scenePath}");
        }

        private static void CreateMenuButton(GameObject parent, string name, string text, Vector2 anchor, ButtonType type)
        {
            GameObject buttonObj = new GameObject(name);
            buttonObj.transform.SetParent(parent.transform, false);
            
            RectTransform rect = buttonObj.AddComponent<RectTransform>();
            rect.anchorMin = anchor;
            rect.anchorMax = anchor;
            rect.sizeDelta = new Vector2(200, 50);
            rect.anchoredPosition = Vector2.zero;

            UnityEngine.UI.Image image = buttonObj.AddComponent<UnityEngine.UI.Image>();
            image.color = new Color(0.2f, 0.2f, 0.2f, 1f);

            UnityEngine.UI.Button button = buttonObj.AddComponent<UnityEngine.UI.Button>();
            
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(buttonObj.transform, false);
            var textComponent = textObj.AddComponent<UnityEngine.UI.Text>();
            textComponent.text = text;
            textComponent.fontSize = 24;
            textComponent.alignment = TextAnchor.MiddleCenter;
            textComponent.color = Color.white;
            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.sizeDelta = Vector2.zero;

            MainMenuButton menuButton = buttonObj.AddComponent<MainMenuButton>();
            
            // Set button type using SerializedObject
            SerializedObject serializedButton = new SerializedObject(menuButton);
            SerializedProperty buttonTypeProp = serializedButton.FindProperty("buttonType");
            if (buttonTypeProp != null)
            {
                buttonTypeProp.enumValueIndex = (int)type;
                serializedButton.ApplyModifiedProperties();
            }

            button.onClick.AddListener(menuButton.OnButtonClick);
        }

        private static void CreateWall(Vector3 position, Vector3 scale)
        {
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.name = "Wall";
            wall.transform.position = position;
            wall.transform.localScale = scale;
            wall.tag = Constants.TAG_WALL;
            wall.layer = LayerMask.NameToLayer(Constants.LAYER_WALL);
            var wallRenderer = wall.GetComponent<Renderer>();
            if (wallRenderer != null) wallRenderer.material.color = Color.gray;
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

        private enum ButtonType
        {
            Play,
            Settings,
            Quit,
            Leaderboard
        }
    }
}

