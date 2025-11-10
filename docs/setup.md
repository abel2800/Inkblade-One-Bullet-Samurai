# Setup Guide

Complete installation and configuration instructions for INKBLADE: ONE BULLET SAMURAI development environment.

## 📋 Prerequisites

### Required Tools

1. **Unity & Unity Hub**
   - Download [Unity Hub](https://unity.com/download)
   - Install latest LTS Unity version (recommended: 2022.3 LTS or newer)
   - Required modules:
     - Windows Build Support (IL2CPP)
     - Android Build Support (if targeting mobile)
     - WebGL Build Support (optional, for web builds)
     - Visual Studio Community 2022 (or VS Code)

2. **Visual Studio Community / Visual Studio Code**
   - **Visual Studio Community** (Recommended):
     - Download from [Visual Studio Downloads](https://visualstudio.microsoft.com/downloads/)
     - Install with "Game development with Unity" workload
   - **Visual Studio Code** (Alternative):
     - Install [VS Code](https://code.visualstudio.com/)
     - Install extensions:
       - C# (by Microsoft)
       - Unity Code Snippets
       - Unity Debugger

3. **Git and Git LFS**
   ```bash
   # Install Git from https://git-scm.com/downloads
   # Install Git LFS from https://git-lfs.github.com/
   
   # Configure Git
   git config --global user.name "Your Name"
   git config --global user.email "your.email@example.com"
   
   # Initialize Git LFS
   git lfs install
   ```

4. **Node.js & npm** (for backend development)
   ```bash
   # Download from https://nodejs.org/
   # Verify installation
   node --version
   npm --version
   ```

5. **Docker** (for local backend)
   ```bash
   # Download from https://www.docker.com/products/docker-desktop
   # Verify installation
   docker --version
   docker-compose --version
   ```

### Optional Tools

- **Inkscape** - Vector graphics (silhouettes)
- **Krita** - Raster painting (brush effects)
- **GIMP** - Image editing
- **Audacity** - Audio editing
- **Postman** - API testing
- **GitHub Desktop** - Git GUI

## 🚀 Project Setup

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/inkblade-one-bullet-samurai.git
cd inkblade-one-bullet-samurai
```

### 2. Initialize Git LFS

```bash
git lfs install
git lfs pull
```

### 3. Open in Unity

1. Open **Unity Hub**
2. Click **"Add"** button
3. Navigate to the project folder and select it
4. Unity will detect the project and show it in the list
5. Click **"Open"** to launch Unity Editor

**Note:** First import may take several minutes as Unity processes assets.

### 4. Configure Unity Project

1. **Set Target Platform:**
   - Go to `File > Build Settings`
   - Select your target platform (Windows, WebGL, etc.)
   - Click **"Switch Platform"**

2. **Configure Project Settings:**
   - `Edit > Project Settings > Player`
   - Set Company Name and Product Name
   - Configure resolution and other settings

3. **Verify Scripting Backend:**
   - `Edit > Project Settings > Player > Other Settings`
   - Set Scripting Backend to **IL2CPP** (for Windows builds)

### 5. Backend Setup (Optional)

If implementing the backend:

```bash
cd backend
npm install
cp .env.example .env
# Edit .env with your configuration
```

**Using Docker:**

```bash
cd backend
docker-compose up -d
```

This will start:
- PostgreSQL database
- Backend API server
- Redis (if configured)

## ✅ Verification

### Unity Setup Verification

1. Open `Assets/Scenes/MainMenu.unity`
2. Press **Play** in Unity Editor
3. Check Console for errors (should be empty)

### Backend Setup Verification

```bash
# Test API health endpoint
curl http://localhost:3000/api/health

# Expected response:
# {"status":"ok","message":"API is running"}
```

## 🔧 Troubleshooting

### Unity Issues

**Problem:** Scripts not compiling
- **Solution:** Check Unity version matches project requirements
- Verify Visual Studio/VS Code extensions are installed
- Restart Unity Editor

**Problem:** Assets not loading
- **Solution:** Ensure Git LFS is installed and initialized
- Run `git lfs pull` to download large files

**Problem:** Build errors
- **Solution:** Check Build Settings platform is correct
- Verify all required modules are installed in Unity Hub

### Git LFS Issues

**Problem:** Large files not tracked
- **Solution:** Verify `.gitattributes` is in repository root
- Run `git lfs migrate import --include="*.png,*.wav"` if needed

### Backend Issues

**Problem:** Database connection fails
- **Solution:** Verify Docker containers are running
- Check `.env` file has correct database credentials
- Ensure PostgreSQL port (5432) is not in use

## 📚 Next Steps

- Read [Architecture Documentation](architecture.md)
- Review [API Documentation](api.md) (if using backend)
- Check [Art Pipeline Guide](art-pipeline.md)

## 🆘 Getting Help

If you encounter issues:

1. Check existing [GitHub Issues](https://github.com/yourusername/inkblade-one-bullet-samurai/issues)
2. Review Unity Console logs
3. Open a new issue with:
   - Unity version
   - OS and platform
   - Error messages/logs
   - Steps to reproduce
