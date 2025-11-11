const express = require('express');
const { query } = require('../config/database');
const { authenticateToken } = require('../middleware/auth');
const { validate, schemas } = require('../middleware/validation');

const router = express.Router();

/**
 * POST /api/score
 * Submit a score
 */
router.post('/', authenticateToken, validate(schemas.score), async (req, res) => {
  try {
    const { score, levelId, timeElapsed, enemiesKilled, deaths } = req.body;
    const userId = req.user.id;

    // Insert score
    const result = await query(
      `INSERT INTO scores (user_id, score, level_id, time_elapsed, enemies_killed, deaths, created_at)
       VALUES ($1, $2, $3, $4, $5, $6, NOW())
       RETURNING id, score, level_id, created_at`,
      [userId, score, levelId || 1, timeElapsed || null, enemiesKilled || null, deaths || null]
    );

    // Get rank
    const rankResult = await query(
      `SELECT COUNT(*) + 1 as rank
       FROM scores
       WHERE score > $1 AND level_id = $2`,
      [score, levelId || 1]
    );

    const rank = parseInt(rankResult.rows[0].rank);

    res.status(201).json({
      id: result.rows[0].id,
      userId: userId,
      score: result.rows[0].score,
      levelId: result.rows[0].level_id,
      rank: rank,
      createdAt: result.rows[0].created_at
    });
  } catch (error) {
    console.error('Score submission error:', error);
    res.status(500).json({
      error: 'Internal server error',
      message: 'Failed to submit score'
    });
  }
});

/**
 * GET /api/score/best
 * Get user's best score
 */
router.get('/best', authenticateToken, async (req, res) => {
  try {
    const userId = req.user.id;
    const levelId = req.query.levelId || 1;

    const result = await query(
      `SELECT score, level_id, created_at
       FROM scores
       WHERE user_id = $1 AND level_id = $2
       ORDER BY score DESC
       LIMIT 1`,
      [userId, levelId]
    );

    if (result.rows.length === 0) {
      return res.status(404).json({
        error: 'No score found'
      });
    }

    // Get rank
    const rankResult = await query(
      `SELECT COUNT(*) + 1 as rank
       FROM scores
       WHERE score > $1 AND level_id = $2`,
      [result.rows[0].score, levelId]
    );

    const rank = parseInt(rankResult.rows[0].rank);

    res.json({
      score: result.rows[0].score,
      levelId: result.rows[0].level_id,
      rank: rank,
      createdAt: result.rows[0].created_at
    });
  } catch (error) {
    console.error('Get best score error:', error);
    res.status(500).json({
      error: 'Internal server error',
      message: 'Failed to get best score'
    });
  }
});

module.exports = router;

