# Database Setup Instructions

## Step 1: Create Database Manually

Create the PostgreSQL database using one of these methods:

### Option A: Using psql Command Line
```bash
psql -U postgres
CREATE DATABASE inkblade_db;
\q
```

### Option B: Using pgAdmin
1. Open pgAdmin
2. Connect to PostgreSQL server
3. Right-click on "Databases"
4. Select "Create" > "Database"
5. Name: `inkblade_db`
6. Click "Save"

### Option C: Using SQL Command
```sql
CREATE DATABASE inkblade_db;
```

## Step 2: Verify Database Connection

The `.env` file is already configured with:
- **Host:** localhost
- **Port:** 5432
- **Database:** inkblade_db
- **User:** postgres
- **Password:** 1992

## Step 3: Run Migrations (Automatic Table Creation)

Once the database is created, run the migration script to automatically create all tables:

```bash
cd backend
npm run migrate
```

This will automatically create:
- ✅ `users` table
- ✅ `scores` table
- ✅ `analytics` table
- ✅ All indexes
- ✅ All relationships

## Step 4: Verify Tables Created

You can verify the tables were created:

```bash
psql -U postgres -d inkblade_db -c "\dt"
```

Or in pgAdmin:
- Expand `inkblade_db` database
- Expand "Schemas" > "public" > "Tables"
- You should see: users, scores, analytics

## ✅ Done!

Your database is now set up and ready to use!

## 🚀 Start Backend Server

```bash
cd backend
npm run dev
```

The server will start on http://localhost:3000

## 🔧 Troubleshooting

### Connection Error
- Verify PostgreSQL is running
- Check password is correct (1992)
- Verify database `inkblade_db` exists
- Check port 5432 is not blocked

### Migration Errors
- Ensure database exists first
- Check user has CREATE TABLE permissions
- Verify PostgreSQL version is 14+

---

**Note:** The `.env` file is already configured. Just create the database and run migrations!

