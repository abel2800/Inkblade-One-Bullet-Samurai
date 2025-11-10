# API Documentation

Backend API documentation for INKBLADE: ONE BULLET SAMURAI (Optional Fullstack Feature).

## 🌐 Base URL

```
Development: http://localhost:3000
Production: https://api.inkblade-game.com
```

## 🔐 Authentication

Most endpoints require JWT authentication. Include the token in the Authorization header:

```
Authorization: Bearer <your_jwt_token>
```

## 📋 Endpoints

### Authentication

#### Register User

```http
POST /api/auth/register
Content-Type: application/json
```

**Request Body:**
```json
{
  "username": "player123",
  "email": "player@example.com",
  "password": "securePassword123"
}
```

**Response (201 Created):**
```json
{
  "id": "uuid-here",
  "username": "player123",
  "email": "player@example.com",
  "token": "jwt-token-here"
}
```

**Error Response (400 Bad Request):**
```json
{
  "error": "Validation failed",
  "details": ["Username already exists", "Email is invalid"]
}
```

#### Login User

```http
POST /api/auth/login
Content-Type: application/json
```

**Request Body:**
```json
{
  "email": "player@example.com",
  "password": "securePassword123"
}
```

**Response (200 OK):**
```json
{
  "id": "uuid-here",
  "username": "player123",
  "email": "player@example.com",
  "token": "jwt-token-here"
}
```

**Error Response (401 Unauthorized):**
```json
{
  "error": "Invalid credentials"
}
```

### Scores

#### Submit Score

```http
POST /api/score
Authorization: Bearer <token>
Content-Type: application/json
```

**Request Body:**
```json
{
  "score": 1250,
  "levelId": 1,
  "timeElapsed": 120.5,
  "enemiesKilled": 15,
  "deaths": 2
}
```

**Response (201 Created):**
```json
{
  "id": "score-uuid",
  "userId": "user-uuid",
  "score": 1250,
  "levelId": 1,
  "rank": 42,
  "createdAt": "2024-01-15T10:30:00Z"
}
```

#### Get Leaderboard

```http
GET /api/leaderboard?limit=10&levelId=1
```

**Query Parameters:**
- `limit` (optional, default: 10) - Number of entries to return
- `levelId` (optional) - Filter by level
- `offset` (optional, default: 0) - Pagination offset

**Response (200 OK):**
```json
{
  "leaderboard": [
    {
      "rank": 1,
      "username": "topPlayer",
      "score": 5000,
      "levelId": 1,
      "createdAt": "2024-01-15T09:00:00Z"
    },
    {
      "rank": 2,
      "username": "player123",
      "score": 4500,
      "levelId": 1,
      "createdAt": "2024-01-15T08:30:00Z"
    }
  ],
  "total": 150,
  "limit": 10,
  "offset": 0
}
```

#### Get User's Best Score

```http
GET /api/score/best?levelId=1
Authorization: Bearer <token>
```

**Response (200 OK):**
```json
{
  "score": 1250,
  "levelId": 1,
  "rank": 42,
  "createdAt": "2024-01-15T10:30:00Z"
}
```

### User Statistics

#### Get User Stats

```http
GET /api/stats/user/{userId}
Authorization: Bearer <token>
```

**Response (200 OK):**
```json
{
  "userId": "user-uuid",
  "username": "player123",
  "totalScore": 15000,
  "gamesPlayed": 25,
  "averageScore": 600,
  "bestScore": 2500,
  "totalEnemiesKilled": 150,
  "totalDeaths": 10,
  "favoriteLevel": 1,
  "playTime": 3600
}
```

### Analytics

#### Submit Analytics Event

```http
POST /api/analytics
Authorization: Bearer <token>
Content-Type: application/json
```

**Request Body:**
```json
{
  "eventType": "level_complete",
  "metadata": {
    "levelId": 1,
    "timeElapsed": 120.5,
    "score": 1250
  }
}
```

**Response (201 Created):**
```json
{
  "id": "analytics-uuid",
  "eventType": "level_complete",
  "createdAt": "2024-01-15T10:30:00Z"
}
```

**Event Types:**
- `game_start`
- `level_start`
- `level_complete`
- `level_fail`
- `enemy_killed`
- `bullet_retrieved`
- `dash_used`
- `game_over`

### Health Check

#### API Health

```http
GET /api/health
```

**Response (200 OK):**
```json
{
  "status": "ok",
  "message": "API is running",
  "timestamp": "2024-01-15T10:30:00Z",
  "version": "1.0.0"
}
```

## 🗄️ Database Schema

### Users Table

```sql
CREATE TABLE users (
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  username VARCHAR(50) UNIQUE NOT NULL,
  email VARCHAR(255) UNIQUE NOT NULL,
  password_hash VARCHAR(255) NOT NULL,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

### Scores Table

```sql
CREATE TABLE scores (
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  user_id UUID REFERENCES users(id) ON DELETE CASCADE,
  score INTEGER NOT NULL,
  level_id INTEGER NOT NULL,
  time_elapsed FLOAT,
  enemies_killed INTEGER,
  deaths INTEGER,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_scores_user_id ON scores(user_id);
CREATE INDEX idx_scores_score ON scores(score DESC);
CREATE INDEX idx_scores_level_id ON scores(level_id);
```

### Analytics Table

```sql
CREATE TABLE analytics (
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  user_id UUID REFERENCES users(id) ON DELETE SET NULL,
  event_type VARCHAR(50) NOT NULL,
  metadata JSONB,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_analytics_user_id ON analytics(user_id);
CREATE INDEX idx_analytics_event_type ON analytics(event_type);
CREATE INDEX idx_analytics_created_at ON analytics(created_at);
```

## 🔒 Security

### Rate Limiting

- Authentication endpoints: 5 requests per minute
- Score submission: 10 requests per minute
- Analytics: 100 requests per minute

### Input Validation

All inputs are validated server-side:
- Username: 3-50 characters, alphanumeric + underscore
- Email: Valid email format
- Password: Minimum 8 characters
- Score: Positive integer, reasonable maximum

### Error Responses

All errors follow this format:

```json
{
  "error": "Error message",
  "code": "ERROR_CODE",
  "details": ["Additional details"]
}
```

**HTTP Status Codes:**
- `200` - Success
- `201` - Created
- `400` - Bad Request
- `401` - Unauthorized
- `403` - Forbidden
- `404` - Not Found
- `429` - Too Many Requests
- `500` - Internal Server Error

## 🧪 Example Requests

### cURL Examples

**Register:**
```bash
curl -X POST http://localhost:3000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"username":"testuser","email":"test@example.com","password":"password123"}'
```

**Submit Score:**
```bash
curl -X POST http://localhost:3000/api/score \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"score":1250,"levelId":1,"timeElapsed":120.5}'
```

**Get Leaderboard:**
```bash
curl http://localhost:3000/api/leaderboard?limit=10
```

## 📝 Notes

- All timestamps are in ISO 8601 format (UTC)
- JWT tokens expire after 7 days
- Scores are validated server-side to prevent cheating
- Leaderboard updates are real-time

