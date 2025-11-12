# Quick Start Guide

Get up and running with INKBLADE: ONE BULLET SAMURAI in minutes!

## 🚀 For Developers

### 1. Clone & Setup (2 minutes)

```bash
# Clone repository
git clone https://github.com/abel2800/Inkblade-One-Bullet-Samurai.git
cd Inkblade-One-Bullet-Samurai

# Setup Git LFS
git lfs install
git lfs pull
```

### 2. Open in Unity (1 minute)

1. Open **Unity Hub**
2. Click **"Add"** → Select project folder
3. Click **"Open"** → Wait for import

### 3. Automated Setup (30 seconds)

Once Unity is open:

1. **Menu:** `Inkblade > Setup Project` → Click **"Setup All"**
2. **Menu:** `Inkblade > Create Prefabs` → Click **"Create All Prefabs"**

✅ Done! Tags, layers, and prefabs are ready.

### 4. Create Scenes (5 minutes)

Follow: `docs/UNITY_SETUP_GUIDE.md`

**Quick version:**
- Create MainMenu scene → Add UI → Add UIManager
- Create Level_Play scene → Add systems → Add player

### 5. Test (1 minute)

- Press **Play** in Unity
- Test movement (WASD)
- Test dash (Space)
- Test shooting (Left Click)
- Test retrieval (E key)

## 🔧 For Backend Setup

### Quick Setup (5 minutes)

**Windows:**
```bash
cd backend
scripts\setup-backend.bat
```

**Mac/Linux:**
```bash
cd backend
chmod +x scripts/setup-backend.sh
./scripts/setup-backend.sh
```

**Manual:**
```bash
cd backend
npm install
cp .env.example .env
# Edit .env with your settings
createdb inkblade_db
npm run migrate
npm run dev
```

## 📦 Building

### Windows Build

```bash
scripts\build-windows.bat
```

### WebGL Build

```bash
chmod +x scripts/build-webgl.sh
./scripts/build-webgl.sh
```

## 🎮 Controls

- **Movement:** WASD or Arrow Keys
- **Dash:** Space
- **Shoot:** Left Mouse Click
- **Retrieve Bullet:** E
- **Pause:** Escape

## 🛠️ Development Tools

### Editor Menu

- `Inkblade > Setup Project` - Automated project setup
- `Inkblade > Create Prefabs` - Automated prefab creation

### Cheat Codes (Development Only)

- **G** - God mode (invulnerability)
- **B** - Infinite bullet
- **S** - Add 1000 score
- **K** - Kill all enemies
- **H** - Heal player

## 📚 Documentation

- **[PROJECT_SUMMARY.md](PROJECT_SUMMARY.md)** - Complete overview
- **[DEVELOPMENT_CHECKLIST.md](DEVELOPMENT_CHECKLIST.md)** - Development checklist
- **[docs/UNITY_SETUP_GUIDE.md](docs/UNITY_SETUP_GUIDE.md)** - Detailed Unity setup
- **[docs/BACKEND_SETUP.md](docs/BACKEND_SETUP.md)** - Backend setup
- **[docs/FULL_PROJECT_TODO.md](docs/FULL_PROJECT_TODO.md)** - Full task list

## ⚡ Common Tasks

### Add a New Feature

1. Create script in appropriate folder (`Assets/Scripts/`)
2. Use proper namespace (`Inkblade.*`)
3. Test in Unity
4. Commit: `git commit -m "feat: Description"`

### Fix a Bug

1. Reproduce bug
2. Fix in code
3. Test fix
4. Commit: `git commit -m "fix: Description"`

### Update Documentation

1. Edit relevant `.md` file
2. Commit: `git commit -m "docs: Description"`

## 🐛 Troubleshooting

### Unity Won't Open

- Check Unity version (2022.3 LTS recommended)
- Verify project folder is correct
- Check for file permissions

### Scripts Not Compiling

- Check Unity Console for errors
- Verify all namespaces are correct
- Ensure TextMeshPro is imported

### Backend Won't Start

- Check Node.js is installed: `node --version`
- Verify `.env` file exists and is configured
- Check PostgreSQL is running
- Check port 3000 is available

### Build Fails

- Check Unity path in build script
- Verify project compiles without errors
- Check build log for details

## ✅ Verification

After setup, verify:

- [ ] Unity opens without errors
- [ ] No compilation errors
- [ ] Tags and layers are set
- [ ] Prefabs are created
- [ ] Scenes can be created
- [ ] Backend runs (if using)

## 🎯 Next Steps

1. **Add Art Assets** - Create or import sprites
2. **Add Audio** - Import sound effects and music
3. **Test Gameplay** - Play and iterate
4. **Polish** - Fine-tune game feel
5. **Build** - Create release builds

---

**Need Help?** Check the full documentation in the `docs/` folder!

**Ready to Code?** All systems are ready - just add art and test! 🎮

