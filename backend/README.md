# Backend API

Optional backend implementation for INKBLADE: ONE BULLET SAMURAI.

## 🚀 Quick Start

### Prerequisites

- Node.js 18+ (or .NET 7+ for ASP.NET Core)
- PostgreSQL 14+
- Docker (optional, for containerized setup)

### Installation

```bash
cd backend
npm install
cp .env.example .env
# Edit .env with your configuration
```

### Running Locally

```bash
# Development mode
npm run dev

# Production mode
npm start
```

### Using Docker

```bash
docker-compose up -d
```

## 📚 Documentation

See [docs/api.md](../docs/api.md) for complete API documentation.

## 🗄️ Database Setup

```bash
# Create database
createdb inkblade_db

# Run migrations
npm run migrate
```

## 🧪 Testing

```bash
# Run tests
npm test

# Run with coverage
npm run test:coverage
```

## 📝 Environment Variables

See `.env.example` for required environment variables.

## 🏗️ Architecture

- **Framework:** Express.js (or ASP.NET Core)
- **Database:** PostgreSQL
- **Authentication:** JWT
- **Validation:** Joi (or FluentValidation)

## 📦 Deployment

See deployment instructions in [docs/api.md](../docs/api.md).

