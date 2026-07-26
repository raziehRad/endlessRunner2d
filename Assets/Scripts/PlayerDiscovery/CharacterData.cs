using UnityEngine;

[CreateAssetMenu(fileName = "new Character", menuName = "Game/Character")]

    public class CharacterData : ScriptableObject
    {
        public Sprite IdleSprite;
        public RuntimeAnimatorController animator;
        public float scale = 1f;
        
    }
