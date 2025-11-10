# Quick Setup Guide

## 🚀 Initial Git Setup

### 1. Initialize Git Repository

```bash
# Navigate to project directory
cd "d:\New folder\game"

# Initialize Git
git init

# Configure Git LFS (for large assets)
git lfs install

# Add all files
git add .

# Create initial commit
git commit -m "Initial commit: Complete project structure setup"
```

### 2. Create GitHub Repository

1. Go to [GitHub](https://github.com) and create a new repository
2. Name it: `inkblade-one-bullet-samurai`
3. **DO NOT** initialize with README, .gitignore, or license (we already have them)
4. Copy the repository URL

### 3. Connect to GitHub

```bash
# Add remote (replace with your GitHub username)
git remote add origin https://github.com/YOUR_USERNAME/inkblade-one-bullet-samurai.git

# Rename branch to main (if needed)
git branch -M main

# Push to GitHub
git push -u origin main
```

## 🎮 Unity Setup

### 1. Open Project in Unity

1. Open **Unity Hub**
2. Click **"Add"** button
3. Navigate to: `d:\New folder\game`
4. Select the folder
5. Unity will detect it and show in projects list
6. Click **"Open"** to launch Unity Editor

**Note:** First time opening will take a few minutes as Unity imports assets.

### 2. Configure Unity Project

1. **Set Target Platform:**
   - `File > Build Settings`
   - Select **Windows** (or your target platform)
   - Click **"Switch Platform"**

2. **Project Settings:**
   - `Edit > Project Settings > Player`
   - Set **Company Name** and **Product Name**
   - Configure resolution settings

3. **Scripting Backend:**
   - `Edit > Project Settings > Player > Other Settings`
   - Set **Scripting Backend** to **IL2CPP** (for Windows builds)

## 📦 Backend Setup (Optional)

If you plan to implement the backend:

### 1. Setup Node.js Backend

```bash
cd backend

# Copy environment template
# Create .env file manually with these variables:
# PORT=3000
# NODE_ENV=development
# DB_HOST=localhost
# DB_PORT=5432
# DB_NAME=inkblade_db
# DB_USER=postgres
# DB_PASSWORD=your_password
# JWT_SECRET=your_secret_here

# Install dependencies (when package.json is created)
npm install
```

### 2. Setup with Docker

```bash
cd backend

# Start PostgreSQL and backend
docker-compose up -d

# Check if services are running
docker-compose ps
```

## ✅ Verification Checklist

- [ ] Git repository initialized
- [ ] Git LFS installed and configured
- [ ] All files committed locally
- [ ] GitHub repository created and connected
- [ ] Unity project opens without errors
- [ ] Unity project settings configured
- [ ] Backend setup (if applicable)

## 📝 Next Steps

1. **Start Development:**
   - Follow [docs/ROADMAP.md](docs/ROADMAP.md)
   - Begin with Epic 1: Core Game Loop

2. **Daily Workflow:**
   ```bash
   # Make changes
   # Test in Unity
   
   # Commit changes
   git add .
   git commit -m "feat: Add player movement system"
   
   # Push to GitHub
   git push origin main
   ```

3. **Create Issues:**
   - Use GitHub issue templates
   - Track progress with milestones

## 🆘 Troubleshooting

### Git LFS Issues

If large files aren't tracked:
```bash
git lfs migrate import --include="*.png,*.wav,*.unity" --everything
```

### Unity Issues

- **Scripts not compiling:** Check Unity version matches requirements
- **Assets missing:** Run `git lfs pull` to download large files
- **Build errors:** Verify Build Settings platform is correct

### Backend Issues

- **Database connection fails:** Check Docker containers are running
- **Port already in use:** Change PORT in .env file

## 📚 Documentation

- [Setup Guide](docs/setup.md) - Detailed setup instructions
- [Architecture](docs/architecture.md) - System design
- [API Documentation](docs/api.md) - Backend API (if applicable)
- [Art Pipeline](docs/art-pipeline.md) - Asset creation
- [Contributing](CONTRIBUTING.md) - Development guidelines
- [Roadmap](docs/ROADMAP.md) - Development plan

## 🎯 Ready to Start!

Your project structure is complete and ready for development. Start implementing features following the roadmap!

