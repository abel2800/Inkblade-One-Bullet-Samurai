@echo off
REM Windows Build Script for INKBLADE: ONE BULLET SAMURAI
REM This script helps automate Windows builds

echo ========================================
echo INKBLADE: ONE BULLET SAMURAI
echo Windows Build Script
echo ========================================
echo.

REM Check if Unity is installed
set UNITY_PATH=C:\Program Files\Unity\Hub\Editor\2022.3.15f1\Editor\Unity.exe
if not exist "%UNITY_PATH%" (
    echo ERROR: Unity not found at expected path
    echo Please update UNITY_PATH in this script
    pause
    exit /b 1
)

echo Unity found at: %UNITY_PATH%
echo.

REM Set build path
set BUILD_PATH=%~dp0..\Builds\Windows
set PROJECT_PATH=%~dp0..

echo Project Path: %PROJECT_PATH%
echo Build Path: %BUILD_PATH%
echo.

REM Create build directory if it doesn't exist
if not exist "%BUILD_PATH%" mkdir "%BUILD_PATH%"

echo Starting build...
echo.

REM Run Unity build
"%UNITY_PATH%" ^
    -batchmode ^
    -quit ^
    -projectPath "%PROJECT_PATH%" ^
    -buildTarget Win64 ^
    -buildWindows64Player "%BUILD_PATH%\Inkblade.exe" ^
    -logFile "%BUILD_PATH%\build.log"

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo Build completed successfully!
    echo Build location: %BUILD_PATH%
    echo ========================================
) else (
    echo.
    echo ========================================
    echo Build failed! Check build.log for details
    echo ========================================
)

pause

