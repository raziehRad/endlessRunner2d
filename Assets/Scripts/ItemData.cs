
    using UnityEngine;

    [CreateAssetMenu(fileName = "new item", menuName = "Game/Item")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        public int value;
        public ItemType type= ItemType.item;
    }

    public enum ItemType
    {
        item,enemy
    }
