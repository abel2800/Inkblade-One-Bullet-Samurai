const express = require('express');
const { query } = require('../config/database');
const { authenticateToken } = require('../middleware/auth');

const router = express.Router();

/**
 * GET /api/stats/user/:userId
 * Get user statistics
 */
router.get('/user/:userId', authenticateToken, async (req, res) => {
  try {
    const userId = req.params.userId;

    // Verify user can only access their own stats
    if (req.user.id !== userId && req.user.id !== userId) {
      return res.status(403).json({
        error: 'Forbidden',
        message: 'Cannot access other user stats'
      });
    }

    // Get user stats
    const statsResult = await query(
      `SELECT 
        COUNT(DISTINCT s.id) as games_played,
        SUM(s.score) as total_score,
        AVG(s.score) as average_score,
        MAX(s.score) as best_score,
        SUM(s.enemies_killed) as total_enemies_killed,
        SUM(s.deaths) as total_deaths,
        SUM(s.time_elapsed) as total_play_time
      FROM scores s
      WHERE s.user_id = $1`,
      [userId]
    );

    // Get favorite level
    const levelResult = await query(
      `SELECT level_id, COUNT(*) as play_count
       FROM scores
       WHERE user_id = $1
       GROUP BY level_id
       ORDER BY play_count DESC
       LIMIT 1`,
      [userId]
    );

    const stats = statsResult.rows[0];
    const favoriteLevel = levelResult.rows.length > 0 ? levelResult.rows[0].level_id : null;

    // Get username
    const userResult = await query(
      'SELECT username FROM users WHERE id = $1',
      [userId]
    );

    res.json({
      userId: userId,
      username: userResult.rows[0].username,
      totalScore: parseInt(stats.total_score) || 0,
      gamesPlayed: parseInt(stats.games_played) || 0,
      averageScore: Math.round(parseFloat(stats.average_score) || 0),
      bestScore: parseInt(stats.best_score) || 0,
      totalEnemiesKilled: parseInt(stats.total_enemies_killed) || 0,
      totalDeaths: parseInt(stats.total_deaths) || 0,
      favoriteLevel: favoriteLevel,
      playTime: Math.round(parseFloat(stats.total_play_time) || 0)
    });
  } catch (error) {
    console.error('Stats error:', error);
    res.status(500).json({
      error: 'Internal server error',
      message: 'Failed to get user stats'
    });
  }
});

module.exports = router;

