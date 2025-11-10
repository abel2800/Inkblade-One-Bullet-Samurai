namespace Inkblade.Utils
{
    /// <summary>
    /// Game constants and configuration values.
    /// </summary>
    public static class Constants
    {
        // Tags
        public const string TAG_PLAYER = "Player";
        public const string TAG_ENEMY = "Enemy";
        public const string TAG_BULLET = "Bullet";
        public const string TAG_WALL = "Wall";
        public const string TAG_GROUND = "Ground";

        // Layers
        public const string LAYER_PLAYER = "Player";
        public const string LAYER_ENEMY = "Enemy";
        public const string LAYER_BULLET = "Bullet";
        public const string LAYER_WALL = "Wall";
        public const string LAYER_GROUND = "Ground";

        // Input
        public const string INPUT_HORIZONTAL = "Horizontal";
        public const string INPUT_VERTICAL = "Vertical";
        public const string INPUT_JUMP = "Jump";
        public const string INPUT_DASH = "Dash";
        public const string INPUT_SHOOT = "Fire1";
        public const string INPUT_RETRIEVE = "Interact";

        // PlayerPrefs Keys
        public const string PREF_MASTER_VOLUME = "MasterVolume";
        public const string PREF_MUSIC_VOLUME = "MusicVolume";
        public const string PREF_SFX_VOLUME = "SFXVolume";
        public const string PREF_HIGH_SCORE = "HighScore";
        public const string PREF_LEVEL_PROGRESS = "LevelProgress";

        // Animation Parameters
        public const string ANIM_SPEED = "Speed";
        public const string ANIM_IS_GROUNDED = "IsGrounded";
        public const string ANIM_DASH = "Dash";
        public const string ANIM_ATTACK = "Attack";
        public const string ANIM_DEATH = "Death";
        public const string ANIM_HIT = "Hit";

        // Enemy States
        public const string ENEMY_STATE_IDLE = "Idle";
        public const string ENEMY_STATE_PURSUE = "Pursue";
        public const string ENEMY_STATE_ATTACK = "Attack";
        public const string ENEMY_STATE_STAGGER = "Stagger";
        public const string ENEMY_STATE_DEAD = "Dead";
    }
}

