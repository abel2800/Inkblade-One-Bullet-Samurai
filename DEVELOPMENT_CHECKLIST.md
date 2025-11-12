# Development Checklist

Quick reference checklist for INKBLADE: ONE BULLET SAMURAI development.

## ✅ Pre-Development Setup

- [ ] Git repository cloned
- [ ] Git LFS installed and configured
- [ ] Unity Hub installed
- [ ] Unity LTS version installed (2022.3+)
- [ ] Visual Studio or VS Code with C# extensions
- [ ] Node.js installed (for backend)
- [ ] PostgreSQL installed (for backend, optional)

## 🎮 Unity Setup

- [ ] Project opened in Unity Hub
- [ ] Unity Editor launched successfully
- [ ] No compilation errors
- [ ] Run `Inkblade > Setup Project` → "Setup All"
- [ ] Tags created (Player, Enemy, Bullet, Wall, Ground)
- [ ] Layers configured (Player, Enemy, Bullet, Wall, Ground)
- [ ] Physics2D layer collisions configured
- [ ] TextMeshPro imported

## 📦 Prefabs

- [ ] Run `Inkblade > Create Prefabs` → "Create All Prefabs"
- [ ] Player prefab created
- [ ] Bullet prefab created
- [ ] Enemy prefab created
- [ ] Assign sprites to prefabs
- [ ] Configure component settings
- [ ] Test prefabs in scene

## 🎬 Scenes

- [ ] MainMenu scene created
- [ ] MainMenu UI set up
- [ ] UIManager configured
- [ ] Level_Play scene created
- [ ] Ground and walls created
- [ ] Player spawn point set
- [ ] Enemy spawn points set
- [ ] All managers added (GameManager, BulletManager, etc.)
- [ ] Camera configured
- [ ] UI panels created

## 🎨 Art Assets

- [ ] Player sprite created/imported
- [ ] Enemy sprite created/imported
- [ ] Bullet sprite created/imported
- [ ] Background texture created/imported
- [ ] UI sprites created/imported
- [ ] Particle effect sprites created/imported
- [ ] All sprites assigned to prefabs

## 🔊 Audio Assets

- [ ] Shoot sound effect
- [ ] Retrieve sound effect
- [ ] Dash sound effect
- [ ] Enemy hit sound
- [ ] Enemy death sound
- [ ] Player hit sound
- [ ] Main theme music
- [ ] All audio assigned to AudioManager

## 🧪 Testing

### Core Gameplay
- [ ] Player movement works
- [ ] Dash mechanic works
- [ ] Bullet shooting works
- [ ] Bullet retrieval works
- [ ] Enemy AI works
- [ ] Enemy spawning works
- [ ] Collisions work correctly

### UI Systems
- [ ] Main menu navigation works
- [ ] HUD displays correctly
- [ ] Pause menu works
- [ ] Game over screen works
- [ ] Settings menu works
- [ ] Volume controls work

### Polish
- [ ] Camera shake works
- [ ] Particle effects work
- [ ] Slow motion works
- [ ] Audio plays correctly

### Backend (Optional)
- [ ] Backend server running
- [ ] Database connected
- [ ] User registration works
- [ ] User login works
- [ ] Score submission works
- [ ] Leaderboard displays

## 🚀 Build & Release

- [ ] Windows build created
- [ ] Windows build tested
- [ ] WebGL build created (optional)
- [ ] WebGL build tested (optional)
- [ ] Builds uploaded to GitHub Releases
- [ ] README updated with screenshots
- [ ] Gameplay trailer created
- [ ] GitHub release created

## 📝 Code Quality

- [ ] No compiler errors
- [ ] No compiler warnings (or acceptable warnings)
- [ ] Code follows naming conventions
- [ ] Public methods documented
- [ ] No memory leaks
- [ ] Performance acceptable

## 🐛 Bug Fixes

- [ ] All known bugs fixed
- [ ] Edge cases handled
- [ ] Error messages clear
- [ ] Game doesn't crash

## 📚 Documentation

- [ ] README updated
- [ ] Setup guide complete
- [ ] Architecture documented
- [ ] API documented (if backend)
- [ ] Code comments added
- [ ] Changelog updated

## ✅ Final Checklist

- [ ] All systems tested
- [ ] All features working
- [ ] Performance optimized
- [ ] Art assets complete
- [ ] Audio assets complete
- [ ] Documentation complete
- [ ] Builds ready
- [ ] Ready for release

---

**Use this checklist to track your progress!**

