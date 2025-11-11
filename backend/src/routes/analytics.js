const express = require('express');
const { query } = require('../config/database');
const { authenticateToken } = require('../middleware/auth');
const { validate, schemas } = require('../middleware/validation');

const router = express.Router();

/**
 * POST /api/analytics
 * Submit analytics event
 */
router.post('/', authenticateToken, validate(schemas.analytics), async (req, res) => {
  try {
    const { eventType, metadata } = req.body;
    const userId = req.user.id;

    // Insert analytics event
    const result = await query(
      `INSERT INTO analytics (user_id, event_type, metadata, created_at)
       VALUES ($1, $2, $3, NOW())
       RETURNING id, event_type, created_at`,
      [userId, eventType, JSON.stringify(metadata || {})]
    );

    res.status(201).json({
      id: result.rows[0].id,
      eventType: result.rows[0].event_type,
      createdAt: result.rows[0].created_at
    });
  } catch (error) {
    console.error('Analytics error:', error);
    res.status(500).json({
      error: 'Internal server error',
      message: 'Failed to submit analytics event'
    });
  }
});

module.exports = router;

