# Testing Guide

Comprehensive testing guide for INKBLADE: ONE BULLET SAMURAI.

## 🧪 Testing Checklist

### Unit Testing

#### Player System
- [ ] **Movement**
  - [ ] Player moves in all directions (WASD/Arrows)
  - [ ] Movement is smooth with acceleration/deceleration
  - [ ] Player stops when input is released
  - [ ] Movement respects boundaries

- [ ] **Dash**
  - [ ] Dash moves player in correct direction
  - [ ] Dash has cooldown
  - [ ] Dash provides invulnerability (if enabled)
  - [ ] Dash cooldown UI updates correctly
  - [ ] Cannot dash while dash is on cooldown

- [ ] **Health**
  - [ ] Player takes damage correctly
  - [ ] Health bar updates
  - [ ] Invulnerability frames work after damage
  - [ ] Player dies at 0 health
  - [ ] Respawn works correctly

#### Bullet System
- [ ] **Shooting**
  - [ ] Bullet spawns at correct position
  - [ ] Bullet moves in correct direction (towards mouse)
  - [ ] Player cannot shoot without bullet
  - [ ] Bullet state updates correctly

- [ ] **Sticking**
  - [ ] Bullet sticks to walls
  - [ ] Bullet sticks to enemies
  - [ ] Bullet stops physics when stuck
  - [ ] Impact effect spawns

- [ ] **Retrieval**
  - [ ] Player can retrieve bullet (E key)
  - [ ] Retrieval works within range
  - [ ] Bullet returns to pool
  - [ ] Player can shoot again after retrieval

#### Enemy System
- [ ] **AI States**
  - [ ] Enemy starts in Idle state
  - [ ] Enemy detects player at correct range
  - [ ] Enemy pursues player
  - [ ] Enemy attacks when in range
  - [ ] Enemy staggers on damage
  - [ ] Enemy dies at 0 health

- [ ] **Movement**
  - [ ] Enemy moves towards player
  - [ ] Enemy stops at correct distance
  - [ ] Enemy faces player direction
  - [ ] Enemy respects boundaries

- [ ] **Spawning**
  - [ ] Enemies spawn at spawn points
  - [ ] Wave system works
  - [ ] Enemy count is correct per wave
  - [ ] Next wave starts after previous completes

### Integration Testing

#### Player-Bullet Integration
- [ ] Player shoots bullet correctly
- [ ] Bullet hits enemies
- [ ] Bullet can be retrieved
- [ ] Player state updates correctly

#### Bullet-Enemy Integration
- [ ] Bullet damages enemy on hit
- [ ] Bullet sticks to enemy
- [ ] Enemy takes correct damage
- [ ] Enemy health updates

#### Enemy-Player Integration
- [ ] Enemy detects player
- [ ] Enemy attacks player
- [ ] Player takes damage from enemy
- [ ] Collision works correctly

#### UI Integration
- [ ] HUD updates with player stats
- [ ] Health bar reflects player health
- [ ] Bullet indicator shows correct state
- [ ] Score updates correctly
- [ ] Time displays correctly
- [ ] Pause menu works
- [ ] Game over screen shows correct stats

#### Audio Integration
- [ ] Sound effects play on events
- [ ] Music plays correctly
- [ ] Volume controls work
- [ ] Settings persist

### System Testing

#### GameManager
- [ ] Game starts correctly
- [ ] Pause/resume works
- [ ] Game over triggers correctly
- [ ] Score tracking works
- [ ] Slow motion works
- [ ] Scene reload works

#### SaveManager
- [ ] High score saves
- [ ] High score loads
- [ ] Settings save
- [ ] Settings load

#### Backend Integration (Optional)
- [ ] User registration works
- [ ] User login works
- [ ] Score submission works
- [ ] Leaderboard displays
- [ ] Token management works

## 🎮 Playtesting

### Gameplay Feel
- [ ] Movement feels responsive
- [ ] Dash feels satisfying
- [ ] Shooting feels good
- [ ] Retrieval is intuitive
- [ ] Combat is engaging
- [ ] Difficulty progression feels right

### Visual Feedback
- [ ] Camera shake on kill
- [ ] Slow motion on kill
- [ ] Particle effects visible
- [ ] UI is clear and readable
- [ ] Visual feedback is satisfying

### Audio Feedback
- [ ] Sounds are clear
- [ ] Music fits the mood
- [ ] Volume levels are balanced
- [ ] Audio enhances gameplay

### Performance
- [ ] FPS stays above 60 (target)
- [ ] No frame drops
- [ ] Memory usage is reasonable
- [ ] No memory leaks
- [ ] Build size is acceptable

## 🐛 Bug Testing

### Common Issues to Check
- [ ] Player can't get stuck in walls
- [ ] Bullet doesn't get stuck in air
- [ ] Enemies don't clip through walls
- [ ] UI doesn't overlap incorrectly
- [ ] Audio doesn't overlap incorrectly
- [ ] No null reference exceptions
- [ ] No missing component errors

### Edge Cases
- [ ] Player shoots at edge of screen
- [ ] Player dashes into wall
- [ ] Multiple enemies at once
- [ ] Bullet lifetime expires
- [ ] Player dies while dashing
- [ ] Game paused during dash
- [ ] Scene transition during gameplay

## 📊 Performance Testing

### Metrics to Monitor
- **FPS:** Should stay above 60
- **Memory:** Should not continuously increase
- **Draw Calls:** Should be optimized
- **Physics Updates:** Should be efficient
- **Audio Sources:** Should be pooled

### Tools
- Use `PerformanceMonitor` component
- Unity Profiler
- Build and test on target platform

## 🔍 Regression Testing

After each change:
- [ ] Test all core mechanics still work
- [ ] Test UI still functions
- [ ] Test audio still plays
- [ ] Test no new bugs introduced
- [ ] Test performance hasn't degraded

## 📝 Test Scenarios

### Scenario 1: Basic Gameplay
1. Start game
2. Move player
3. Shoot bullet
4. Hit enemy
5. Retrieve bullet
6. Repeat

### Scenario 2: Dash Test
1. Move player
2. Dash forward
3. Wait for cooldown
4. Dash again
5. Verify cooldown works

### Scenario 3: Enemy Wave
1. Start game
2. Kill all enemies in wave 1
3. Verify wave 2 starts
4. Verify difficulty increases

### Scenario 4: Death & Restart
1. Play until death
2. Verify game over screen
3. Click retry
4. Verify game restarts correctly

## ✅ Testing Best Practices

1. **Test Early and Often**
   - Test after each feature
   - Don't wait until the end

2. **Test on Target Platform**
   - Test Windows build
   - Test WebGL build (if applicable)

3. **Test with Different Settings**
   - Different resolutions
   - Different volume levels
   - Different input methods

4. **Document Bugs**
   - Use GitHub Issues
   - Include steps to reproduce
   - Include screenshots/logs

5. **Test Edge Cases**
   - Extreme values
   - Rapid input
   - Unusual scenarios

## 🎯 Acceptance Criteria

Game is ready when:
- [ ] All core mechanics work
- [ ] No critical bugs
- [ ] Performance is acceptable
- [ ] UI is functional
- [ ] Audio works
- [ ] Builds successfully
- [ ] Plays smoothly

---

**Remember:** Testing is an ongoing process. Continue testing as you add features and make changes!

