using UnityEngine;
using UnityEditor;
using System.IO;

namespace Inkblade.Editor
{
    /// <summary>
    /// Generates simple placeholder sprites for development.
    /// </summary>
    public class PlaceholderSpriteGenerator : EditorWindow
    {
        [MenuItem("Inkblade/Generate Placeholder Sprites")]
        public static void ShowWindow()
        {
            GetWindow<PlaceholderSpriteGenerator>("Placeholder Sprite Generator");
        }

        private void OnGUI()
        {
            GUILayout.Label("Generate Placeholder Sprites", EditorStyles.boldLabel);
            GUILayout.Space(10);

            GUILayout.Label("This will create simple colored square sprites for:");
            GUILayout.Label("• Player (Blue)");
            GUILayout.Label("• Enemy (Red)");
            GUILayout.Label("• Bullet (White)");
            GUILayout.Label("• Wall (Gray)");
            GUILayout.Label("• Ground (Dark Gray)");
            GUILayout.Space(10);

            if (GUILayout.Button("Generate All Placeholders", GUILayout.Height(40)))
            {
                GenerateAllPlaceholders();
                EditorUtility.DisplayDialog("Success", "Placeholder sprites generated!", "OK");
            }

            GUILayout.Space(20);
            GUILayout.Label("Note: These are simple colored squares for testing.", EditorStyles.helpBox);
        }

        private static void GenerateAllPlaceholders()
        {
            // Create directories
            EnsureDirectoryExists("Assets/Art/Characters/Player");
            EnsureDirectoryExists("Assets/Art/Characters/Enemies");
            EnsureDirectoryExists("Assets/Art/Weapons");
            EnsureDirectoryExists("Assets/Art/Backgrounds");

            // Generate sprites
            CreatePlaceholderSprite("Assets/Art/Characters/Player/player_placeholder.png", 64, 64, Color.blue);
            CreatePlaceholderSprite("Assets/Art/Characters/Enemies/enemy_placeholder.png", 48, 48, Color.red);
            CreatePlaceholderSprite("Assets/Art/Weapons/bullet_placeholder.png", 16, 16, Color.white);
            CreatePlaceholderSprite("Assets/Art/Backgrounds/wall_placeholder.png", 32, 128, Color.gray);
            CreatePlaceholderSprite("Assets/Art/Backgrounds/ground_placeholder.png", 256, 32, new Color(0.3f, 0.3f, 0.3f));

            // Refresh asset database
            AssetDatabase.Refresh();

            Debug.Log("Placeholder sprites generated successfully!");
        }

        private static void CreatePlaceholderSprite(string path, int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

            // Fill with color
            Color[] pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }
            texture.SetPixels(pixels);
            texture.Apply();

            // Encode to PNG
            byte[] pngData = texture.EncodeToPNG();
            File.WriteAllBytes(path, pngData);

            // Import as sprite
            AssetDatabase.ImportAsset(path);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer != null)
            {
                importer.textureType = TextureImporterType.Sprite;
                importer.spritePixelsPerUnit = 100;
                importer.filterMode = FilterMode.Point;
                importer.textureCompression = TextureImporterCompression.Uncompressed;
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            }

            Object.DestroyImmediate(texture);
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                string parentPath = Path.GetDirectoryName(path).Replace('\\', '/');
                string folderName = Path.GetFileName(path);
                AssetDatabase.CreateFolder(parentPath, folderName);
            }
        }
    }
}

