using System;
using UnityEngine;
using UnityEngine.UI;


public class CharacterSelectHandler : MonoBehaviour
{
    [SerializeField] private Image characterImage;

    private int characterIndex;

    private void OnEnable()
    {
        var characters = GameManager.Instance.Player.GetCharacterList;
        characterImage.sprite= characters[SaveManager.LoadCharacter()].IdleSprite;
        characterIndex = SaveManager.LoadCharacter();
    }

    public void CliCkRightButton()
    {
        var characters = GameManager.Instance.Player.GetCharacterList;

        characterIndex++;

        if (characterIndex >= characters.Count)
        {
            characterIndex = 0;
        }

        characterImage.sprite = characters[characterIndex].IdleSprite;
    }
    public void CliCkLeftButton()
    {
        var characters = GameManager.Instance.Player.GetCharacterList;

        characterIndex--;

        if (characterIndex < 0)
        {
            characterIndex = characters.Count - 1;
        }

        characterImage.sprite = characters[characterIndex].IdleSprite;
    }
    public void SelectCharacter()
    {
        SaveManager.SaveCharacter(characterIndex);
        GameManager.Instance.Player.SetCharacter();
        gameObject.SetActive(false);
    }
}
