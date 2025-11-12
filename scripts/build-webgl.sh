#!/bin/bash
# WebGL Build Script for INKBLADE: ONE BULLET SAMURAI
# This script helps automate WebGL builds

echo "========================================"
echo "INKBLADE: ONE BULLET SAMURAI"
echo "WebGL Build Script"
echo "========================================"
echo ""

# Check if Unity is installed
UNITY_PATH="/Applications/Unity/Hub/Editor/2022.3.15f1/Unity.app/Contents/MacOS/Unity"
if [ ! -f "$UNITY_PATH" ]; then
    UNITY_PATH="/opt/unity/Editor/Unity"
    if [ ! -f "$UNITY_PATH" ]; then
        echo "ERROR: Unity not found"
        echo "Please update UNITY_PATH in this script"
        exit 1
    fi
fi

echo "Unity found at: $UNITY_PATH"
echo ""

# Set build path
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
PROJECT_PATH="$(dirname "$SCRIPT_DIR")"
BUILD_PATH="$PROJECT_PATH/Builds/WebGL"

echo "Project Path: $PROJECT_PATH"
echo "Build Path: $BUILD_PATH"
echo ""

# Create build directory if it doesn't exist
mkdir -p "$BUILD_PATH"

echo "Starting build..."
echo ""

# Run Unity build
"$UNITY_PATH" \
    -batchmode \
    -quit \
    -projectPath "$PROJECT_PATH" \
    -buildTarget WebGL \
    -buildPath "$BUILD_PATH" \
    -logFile "$BUILD_PATH/build.log"

if [ $? -eq 0 ]; then
    echo ""
    echo "========================================"
    echo "Build completed successfully!"
    echo "Build location: $BUILD_PATH"
    echo "========================================"
else
    echo ""
    echo "========================================"
    echo "Build failed! Check build.log for details"
    echo "========================================"
    exit 1
fi

