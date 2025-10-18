
    using UnityEngine;

    [CreateAssetMenu(fileName = "new enemy",menuName = "Game/enemy")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public float health=100;
        public float speed=10;
        public float damage=20;
        public int score=20;
    }
