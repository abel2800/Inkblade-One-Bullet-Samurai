# Backend API

Node.js + Express + PostgreSQL backend for INKBLADE: ONE BULLET SAMURAI.

## Quick Setup

1. **Install dependencies:**
   ```bash
   npm install
   ```

2. **Create database:**
   ```sql
   CREATE DATABASE game;
   ```

3. **Configure `.env` file:**
   ```env
   DB_HOST=localhost
   DB_PORT=5432
   DB_NAME=game
   DB_USER=postgres
   DB_PASSWORD=your_password
   JWT_SECRET=your_secret
   ```

4. **Run migrations:**
   ```bash
   npm run migrate
   ```

5. **Start server:**
   ```bash
   npm run dev
   ```

## API Endpoints

- `POST /api/auth/register` - Register user
- `POST /api/auth/login` - Login user
- `POST /api/score` - Submit score
- `GET /api/leaderboard` - Get leaderboard
- `GET /api/stats/user/:userId` - Get user stats
- `POST /api/analytics` - Submit analytics

## Scripts

- `npm start` - Start production server
- `npm run dev` - Start development server
- `npm run migrate` - Run database migrations
- `npm run test-db` - Test database connection
