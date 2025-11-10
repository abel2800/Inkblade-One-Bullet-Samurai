# Contributing to INKBLADE: ONE BULLET SAMURAI

Thank you for your interest in contributing! This document provides guidelines and instructions for contributing to the project.

## 🚀 Getting Started

1. **Fork the repository** and clone it locally
2. **Set up your development environment** (see [docs/setup.md](docs/setup.md))
3. **Create a branch** for your feature/fix:
   ```bash
   git checkout -b feature/your-feature-name
   ```

## 📋 Development Workflow

### Branch Naming Convention

- `feature/` - New features
- `fix/` - Bug fixes
- `docs/` - Documentation updates
- `refactor/` - Code refactoring
- `test/` - Test additions/updates

### Commit Messages

Use clear, descriptive commit messages:

```
feat: Add dash cooldown indicator to UI
fix: Resolve bullet retrieval collision bug
docs: Update architecture documentation
refactor: Optimize enemy pathfinding algorithm
```

### Code Standards

- **C# Naming Conventions:**
  - Classes: `PascalCase` (e.g., `PlayerController`)
  - Methods: `PascalCase` (e.g., `ShootBullet()`)
  - Private fields: `camelCase` with underscore prefix (e.g., `_rigidbody`)
  - Public properties: `PascalCase` (e.g., `Health`)

- **Code Style:**
  - Use XML comments for public methods
  - Keep methods focused and single-responsibility
  - Follow Unity's coding conventions
  - Use meaningful variable names

### Pull Request Process

1. **Update documentation** if you've changed functionality
2. **Test your changes** thoroughly
3. **Ensure your code compiles** without errors
4. **Create a pull request** with:
   - Clear description of changes
   - Reference to related issues
   - Screenshots/GIFs if UI changes
   - Testing notes

### Testing Guidelines

- Test locally before submitting PR
- Test on target platforms (Windows, WebGL)
- Verify no console errors or warnings
- Check performance impact of changes

## 🎨 Asset Guidelines

- **Art Assets:** Follow the Sumi-e ink aesthetic (black & white)
- **Audio:** Use royalty-free sources or original content
- **Code:** Comment complex logic and algorithms

## 📝 Reporting Issues

When reporting bugs, please include:

- Unity version
- OS and platform
- Steps to reproduce
- Expected vs actual behavior
- Console logs/errors
- Screenshots if applicable

Use the issue templates provided in `.github/ISSUE_TEMPLATE/`.

## 🏗️ Project Structure

Please maintain the existing folder structure:

```
Assets/
├── Scripts/
│   ├── Player/
│   ├── Weapons/
│   ├── Enemies/
│   ├── UI/
│   ├── Systems/
│   └── Utils/
├── Prefabs/
├── Scenes/
├── Art/
└── Audio/
```

## 📚 Additional Resources

- [Unity Coding Standards](https://docs.unity3d.com/Manual/CodingStandards.html)
- [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

## ❓ Questions?

Feel free to open an issue with the `question` label or contact the maintainers.

Thank you for contributing! 🎮
