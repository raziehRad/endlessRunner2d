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
        data.health -= damage;
        if (data.health<0)
        {
            HUDManager.instace.SetPlayerScore(data.score);
            gameObject.SetActive(false);
            data.health = 100;
            
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