namespace NavySpade.pj77.Extension{
    public static class StringManager{
        public const string DEFAULT = "Default";
        public const string PLAYER_ATTACK_LAYER = "PlayerAttack";
        public const string ENEMY_ATTACK_LAYER = "EnemyAttack";
        public const string ENEMY_AND_PLAYER_ATTACK_LAYER = "EnemyAndPlayerAttack";
        public const string PLAYER_LAYER = "Player";
        public const string ENEMY_LAYER = "Enemy";
        public const string GROUND_LAYER = "Ground";
        public const string RESULT_OF_LOADING_DATA_MODEL = "Result of loading the data model";
        public const string RESULF_OF_LOADING_RESOURCES = "Result of loading resources";

        public static string PathForResources(int levelIndex, int instanceId){
            return $"Level_{levelIndex};resource_{instanceId}";
        }
    }
}