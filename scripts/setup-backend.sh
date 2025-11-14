#!/bin/bash
# Backend Setup Script for INKBLADE: ONE BULLET SAMURAI

echo "========================================"
echo "INKBLADE Backend Setup"
echo "========================================"
echo ""

SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
BACKEND_DIR="$(dirname "$SCRIPT_DIR")/backend"

cd "$BACKEND_DIR"

# Check if Node.js is installed
if ! command -v node &> /dev/null; then
    echo "ERROR: Node.js is not installed"
    echo "Please install Node.js 18+ from https://nodejs.org/"
    exit 1
fi

echo "Node.js version: $(node --version)"
echo ""

# Check if npm is installed
if ! command -v npm &> /dev/null; then
    echo "ERROR: npm is not installed"
    exit 1
fi

echo "npm version: $(npm --version)"
echo ""

# Install dependencies
echo "Installing dependencies..."
npm install

if [ $? -ne 0 ]; then
    echo "ERROR: Failed to install dependencies"
    exit 1
fi

echo ""
echo "Dependencies installed successfully!"
echo ""

# Check if .env exists
if [ ! -f ".env" ]; then
    echo ".env file not found!"
    echo "Please create .env file with your configuration:"
    echo "   - Database credentials (DB_HOST, DB_PORT, DB_NAME, DB_USER, DB_PASSWORD)"
    echo "   - JWT secret (JWT_SECRET)"
    echo "   - See backend/README.md for details"
    echo ""
else
    echo ".env file already exists"
    echo ""
fi

# Check if PostgreSQL is available
if command -v psql &> /dev/null; then
    echo "PostgreSQL is installed"
    echo ""
    echo "To set up the database:"
    echo "  1. Create database: createdb game"
    echo "  2. Run migrations: npm run migrate"
    echo ""
else
    echo "⚠️  PostgreSQL not found in PATH"
    echo "   Please install PostgreSQL or use Docker"
    echo ""
fi

echo "========================================"
echo "Setup complete!"
echo ""
echo "Next steps:"
echo "  1. Edit .env file with your settings"
echo "  2. Set up PostgreSQL database"
echo "  3. Run: npm run migrate"
echo "  4. Start server: npm run dev"
echo "========================================"

