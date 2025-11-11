# Backend Setup Guide

Complete guide for setting up and running the INKBLADE backend API.

## 🚀 Quick Start

### Prerequisites

- Node.js 18+ installed
- PostgreSQL 14+ installed and running
- npm or yarn package manager

### Installation

1. **Navigate to backend directory:**
   ```bash
   cd backend
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Set up environment variables:**
   ```bash
   cp .env.example .env
   # Edit .env with your configuration
   ```

4. **Configure .env file:**
   ```env
   PORT=3000
   NODE_ENV=development
   DB_HOST=localhost
   DB_PORT=5432
   DB_NAME=inkblade_db
   DB_USER=postgres
   DB_PASSWORD=your_password
   JWT_SECRET=your_secret_key_here
   JWT_EXPIRES_IN=7d
   CORS_ORIGIN=http://localhost:8080
   ```

5. **Set up database:**
   ```bash
   # Create database
   createdb inkblade_db
   
   # Run migrations
   npm run migrate
   ```

6. **Start the server:**
   ```bash
   # Development mode (with auto-reload)
   npm run dev
   
   # Production mode
   npm start
   ```

## 🐳 Docker Setup

### Using Docker Compose

1. **Start services:**
   ```bash
   docker-compose up -d
   ```

2. **Run migrations:**
   ```bash
   docker exec -it inkblade_backend npm run migrate
   ```

3. **Check logs:**
   ```bash
   docker-compose logs -f
   ```

## 📋 API Endpoints

### Authentication

- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login user

### Scores

- `POST /api/score` - Submit score (requires auth)
- `GET /api/score/best` - Get user's best score (requires auth)

### Leaderboard

- `GET /api/leaderboard` - Get leaderboard
  - Query params: `limit`, `offset`, `levelId`

### Statistics

- `GET /api/stats/user/:userId` - Get user statistics (requires auth)

### Analytics

- `POST /api/analytics` - Submit analytics event (requires auth)

### Health

- `GET /api/health` - Health check

## 🔐 Authentication

Most endpoints require JWT authentication. Include token in Authorization header:

```
Authorization: Bearer <your_jwt_token>
```

## 🧪 Testing

```bash
# Run tests
npm test

# Run with coverage
npm run test:coverage
```

## 📝 Database Schema

### Users Table
- `id` (UUID, Primary Key)
- `username` (VARCHAR, Unique)
- `email` (VARCHAR, Unique)
- `password_hash` (VARCHAR)
- `created_at` (TIMESTAMP)
- `updated_at` (TIMESTAMP)

### Scores Table
- `id` (UUID, Primary Key)
- `user_id` (UUID, Foreign Key)
- `score` (INTEGER)
- `level_id` (INTEGER)
- `time_elapsed` (FLOAT)
- `enemies_killed` (INTEGER)
- `deaths` (INTEGER)
- `created_at` (TIMESTAMP)

### Analytics Table
- `id` (UUID, Primary Key)
- `user_id` (UUID, Foreign Key, Nullable)
- `event_type` (VARCHAR)
- `metadata` (JSONB)
- `created_at` (TIMESTAMP)

## 🚀 Deployment

### Environment Variables for Production

```env
NODE_ENV=production
PORT=3000
DB_HOST=your_production_db_host
DB_PORT=5432
DB_NAME=inkblade_db
DB_USER=your_db_user
DB_PASSWORD=your_secure_password
JWT_SECRET=your_very_secure_secret_key
CORS_ORIGIN=https://your-frontend-domain.com
```

### Deploy to Render/Fly.io/Railway

1. **Set up database** on your hosting provider
2. **Set environment variables** in hosting dashboard
3. **Deploy code** (connect GitHub repo or push code)
4. **Run migrations** after first deployment

## 🔧 Troubleshooting

### Database Connection Issues

- Verify PostgreSQL is running: `pg_isready`
- Check connection credentials in `.env`
- Ensure database exists: `psql -l | grep inkblade_db`

### Port Already in Use

- Change `PORT` in `.env` file
- Or kill process using port: `lsof -ti:3000 | xargs kill`

### Migration Errors

- Ensure database exists
- Check user has CREATE TABLE permissions
- Verify PostgreSQL version is 14+

## 📚 Additional Resources

- [Express.js Documentation](https://expressjs.com/)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/)
- [JWT Documentation](https://jwt.io/)

