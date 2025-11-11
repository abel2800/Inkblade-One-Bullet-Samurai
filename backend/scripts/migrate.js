const { query } = require('../src/config/database');

async function migrate() {
  try {
    console.log('🔄 Starting database migration...');

    // Create users table
    await query(`
      CREATE TABLE IF NOT EXISTS users (
        id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
        username VARCHAR(50) UNIQUE NOT NULL,
        email VARCHAR(255) UNIQUE NOT NULL,
        password_hash VARCHAR(255) NOT NULL,
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
      )
    `);
    console.log('✅ Created users table');

    // Create scores table
    await query(`
      CREATE TABLE IF NOT EXISTS scores (
        id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
        user_id UUID REFERENCES users(id) ON DELETE CASCADE,
        score INTEGER NOT NULL,
        level_id INTEGER NOT NULL DEFAULT 1,
        time_elapsed FLOAT,
        enemies_killed INTEGER,
        deaths INTEGER,
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
      )
    `);
    console.log('✅ Created scores table');

    // Create indexes
    await query(`
      CREATE INDEX IF NOT EXISTS idx_scores_user_id ON scores(user_id)
    `);
    await query(`
      CREATE INDEX IF NOT EXISTS idx_scores_score ON scores(score DESC)
    `);
    await query(`
      CREATE INDEX IF NOT EXISTS idx_scores_level_id ON scores(level_id)
    `);
    console.log('✅ Created indexes');

    // Create analytics table
    await query(`
      CREATE TABLE IF NOT EXISTS analytics (
        id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
        user_id UUID REFERENCES users(id) ON DELETE SET NULL,
        event_type VARCHAR(50) NOT NULL,
        metadata JSONB,
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
      )
    `);
    console.log('✅ Created analytics table');

    // Create analytics indexes
    await query(`
      CREATE INDEX IF NOT EXISTS idx_analytics_user_id ON analytics(user_id)
    `);
    await query(`
      CREATE INDEX IF NOT EXISTS idx_analytics_event_type ON analytics(event_type)
    `);
    await query(`
      CREATE INDEX IF NOT EXISTS idx_analytics_created_at ON analytics(created_at)
    `);
    console.log('✅ Created analytics indexes');

    console.log('✅ Migration completed successfully!');
    process.exit(0);
  } catch (error) {
    console.error('❌ Migration failed:', error);
    process.exit(1);
  }
}

migrate();

