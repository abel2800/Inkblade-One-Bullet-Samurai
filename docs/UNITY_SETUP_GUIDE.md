# Unity Setup Guide

Step-by-step guide for setting up the Unity project.

## 🚀 Quick Setup

### 1. Open Project in Unity

1. Open **Unity Hub**
2. Click **"Add"** button
3. Navigate to your project folder: `d:\New folder\game`
4. Select the folder
5. Unity will detect the project and show it in the list
6. Click **"Open"** to launch Unity Editor

**Note:** First import may take a few minutes as Unity processes assets.

### 2. Use Automated Setup (Recommended)

Once Unity is open, use the automated setup tools:

1. Go to menu: **Inkblade > Setup Project**
2. Click **"Setup All"** button
3. This will automatically:
   - Create all required tags
   - Set up all layers
   - Configure Physics2D layer collisions

### 3. Create Prefabs (Automated)

1. Go to menu: **Inkblade > Create Prefabs**
2. Click **"Create All Prefabs"** button
3. This will create:
   - Player prefab
   - Bullet prefab
   - Enemy prefab

**Note:** You'll still need to assign sprites manually.

### 4. Manual Setup (If Automated Fails)

#### Tags Setup

1. Go to **Edit > Project Settings > Tags and Layers**
2. Add these tags:
   - Player
   - Enemy
   - Bullet
   - Wall
   - Ground

#### Layers Setup

1. In the same window, set these layers:
   - Layer 8: Player
   - Layer 9: Enemy
   - Layer 10: Bullet
   - Layer 11: Wall
   - Layer 12: Ground

#### Physics2D Layer Collisions

1. Go to **Edit > Project Settings > Physics2D**
2. Configure layer collisions:
   - **Player** can collide with: Enemy, Wall, Ground
   - **Bullet** can collide with: Enemy, Wall, Ground
   - **Enemy** can collide with: Player, Wall, Ground

## 📦 Import TextMeshPro

1. Go to **Window > TextMeshPro > Import TMP Essential Resources**
2. Click **"Import"** in the dialog
3. (Optional) Import Examples & Extras

## 🎮 Create Scenes

### MainMenu Scene

1. **File > New Scene**
2. Choose **Basic (Built-in)**
3. Save as: `Assets/Scenes/MainMenu.unity`

4. **Create UI:**
   - Right-click in Hierarchy > **UI > Canvas**
   - Right-click Canvas > **UI > Panel** (rename to "MainMenuPanel")
   - Right-click MainMenuPanel > **UI > Button - TextMeshPro** (create 3 buttons: Play, Settings, Quit)

5. **Add UIManager:**
   - Create empty GameObject: "UIManager"
   - Add **UIManager** component
   - Assign MainMenuPanel to UIManager

6. **Configure Buttons:**
   - Select Play button
   - Add **MainMenuButton** component
   - Set Button Type to "Play"
   - Connect OnClick to MainMenuButton.OnButtonClick

### Level_Play Scene

1. **File > New Scene**
2. Choose **Basic (Built-in)**
3. Save as: `Assets/Scenes/Level_Play.unity`

4. **Set up Camera:**
   - Select Main Camera
   - Position: (0, 0, -10)
   - Projection: Orthographic
   - Size: 5
   - Background: Light gray/beige

5. **Create Ground:**
   - Create GameObject: "Ground"
   - Add **SpriteRenderer** component
   - Add **BoxCollider2D** component
   - Set Layer to "Ground"
   - Position at bottom of screen
   - Scale to cover bottom of screen

6. **Create Walls:**
   - Create left and right walls (similar to ground)
   - Set Layer to "Wall"

7. **Add Systems:**
   - Create empty GameObject: "GameManager"
     - Add **GameManager** component
   - Create empty GameObject: "BulletManager"
     - Add **BulletManager** component
     - Assign Bullet prefab
   - Create empty GameObject: "AudioManager"
     - Add **AudioManager** component
   - Create empty GameObject: "SaveManager"
     - Add **SaveManager** component
   - Create empty GameObject: "ParticleManager"
     - Add **ParticleManager** component
   - Create empty GameObject: "EnemySpawner"
     - Add **EnemySpawner** component
     - Assign Enemy prefab

8. **Add Player:**
   - Drag Player prefab into scene
   - Position at (0, 0, 0)
   - Assign to GameManager

9. **Add UI:**
   - Create Canvas
   - Add **UIManager** component
   - Create HUD panel
   - Create PauseMenu panel
   - Create GameOverMenu panel

10. **Add Camera Controller:**
    - Select Main Camera
    - Add **CameraController** component
    - Assign Player as target

## ✅ Verification Checklist

- [ ] Project opens without errors
- [ ] Tags are set up correctly
- [ ] Layers are configured
- [ ] Physics2D collisions are set
- [ ] TextMeshPro is imported
- [ ] Prefabs are created
- [ ] MainMenu scene is created
- [ ] Level_Play scene is created
- [ ] All systems are added to Level_Play
- [ ] No console errors

## 🐛 Troubleshooting

### Scripts Not Compiling

- Check Unity version (2022.3 LTS or newer)
- Verify all namespaces are correct
- Check for missing dependencies

### Prefabs Not Found

- Ensure prefabs are in `Assets/Prefabs/` folder
- Check prefab references in managers

### Physics Not Working

- Verify layer collisions are configured
- Check colliders are enabled
- Ensure Rigidbody2D components are set correctly

### UI Not Showing

- Check Canvas is set to Screen Space - Overlay
- Verify UI panels are active
- Check TextMeshPro is imported

## 📝 Next Steps

After setup is complete:

1. Assign sprites to prefabs
2. Configure audio clips
3. Test gameplay
4. Add art assets
5. Polish and iterate

---

**Need Help?** Check the main [Setup Guide](setup.md) or [Architecture Documentation](architecture.md).

