using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles character selection and character preview
public class CharacterSelectHandler : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    private IReadOnlyList<CharacterData> _characters;
    private int characterIndex;
    private void Awake()
    {
        //_characters = GameManager.Instance.Characters;
        _characters = GameEvents.OnGetCharacter?.Invoke();
    }
    private void OnEnable()
    {
        RefreshCharacter();
        characterIndex = SaveManager.LoadCharacter();
    }
    public void CliCkRightButton()
    {
        characterIndex++;
        WrapIndex();
        RefreshCharacter();
    }

    public void CliCkLeftButton()
    {
        characterIndex--;
        WrapIndex();
        RefreshCharacter();
    }
    // Keep the character index within the available range
    private void WrapIndex()
    {
        if (characterIndex >= _characters.Count)
            characterIndex = 0;

        if (characterIndex < 0)
            characterIndex = _characters.Count - 1;
    }
    public void SelectCharacter()
    {
        SaveManager.SaveCharacter(characterIndex);
        GameEvents.OnSetCharacter?.Invoke();
        gameObject.SetActive(false);
    }
    private void RefreshCharacter()
    {
        characterImage.sprite = _characters[characterIndex].IdleSprite;
    }
}
