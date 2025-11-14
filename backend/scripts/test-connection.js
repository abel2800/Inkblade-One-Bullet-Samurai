// Test database connection script
require('dotenv').config();
const { query } = require('../src/config/database');

async function testConnection() {
  try {
    console.log('🔄 Testing database connection...');
    console.log('Database:', process.env.DB_NAME);
    console.log('Host:', process.env.DB_HOST);
    console.log('Port:', process.env.DB_PORT);
    console.log('User:', process.env.DB_USER);
    console.log('');

    // Test simple query
    const result = await query('SELECT NOW() as current_time, version() as pg_version');
    
    console.log('✅ Database connection successful!');
    console.log('Current time:', result.rows[0].current_time);
    console.log('PostgreSQL version:', result.rows[0].pg_version.split(',')[0]);
    console.log('');
    console.log('✅ Ready to run migrations!');
    
    process.exit(0);
  } catch (error) {
    console.error('❌ Database connection failed!');
    console.error('Error:', error.message);
    console.log('');
    console.log('Please check:');
    console.log('1. PostgreSQL is running');
    console.log('2. Database "game" exists');
    console.log('3. Password is correct');
    console.log('4. User "postgres" has access');
    process.exit(1);
  }
}

testConnection();

