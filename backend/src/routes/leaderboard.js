const express = require('express');
const { query } = require('../config/database');

const router = express.Router();

/**
 * GET /api/leaderboard
 * Get leaderboard
 */
router.get('/', async (req, res) => {
  try {
    const limit = parseInt(req.query.limit) || 10;
    const offset = parseInt(req.query.offset) || 0;
    const levelId = req.query.levelId;

    // Build query
    let queryText = `
      SELECT 
        ROW_NUMBER() OVER (ORDER BY score DESC) as rank,
        u.username,
        s.score,
        s.level_id,
        s.created_at
      FROM scores s
      JOIN users u ON s.user_id = u.id
    `;

    const params = [];
    let paramCount = 1;

    if (levelId) {
      queryText += ` WHERE s.level_id = $${paramCount}`;
      params.push(levelId);
      paramCount++;
    }

    queryText += ` ORDER BY s.score DESC LIMIT $${paramCount} OFFSET $${paramCount + 1}`;
    params.push(limit, offset);

    // Get leaderboard
    const result = await query(queryText, params);

    // Get total count
    let countQuery = 'SELECT COUNT(*) FROM scores';
    if (levelId) {
      countQuery += ' WHERE level_id = $1';
    }
    const countResult = await query(countQuery, levelId ? [levelId] : []);

    const total = parseInt(countResult.rows[0].count);

    res.json({
      leaderboard: result.rows.map(row => ({
        rank: parseInt(row.rank) + offset,
        username: row.username,
        score: row.score,
        levelId: row.level_id,
        createdAt: row.created_at
      })),
      total: total,
      limit: limit,
      offset: offset
    });
  } catch (error) {
    console.error('Leaderboard error:', error);
    res.status(500).json({
      error: 'Internal server error',
      message: 'Failed to get leaderboard'
    });
  }
});

module.exports = router;

