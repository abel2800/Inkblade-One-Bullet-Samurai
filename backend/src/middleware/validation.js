const Joi = require('joi');

/**
 * Validation middleware factory
 */
const validate = (schema) => {
  return (req, res, next) => {
    const { error, value } = schema.validate(req.body, {
      abortEarly: false,
      stripUnknown: true
    });

    if (error) {
      const errors = error.details.map(detail => detail.message);
      return res.status(400).json({
        error: 'Validation failed',
        details: errors
      });
    }

    req.body = value;
    next();
  };
};

// Validation schemas
const schemas = {
  register: Joi.object({
    username: Joi.string().alphanum().min(3).max(50).required(),
    email: Joi.string().email().required(),
    password: Joi.string().min(8).required()
  }),

  login: Joi.object({
    email: Joi.string().email().required(),
    password: Joi.string().required()
  }),

  score: Joi.object({
    score: Joi.number().integer().min(0).max(999999).required(),
    levelId: Joi.number().integer().min(1).optional(),
    timeElapsed: Joi.number().min(0).optional(),
    enemiesKilled: Joi.number().integer().min(0).optional(),
    deaths: Joi.number().integer().min(0).optional()
  }),

  analytics: Joi.object({
    eventType: Joi.string().required(),
    metadata: Joi.object().optional()
  })
};

module.exports = {
  validate,
  schemas
};

