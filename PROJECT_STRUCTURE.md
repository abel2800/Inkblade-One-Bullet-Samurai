# Project Structure

Complete repository structure for INKBLADE: ONE BULLET SAMURAI.

```
inkblade-one-bullet-samurai/
│
├── .gitignore                    # Git ignore rules for Unity
├── .gitattributes                # Git LFS configuration
├── LICENSE                       # MIT License
├── README.md                     # Main project README
├── CONTRIBUTING.md               # Contribution guidelines
├── PROJECT_STRUCTURE.md          # This file
│
├── Assets/                       # Unity project assets
│   ├── Scripts/                  # C# scripts
│   │   ├── Player/               # Player-related scripts
│   │   │   └── .gitkeep
│   │   ├── Weapons/              # Bullet and weapon scripts
│   │   │   └── .gitkeep
│   │   ├── Enemies/              # Enemy AI scripts
│   │   │   └── .gitkeep
│   │   ├── UI/                   # UI management scripts
│   │   │   └── .gitkeep
│   │   ├── Systems/              # Core system scripts
│   │   │   └── .gitkeep
│   │   └── Utils/                # Utility scripts
│   │       └── .gitkeep
│   │
│   ├── Prefabs/                  # Unity prefabs
│   │   └── .gitkeep
│   │
│   ├── Scenes/                   # Unity scene files
│   │   └── .gitkeep
│   │
│   ├── Art/                      # Art assets
│   │   ├── Characters/           # Character sprites
│   │   │   └── .gitkeep
│   │   ├── Weapons/              # Weapon sprites
│   │   │   └── .gitkeep
│   │   ├── Effects/              # Particle effects
│   │   │   └── .gitkeep
│   │   ├── UI/                   # UI sprites
│   │   │   └── .gitkeep
│   │   └── Backgrounds/          # Background images
│   │       └── .gitkeep
│   │
│   └── Audio/                    # Audio assets
│       ├── SFX/                  # Sound effects
│       │   └── .gitkeep
│       └── Music/                # Background music
│           └── .gitkeep
│
├── Builds/                       # Compiled game builds
│   ├── Windows/                  # Windows executables
│   │   └── .gitkeep
│   └── WebGL/                    # WebGL builds
│       └── .gitkeep
│
├── backend/                      # Optional backend code
│   ├── src/                      # Backend source code
│   │   └── .gitkeep
│   ├── Dockerfile                # Docker configuration
│   ├── docker-compose.yml        # Docker Compose setup
│   ├── .gitignore                # Backend-specific gitignore
│   ├── .env.example              # Environment variables template
│   ├── package.json.example      # Node.js dependencies (example)
│   └── README.md                 # Backend documentation
│
├── docs/                         # Documentation
│   ├── images/                   # Documentation images
│   │   └── .gitkeep
│   ├── setup.md                  # Setup instructions
│   ├── architecture.md           # System architecture
│   ├── api.md                    # Backend API documentation
│   ├── art-pipeline.md           # Art creation process
│   ├── CHANGELOG.md              # Version changelog
│   └── ROADMAP.md                # Development roadmap
│
└── .github/                      # GitHub configuration
    ├── ISSUE_TEMPLATE/           # Issue templates
    │   ├── bug_report.md
    │   ├── feature_request.md
    │   └── question.md
    ├── workflows/                # GitHub Actions
    │   └── ci.yml.example        # CI/CD example
    └── pull_request_template.md  # PR template
```

## 📁 Directory Descriptions

### Assets/
Unity project assets. All game content goes here.

### Assets/Scripts/
C# scripts organized by system:
- **Player/**: Player controller, movement, health
- **Weapons/**: Bullet system, shooting, retrieval
- **Enemies/**: Enemy AI, pathfinding, behavior
- **UI/**: UI managers, menus, HUD
- **Systems/**: Core systems (GameManager, AudioManager, SaveManager)
- **Utils/**: Helper classes, extensions, utilities

### Assets/Prefabs/
Unity prefabs for game objects (Player, Bullet, Enemies, UI elements).

### Assets/Scenes/
Unity scene files:
- MainMenu.unity
- Level_Select.unity
- Level_Play.unity
- GameOver.unity
- Settings.unity

### Assets/Art/
All visual assets organized by type.

### Assets/Audio/
Sound effects and music files.

### Builds/
Compiled game builds for distribution.

### backend/
Optional backend implementation (Node.js/Express or ASP.NET Core).

### docs/
Complete project documentation.

### .github/
GitHub-specific files (templates, workflows).

## 🚀 Next Steps

1. **Initialize Git Repository:**
   ```bash
   git init
   git lfs install
   git add .
   git commit -m "Initial commit: Project structure setup"
   ```

2. **Create GitHub Repository:**
   - Create new repo on GitHub
   - Add remote:
     ```bash
     git remote add origin https://github.com/yourusername/inkblade-one-bullet-samurai.git
     git push -u origin main
     ```

3. **Open in Unity:**
   - Open Unity Hub
   - Add project folder
   - Unity will create necessary Unity-specific folders

4. **Start Development:**
   - Follow [docs/ROADMAP.md](docs/ROADMAP.md)
   - Begin with Epic 1: Core Game Loop

## 📝 Notes

- `.gitkeep` files ensure empty directories are tracked by Git
- Unity will generate additional folders (Library, Temp, etc.) - these are in `.gitignore`
- Backend is optional - remove `backend/` folder if not implementing
- All documentation is in `docs/` folder

