
    using System.Collections.Generic;
    using UnityEngine;
   

    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator animator;
        [SerializeField] private List<CharacterData> characters;
        public List<CharacterData> Characters => characters;
        public void ApplyCharacter(int characterIndex)
        {
            var data = characters[characterIndex];
            _spriteRenderer.sprite = data.IdleSprite;
            animator.runtimeAnimatorController = data.animator;
            transform.localScale = Vector3.one * data.scale;
        }
    }
