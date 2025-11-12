@echo off
REM Backend Setup Script for INKBLADE: ONE BULLET SAMURAI (Windows)

echo ========================================
echo INKBLADE Backend Setup
echo ========================================
echo.

cd /d "%~dp0..\backend"

REM Check if Node.js is installed
where node >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Node.js is not installed
    echo Please install Node.js 18+ from https://nodejs.org/
    pause
    exit /b 1
)

echo Node.js version:
node --version
echo.

REM Check if npm is installed
where npm >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: npm is not installed
    pause
    exit /b 1
)

echo npm version:
npm --version
echo.

REM Install dependencies
echo Installing dependencies...
call npm install

if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to install dependencies
    pause
    exit /b 1
)

echo.
echo Dependencies installed successfully!
echo.

REM Check if .env exists
if not exist ".env" (
    echo Creating .env file from .env.example...
    copy .env.example .env
    echo.
    echo Please edit .env file with your configuration:
    echo    - Database credentials
    echo    - JWT secret
    echo    - Other settings
    echo.
) else (
    echo .env file already exists
    echo.
)

echo ========================================
echo Setup complete!
echo.
echo Next steps:
echo   1. Edit .env file with your settings
echo   2. Set up PostgreSQL database
echo   3. Run: npm run migrate
echo   4. Start server: npm run dev
echo ========================================
pause

