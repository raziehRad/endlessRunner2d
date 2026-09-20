
    using UnityEngine;

    // Stores configurable data for an enemy.
// ScriptableObject allows different enemies to share the same data structure
// without hardcoding their values in the enemy logic.
    
    [CreateAssetMenu(fileName = "new enemy",menuName = "Game/enemy")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public float health=100;
        public float speed=10;
        public float damage=20;
        public int score=20;
        public float ypos=1;
        public ItemType type =ItemType.Enemy;
    }
