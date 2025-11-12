# Git Commits Guide

Best practices for committing changes to INKBLADE: ONE BULLET SAMURAI.

## 📝 Commit Message Format

Use conventional commit format:

```
<type>: <description>

[optional body]

[optional footer]
```

### Types

- `feat:` - New feature
- `fix:` - Bug fix
- `docs:` - Documentation changes
- `style:` - Code style changes (formatting, etc.)
- `refactor:` - Code refactoring
- `test:` - Adding or updating tests
- `chore:` - Maintenance tasks
- `perf:` - Performance improvements

### Examples

```bash
# Feature
git commit -m "feat: Add camera shake on enemy kill"

# Bug fix
git commit -m "fix: Resolve bullet retrieval range issue"

# Documentation
git commit -m "docs: Update setup guide with new steps"

# Refactoring
git commit -m "refactor: Optimize enemy pathfinding algorithm"

# Multiple changes
git commit -m "feat: Add particle effects system

- Implement ParticleManager with pooling
- Add hit, death, and impact effects
- Integrate with enemy and bullet systems"
```

## 🔄 Workflow

### Daily Development

1. **Start of day:**
   ```bash
   git pull origin main
   ```

2. **Make changes:**
   - Work on feature
   - Test changes
   - Fix issues

3. **Commit frequently:**
   ```bash
   git add .
   git commit -m "feat: Description of changes"
   ```

4. **End of day:**
   ```bash
   git push origin main
   ```

### Feature Development

1. **Create feature branch:**
   ```bash
   git checkout -b feature/player-animations
   ```

2. **Work on feature:**
   - Make commits as you go
   - Keep commits focused

3. **Push branch:**
   ```bash
   git push origin feature/player-animations
   ```

4. **Create Pull Request:**
   - Review changes
   - Get feedback
   - Merge when ready

## 📋 Commit Checklist

Before committing:
- [ ] Code compiles without errors
- [ ] No console errors/warnings (or acceptable)
- [ ] Changes tested
- [ ] Commit message is clear
- [ ] Related files included
- [ ] No temporary/debug code

## 🚫 What NOT to Commit

- Build artifacts (`/Builds/`, `/Library/`, `/Temp/`)
- User-specific settings
- Temporary files
- API keys or secrets
- Large binary files (use Git LFS)
- Generated files

## 📊 Commit Frequency

**Good:**
- Commit after completing a feature
- Commit after fixing a bug
- Commit after refactoring
- Commit logical units together

**Bad:**
- Committing everything at once
- Committing broken code
- Committing unrelated changes together
- Waiting days between commits

## 🔍 Commit Examples

### Good Commits

```bash
# Single, focused change
git commit -m "fix: Player can now retrieve bullet from any angle"

# Multiple related changes
git commit -m "feat: Add enemy spawn system

- Create EnemySpawner script
- Implement wave-based spawning
- Add spawn point configuration"

# Documentation update
git commit -m "docs: Add testing guide for enemy AI"
```

### Bad Commits

```bash
# Too vague
git commit -m "fix stuff"

# Too many unrelated changes
git commit -m "fix player, add enemy, update ui, fix bugs"

# Commit message doesn't match changes
git commit -m "feat: Add new feature"  # But actually fixed a bug
```

## 🌿 Branch Strategy

### Main Branches

- `main` - Production-ready code
- `develop` - Development branch (optional)

### Feature Branches

- `feature/player-animations`
- `feature/level-design`
- `fix/bullet-retrieval-bug`

### Naming Convention

- `feature/` - New features
- `fix/` - Bug fixes
- `docs/` - Documentation
- `refactor/` - Refactoring
- `test/` - Testing

## 📦 Commit Best Practices

1. **Keep commits small and focused**
   - One logical change per commit
   - Easier to review and revert

2. **Write clear commit messages**
   - Explain what and why
   - Use present tense ("Add" not "Added")

3. **Test before committing**
   - Ensure code works
   - No breaking changes

4. **Review your changes**
   ```bash
   git diff
   git status
   ```

5. **Commit related changes together**
   - Don't mix unrelated changes
   - Keep commits logical

## 🔄 Syncing with Remote

### Before Starting Work
```bash
git pull origin main
```

### After Committing
```bash
git push origin main
```

### If Conflicts Occur
```bash
git pull origin main
# Resolve conflicts
git add .
git commit -m "fix: Resolve merge conflicts"
git push origin main
```

## 📚 Additional Resources

- [Conventional Commits](https://www.conventionalcommits.org/)
- [Git Best Practices](https://git-scm.com/book)
- [GitHub Flow](https://guides.github.com/introduction/flow/)

---

**Remember:** Good commit messages help you and others understand the project's history!

