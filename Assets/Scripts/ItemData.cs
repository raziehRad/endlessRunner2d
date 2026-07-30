
    using UnityEngine;

    [CreateAssetMenu(fileName = "new item", menuName = "Game/Item")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        [Header("General")]
        public ItemType itemType = ItemType.Item;

        
        [Header("Item")]
        public ItemEffect effect;
        public int value;
        public float duration;
    }

    public enum ItemType
    {
        Item,Enemy
    }
    public enum ItemEffect
    {
        None,

        // Health
        Heal,

        // Defense
        Shield,
        Invincible,

        // Score
        AddScore,
        DoubleScore,

        // Coins
        AddCoin,
        Magnet,

        // Weapons
        WeaponUpgrade,
        FireBullet,
        Bomb,

        // Movement
        SpeedBoost,
        SlowMotion,

        // Negative
        Poison,
        Slow,
        Stun,
        
        Flying,
        Jump
    }