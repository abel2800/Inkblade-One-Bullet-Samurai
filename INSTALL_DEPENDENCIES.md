# Dependencies Installation Guide

Quick guide for installing all project dependencies.

## ✅ Installed Dependencies

### Backend Dependencies
All backend npm packages have been installed:
- express (^4.18.2)
- pg (^8.11.0) - PostgreSQL client
- jsonwebtoken (^9.0.2)
- bcrypt (^5.1.1)
- cors (^2.8.5)
- dotenv (^16.3.1)
- express-rate-limit (^6.10.0)
- joi (^17.11.0)
- nodemon (dev)
- jest (dev)
- supertest (dev)

**Status:** ✅ Installed (453 packages)

### Git LFS
**Status:** ✅ Installed (v3.6.0)

## 📋 Required Tools

### Already Installed
- ✅ Node.js v24.11.0
- ✅ npm v11.6.1
- ✅ Git LFS v3.6.0

### Still Needed (for full functionality)

#### Unity Dependencies
Unity will automatically install these when you open the project:
- TextMeshPro (import via Unity Package Manager)
- Unity Physics2D (built-in)
- Unity Input System (optional, using legacy Input Manager)

**To install TextMeshPro:**
1. Open Unity
2. Window > TextMeshPro > Import TMP Essential Resources
3. Click Import

#### Database (for backend)
- PostgreSQL 14+ (if using backend)
  - Download from: https://www.postgresql.org/download/
  - Or use Docker: `docker-compose up -d` (from backend folder)

#### Docker (optional, for backend)
- Docker Desktop (if using containerized backend)
  - Download from: https://www.docker.com/products/docker-desktop

## 🚀 Quick Setup Commands

### Backend Setup (if using)
```bash
cd backend
npm install  # Already done ✅
cp .env.example .env
# Edit .env with your settings
createdb inkblade_db  # If PostgreSQL installed
npm run migrate
```

### Unity Setup
1. Open Unity Hub
2. Add project folder
3. Open in Unity Editor
4. Import TextMeshPro (Window > TextMeshPro > Import TMP Essential Resources)

## ✅ Verification

Run these to verify installations:

```bash
# Check Node.js
node --version  # Should show v18+ or v20+

# Check npm
npm --version  # Should show v9+

# Check Git LFS
git lfs version  # Should show version

# Check backend dependencies
cd backend
npm list --depth=0  # Should show all packages
```

## 📝 Notes

- Backend dependencies are installed and ready
- Unity dependencies will install automatically when opening project
- PostgreSQL is only needed if using the backend
- Docker is optional but recommended for backend development

---

**All code dependencies are installed!** Ready for Unity setup and development.

