using System;
using UnityEngine;

public class FlyingDamage : Enemy
{
    public override void Attack(Player player)
    {
        base.Attack(player);
    }

    public override void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health<0)
        {
            HUDManager.instace.SetPlayerScore(_score);
            gameObject.SetActive(false);
            _health = 100;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Attack(other.transform.GetComponent<Player>());
        }
    }
}