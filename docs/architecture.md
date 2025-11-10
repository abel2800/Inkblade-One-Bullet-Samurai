# Architecture Documentation

Technical architecture and system design for INKBLADE: ONE BULLET SAMURAI.

## 🏗️ High-Level Architecture

### Client (Unity)

```
┌─────────────────────────────────────────┐
│           Unity Client                  │
├─────────────────────────────────────────┤
│  Scenes: MainMenu, Level_Play, etc.    │
│  Core Systems: Player, Bullet, Enemy   │
│  Managers: UI, Audio, Save, Game        │
└─────────────────────────────────────────┘
                    │
                    ▼
┌─────────────────────────────────────────┐
│      Optional Backend API               │
│  (Leaderboards, Analytics, Auth)        │
└─────────────────────────────────────────┘
```

## 📦 Core Systems

### 1. Player System

**PlayerController.cs**
- Handles movement input (WASD/Arrow keys)
- Manages dash mechanics with cooldown
- Handles bullet retrieval interaction
- Manages health and invulnerability frames

**Key Components:**
- `Rigidbody2D` - Physics-based movement
- `Collider2D` - Collision detection
- `Animator` - Animation state machine

**Movement:**
```csharp
// Pseudocode
void Update() {
    Vector2 input = GetInput();
    Move(input);
    if (DashInput() && CanDash()) {
        StartCoroutine(Dash());
    }
}
```

### 2. Bullet System

**Bullet.cs**
- Manages bullet physics and collision
- Handles "sticking" to surfaces/enemies
- Tracks retrieval state

**BulletManager.cs**
- Singleton managing bullet lifecycle
- Object pooling for performance
- Spawn/retrieve logic

**Key States:**
- `Flying` - Bullet in motion
- `Stuck` - Bullet stuck to surface/enemy
- `Retrievable` - Player can retrieve
- `Retrieved` - Back with player

### 3. Enemy System

**EnemyAI.cs**
- State machine: Idle → Pursue → Attack → Stagger → Die
- Pathfinding to player
- Damage dealing on contact

**EnemySpawner.cs**
- Manages enemy spawning
- Wave system
- Difficulty scaling

**Pathfinding:**
- Simple: Direct movement to player
- Advanced: A* pathfinding or Unity NavMesh (2D workaround)

### 4. UI System

**UIManager.cs**
- Manages all UI panels
- Handles menu navigation
- Updates HUD elements

**HUD Components:**
- Health bar
- Bullet indicator (available/retrievable)
- Score display
- Dash cooldown indicator

### 5. Audio System

**AudioManager.cs**
- Singleton for audio playback
- Manages SFX and music
- Volume controls
- Audio pooling

### 6. Save System

**SaveManager.cs**
- Local save using PlayerPrefs
- Settings persistence
- Progress tracking
- Optional cloud sync

## 🔄 Game Flow

```
MainMenu
    │
    ├─> Level Select
    │       │
    │       └─> Level Play
    │               │
    │               ├─> Pause Menu
    │               │       │
    │               │       ├─> Resume
    │               │       ├─> Settings
    │               │       └─> Main Menu
    │               │
    │               └─> Game Over
    │                       │
    │                       ├─> Retry
    │                       ├─> Level Select
    │                       └─> Main Menu
    │
    ├─> Settings
    │
    ├─> Leaderboard (if backend)
    │
    └─> Exit
```

## 🗄️ Data Flow

### Local Data

```
PlayerPrefs:
  - MasterVolume (float)
  - MusicVolume (float)
  - SFXVolume (float)
  - Sensitivity (float)
  - HighScore (int)
  - LevelProgress (int)
```

### Backend Data (Optional)

```
API Endpoints:
  POST /api/auth/register
  POST /api/auth/login
  POST /api/score
  GET  /api/leaderboard
  GET  /api/stats/user/{id}
```

## 🎯 Performance Optimizations

### Object Pooling

- **Bullets:** Pool of bullet prefabs
- **Particles:** Pool of particle effects
- **Enemies:** Pool of enemy prefabs (if applicable)

### Rendering

- **Sprite Atlases:** Combine sprites to reduce draw calls
- **Batching:** Use Static Batching for static objects
- **Culling:** Optimize camera culling settings

### Physics

- **Layer Collisions:** Configure Physics2D layers efficiently
- **Collider Optimization:** Use simple colliders where possible

## 🔐 Security Considerations

### Client-Side

- Input validation
- Anti-cheat measures (server-side validation for scores)
- Secure storage of sensitive data

### Backend (if implemented)

- JWT token authentication
- Password hashing (bcrypt/argon2)
- Rate limiting on API endpoints
- Input sanitization
- CORS configuration

## 📊 System Dependencies

```
PlayerController
    ├─> BulletManager
    ├─> AudioManager
    └─> UIManager

EnemyAI
    ├─> PlayerController (for targeting)
    └─> AudioManager

BulletManager
    ├─> ObjectPool
    └─> AudioManager

UIManager
    ├─> GameManager
    └─> SaveManager

GameManager
    ├─> EnemySpawner
    ├─> SaveManager
    └─> AudioManager
```

## 🧪 Testing Strategy

### Unit Tests

- Pure C# logic (game rules, calculations)
- Use Unity Test Runner

### Integration Tests

- System interactions
- Scene loading
- Save/load functionality

### Manual Testing

- Gameplay feel
- Controls responsiveness
- Visual polish
- Performance on target platforms

## 📝 Code Organization

```
Assets/Scripts/
├── Player/
│   ├── PlayerController.cs
│   └── PlayerHealth.cs
├── Weapons/
│   ├── Bullet.cs
│   ├── BulletManager.cs
│   └── ObjectPool.cs
├── Enemies/
│   ├── EnemyAI.cs
│   ├── EnemySpawner.cs
│   └── EnemyHealth.cs
├── UI/
│   ├── UIManager.cs
│   ├── MainMenu.cs
│   └── HUD.cs
├── Systems/
│   ├── GameManager.cs
│   ├── AudioManager.cs
│   └── SaveManager.cs
└── Utils/
    ├── ObjectPool.cs
    └── Extensions.cs
```

## 🔄 Update Loop

```
Unity Update Loop:
  ├─> PlayerController.Update()
  ├─> EnemyAI.Update() (for each enemy)
  ├─> Bullet.Update() (for each bullet)
  └─> UIManager.Update()

FixedUpdate:
  ├─> Physics calculations
  └─> Movement updates
```

## 📚 Additional Resources

- [Unity Scripting API](https://docs.unity3d.com/ScriptReference/)
- [Unity Best Practices](https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity.html)
- [C# Design Patterns](https://refactoring.guru/design-patterns/csharp)
