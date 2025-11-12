# INKBLADE: ONE BULLET SAMURAI

> A 2D action game with tight controls and a unique one-bullet mechanic, featuring a black & white Sumi-e ink aesthetic.

![Gameplay Screenshot/GIF](docs/images/gameplay.gif)

## 🎮 Key Features

- **One Bullet Mechanic**: Shoot, retrieve, repeat — master the art of precision
- **Tight Controls**: Smooth movement with dash mechanics and invulnerability frames
- **Sumi-e Aesthetic**: Beautiful black & white ink art style
- **Satisfying Combat**: Particle effects, camera shake, and polished game feel
- **Optional Backend**: Leaderboards, analytics, and cloud saves

## 🛠️ Tech Stack

**Client:**
- Unity (LTS version)
- C#
- Rigidbody2D physics
- Object pooling for performance

**Backend (Optional):**
- Node.js + Express + PostgreSQL (or ASP.NET Core + PostgreSQL)
- Docker for containerization
- JWT authentication
- Deployed on Render/Fly.io/Railway

## 🚀 Quick Start

### Prerequisites

1. **Unity & Unity Hub**
   - Install Unity Hub
   - Install latest LTS Unity version (2022.3+ recommended)
   - Add modules: Windows Build Support (IL2CPP), WebGL Build Support (optional)

2. **Visual Studio Community / Visual Studio Code**
   - Visual Studio Community with 'Game development with Unity' workload
   - Or VS Code with C# extension and Unity Debugger extensions

3. **Git and Git LFS**
   ```bash
   git lfs install
   ```

4. **Node.js & npm** (for backend, optional)
   ```bash
   node --version
   npm --version
   ```

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/abel2800/Inkblade-One-Bullet-Samurai.git
   cd Inkblade-One-Bullet-Samurai
   git lfs pull
   ```

2. **Open in Unity**
   - Open Unity Hub
   - Click "Add" and select the project folder
   - Unity will import assets (first time may take a few minutes)

3. **Automated Setup** (Recommended)
   - Once Unity is open, go to menu: `Inkblade > Setup Project`
   - Click "Setup All" to configure tags, layers, and physics
   - Go to menu: `Inkblade > Create Prefabs`
   - Click "Create All Prefabs" to create Player, Bullet, and Enemy prefabs

4. **Create Scenes**
   - Follow the guide in `docs/UNITY_SETUP_GUIDE.md`
   - Or see `QUICK_START.md` for quick instructions

5. **Run the game**
   - Open your Level_Play scene
   - Press Play in Unity Editor

### Building

**Windows Build:**
```bash
# In Unity: File > Build Settings > Windows > Build
# Output will be in /Builds/Windows/
```

**WebGL Build:**
```bash
# In Unity: File > Build Settings > WebGL > Build
# Output will be in /Builds/WebGL/
```

## 📁 Project Structure

```
inkblade-one-bullet-samurai/
├── Assets/                    # Unity project assets
│   ├── Scripts/
│   │   ├── Player/           # Player controller, movement, dash
│   │   ├── Weapons/          # Bullet system, shooting, retrieval
│   │   ├── Enemies/          # Enemy AI, pathfinding, combat
│   │   ├── UI/               # UI managers, menus, HUD
│   │   ├── Systems/          # Audio, Save, Game Manager
│   │   └── Utils/            # Helpers, object pooling
│   ├── Prefabs/              # Game object prefabs
│   ├── Scenes/               # Unity scenes
│   ├── Art/                  # Sprites, textures, animations
│   └── Audio/                # SFX and music
├── Docs/                     # Documentation
│   ├── setup.md             # Detailed setup instructions
│   ├── architecture.md      # System architecture
│   ├── api.md               # Backend API documentation
│   └── art-pipeline.md      # Art creation process
├── Builds/                   # Compiled game builds
├── backend/                  # Optional backend code (if implemented)
│   ├── src/
│   ├── Dockerfile
│   └── docker-compose.yml
└── README.md
```

## 🎯 Development Roadmap

- [x] **Epic 1**: Core Game Loop (Player + Bullet + Enemies) ✅
- [x] **Epic 2**: Level & UI ✅
- [x] **Epic 3**: Polish & Quality ✅
- [x] **Epic 4**: Backend (Optional) ✅
- [x] **Epic 5**: Documentation & GitHub ✅

**Status:** Code Complete (~95%) | Unity Setup Pending

See [CONTRIBUTING.md](CONTRIBUTING.md) for detailed development guidelines.

## 📖 Documentation

- [Setup Guide](docs/setup.md) - Detailed installation and configuration
- [Architecture](docs/architecture.md) - System design and technical specs
- [API Documentation](docs/api.md) - Backend API endpoints (if implemented)
- [Art Pipeline](docs/art-pipeline.md) - Asset creation process

## 🎨 Art Credits

- Silhouette assets: [Kenney](https://kenney.nl/), [OpenGameArt](https://opengameart.org/)
- Brush effects: Custom created with Krita/Inkscape
- Audio: [Freesound](https://freesound.org/), [Kenney](https://kenney.nl/assets)

## 📝 License

MIT License - See [LICENSE](LICENSE) file for details

## 🤝 Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

## 📧 Contact

- GitHub: [@abel2800](https://github.com/abel2800)
- Repository: [Inkblade-One-Bullet-Samurai](https://github.com/abel2800/Inkblade-One-Bullet-Samurai)

## 🎬 Demo

**Coming Soon!** Builds will be available in [Releases](https://github.com/abel2800/Inkblade-One-Bullet-Samurai/releases)

## 🛠️ Development Tools

### Automated Setup
- **Menu:** `Inkblade > Setup Project` - Sets up tags, layers, physics
- **Menu:** `Inkblade > Create Prefabs` - Creates all game prefabs

### Build Scripts
- **Windows:** `scripts/build-windows.bat`
- **WebGL:** `scripts/build-webgl.sh`

### Backend Setup
- **Windows:** `scripts/setup-backend.bat`
- **Mac/Linux:** `scripts/setup-backend.sh`

See [QUICK_START.md](QUICK_START.md) for more details.

---

**Status**: 🚧 In Active Development

