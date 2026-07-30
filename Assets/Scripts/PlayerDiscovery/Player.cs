using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerBoost boost;
    [SerializeField] private PlayerStateMachine _stateMachine;
    [SerializeField] private PlayerScore score;
    [SerializeField] private PlayerCharacter  playerCharacter ;
    [SerializeField] private Weapon  weapon ;
    public List<CharacterData> GetCharacterList => playerCharacter.Characters;

    void Update()
    {
        _stateMachine.Tick();
    }

    void FixedUpdate()
    {
        _movement.Tick();
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
    }

    public void ApplyItem(ItemData item, Collider2D other=null)
    {
        switch (item.effect)
        {
            case ItemEffect.Heal:
                health.Heal(item.value);
                break;

            case ItemEffect.Shield:
                boost.EnableShield(item.duration);
                break;

            case ItemEffect.WeaponUpgrade:
                boost.EnablePowerUp(item);
                break;
            case ItemEffect.Flying:
                boost.EnableFlying(item);
                break;
            case ItemEffect.AddCoin:
                score.AddCoin(item.value,other);
                break;
            case ItemEffect.Jump:
                JumpMode(item);
                break;
        }
    }

    private void JumpMode(ItemData itemData)
    {
        _movement.JumpMode(itemData);
    }

    public void SetCharacter()
    {
        playerCharacter.ApplyCharacter(SaveManager.LoadCharacter());
    }

    public void SetPowerUp(ItemData itemData, bool enable)
    {
        weapon.SetPowerUp(itemData,enable);
    }

    public void SetFlying( bool enable, int flyingHeight)
    {
        _movement.FlyingMode(enable,flyingHeight);
    }
}
