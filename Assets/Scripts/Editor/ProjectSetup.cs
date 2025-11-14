using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Inkblade.Utils;

namespace Inkblade.Editor
{
    /// <summary>
    /// Editor script to automate project setup (tags, layers, etc.)
    /// </summary>
    public class ProjectSetup : EditorWindow
    {
        [MenuItem("Inkblade/Setup Project")]
        public static void ShowWindow()
        {
            GetWindow<ProjectSetup>("Project Setup");
        }

        private void OnGUI()
        {
            GUILayout.Label("INKBLADE Project Setup", EditorStyles.boldLabel);
            GUILayout.Space(10);

            if (GUILayout.Button("Setup Tags", GUILayout.Height(30)))
            {
                SetupTags();
            }

            if (GUILayout.Button("Setup Layers", GUILayout.Height(30)))
            {
                SetupLayers();
            }

            if (GUILayout.Button("Setup Physics2D Layers", GUILayout.Height(30)))
            {
                SetupPhysics2DLayers();
            }

            if (GUILayout.Button("Setup All", GUILayout.Height(40)))
            {
                SetupTags();
                SetupLayers();
                SetupPhysics2DLayers();
                EditorUtility.DisplayDialog("Setup Complete", "Project setup completed successfully!", "OK");
            }

            GUILayout.Space(20);
            GUILayout.Label("Note: Some settings may require manual verification.", EditorStyles.helpBox);
        }

        private static void SetupTags()
        {
            // Define tags using Constants
            string[] tags = { 
                Constants.TAG_PLAYER, 
                Constants.TAG_ENEMY, 
                Constants.TAG_BULLET, 
                Constants.TAG_WALL, 
                Constants.TAG_GROUND 
            };

            foreach (string tag in tags)
            {
                if (!TagExists(tag))
                {
                    SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
                    SerializedProperty tagsProp = tagManager.FindProperty("tags");

                    tagsProp.arraySize++;
                    SerializedProperty newTag = tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1);
                    newTag.stringValue = tag;
                    tagManager.ApplyModifiedProperties();

                    Debug.Log($"Created tag: {tag}");
                }
                else
                {
                    Debug.Log($"Tag already exists: {tag}");
                }
            }
        }

        private static void SetupLayers()
        {
            // Define layers (starting from layer 8 to avoid conflicts) using Constants
            var layers = new System.Collections.Generic.Dictionary<string, int>
            {
                { Constants.LAYER_PLAYER, 8 },
                { Constants.LAYER_ENEMY, 9 },
                { Constants.LAYER_BULLET, 10 },
                { Constants.LAYER_WALL, 11 },
                { Constants.LAYER_GROUND, 12 }
            };

            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty layersProp = tagManager.FindProperty("layers");

            foreach (var layer in layers)
            {
                if (layersProp.GetArrayElementAtIndex(layer.Value).stringValue != layer.Key)
                {
                    layersProp.GetArrayElementAtIndex(layer.Value).stringValue = layer.Key;
                    Debug.Log($"Set layer {layer.Value} to: {layer.Key}");
                }
            }

            tagManager.ApplyModifiedProperties();
        }

        private static void SetupPhysics2DLayers()
        {
            // Configure Physics2D layer collisions
            // Player can collide with Enemy, Wall, Ground
            // Bullet can collide with Enemy, Wall, Ground
            // Enemy can collide with Player, Wall, Ground

            int playerLayer = LayerMask.NameToLayer(Constants.LAYER_PLAYER);
            int enemyLayer = LayerMask.NameToLayer(Constants.LAYER_ENEMY);
            int bulletLayer = LayerMask.NameToLayer(Constants.LAYER_BULLET);
            int wallLayer = LayerMask.NameToLayer(Constants.LAYER_WALL);
            int groundLayer = LayerMask.NameToLayer(Constants.LAYER_GROUND);

            // Player collisions
            Physics2D.IgnoreLayerCollision(playerLayer, playerLayer, true);
            Physics2D.IgnoreLayerCollision(playerLayer, bulletLayer, true);
            Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
            Physics2D.IgnoreLayerCollision(playerLayer, wallLayer, false);
            Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);

            // Bullet collisions
            Physics2D.IgnoreLayerCollision(bulletLayer, bulletLayer, true);
            Physics2D.IgnoreLayerCollision(bulletLayer, playerLayer, true);
            Physics2D.IgnoreLayerCollision(bulletLayer, enemyLayer, false);
            Physics2D.IgnoreLayerCollision(bulletLayer, wallLayer, false);
            Physics2D.IgnoreLayerCollision(bulletLayer, groundLayer, false);

            // Enemy collisions
            Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer, false);
            Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
            Physics2D.IgnoreLayerCollision(enemyLayer, wallLayer, false);
            Physics2D.IgnoreLayerCollision(enemyLayer, groundLayer, false);

            Debug.Log("Physics2D layer collisions configured");
        }

        private static bool TagExists(string tag)
        {
            try
            {
                GameObject.FindGameObjectWithTag(tag);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

