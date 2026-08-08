using System;
using System.Collections.Generic;
    using UnityEngine;
   

    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private List<CharacterData> characters;
         private SpriteRenderer _spriteRenderer;
         private Animator animator;
         public List<CharacterData> Characters => characters;

         private void OnEnable()
         {
             GameEvents.OnSetCharacter += ApplyCharacter;
             GameEvents.OnGetCharacter += GetCharacters;
         }
         private void OnDisable()
         {
             GameEvents.OnSetCharacter -= ApplyCharacter;
             GameEvents.OnGetCharacter -= GetCharacters;
         }
         private void Awake()
        {
            animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
         private IReadOnlyList<CharacterData> GetCharacters()
         {
             return characters;
         }
        private void ApplyCharacter()
        {
           var characterIndex = SaveManager.LoadCharacter();
            var data = characters[characterIndex];
            _spriteRenderer.sprite = data.IdleSprite;
            animator.runtimeAnimatorController = data.animator;
            transform.localScale = Vector3.one * data.scale;
        }
    }
