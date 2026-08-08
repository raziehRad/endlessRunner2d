using System;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

public class FlyingDamage : Enemy
{
    private float currentHealth;
    private void OnEnable()
    {
        currentHealth = Data.health;
    }

    public override void Attack(Player player)
    {
        base.Attack(player);
    }

    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("take damage"+currentHealth);
        if (currentHealth<=0)
        {
            GameEvents.OnScoreChanged?.Invoke(Data.score);
            currentHealth = Data.health;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Attack(other.transform.GetComponent<Player>());
            gameObject.SetActive(false);
        }
    }
}