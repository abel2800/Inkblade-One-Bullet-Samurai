# Backend API

Backend API for INKBLADE: ONE BULLET SAMURAI.

## 🚀 Quick Start

### 1. Create Database (Manual)

Create the PostgreSQL database:

```sql
CREATE DATABASE inkblade_db;
```

### 2. Configure Environment

The `.env` file is already created with:
- Database: inkblade_db
- User: postgres
- Password: 1992
- Host: localhost
- Port: 5432

### 3. Test Connection

```bash
npm run test-db
```

### 4. Run Migrations (Create Tables Automatically)

```bash
npm run migrate
```

This will automatically create:
- users table
- scores table
- analytics table
- All indexes and relationships

### 5. Start Server

```bash
npm run dev
```

Server runs on: http://localhost:3000

## 📋 API Endpoints

- `POST /api/auth/register` - Register user
- `POST /api/auth/login` - Login user
- `POST /api/score` - Submit score
- `GET /api/leaderboard` - Get leaderboard
- `GET /api/stats/user/:userId` - Get user stats
- `POST /api/analytics` - Submit analytics
- `GET /api/health` - Health check

## 📚 Documentation

- [API Documentation](../docs/api.md)
- [Backend Setup](../docs/BACKEND_SETUP.md)
- [Quick Database Setup](QUICK_DATABASE_SETUP.md)

## 🔧 Scripts

- `npm start` - Start production server
- `npm run dev` - Start development server (auto-reload)
- `npm run migrate` - Run database migrations
- `npm run test-db` - Test database connection
- `npm test` - Run tests

---

**Database Password:** 1992 (configured in .env)
