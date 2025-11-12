# Quick Database Setup

## Your Database Configuration

- **Database Name:** inkblade_db
- **User:** postgres
- **Password:** 1992
- **Host:** localhost
- **Port:** 5432

## Steps

### 1. Create Database (Manual)

**Using psql:**
```bash
psql -U postgres
CREATE DATABASE inkblade_db;
\q
```

**Or using SQL:**
```sql
CREATE DATABASE inkblade_db;
```

### 2. Run Migrations (Automatic)

After creating the database, run:

```bash
cd backend
npm run migrate
```

This will automatically create:
- ✅ users table
- ✅ scores table  
- ✅ analytics table
- ✅ All indexes
- ✅ All relationships

### 3. Start Server

```bash
npm run dev
```

Server will run on: http://localhost:3000

## ✅ Done!

Your database is configured and ready!

