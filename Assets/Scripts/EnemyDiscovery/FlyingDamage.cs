
using UnityEngine;

// Handles the behavior of a flying enemy that can damage the player
// and be defeated by taking enough damage.
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
        // Award score and deactivate the enemy when its health reaches zero.
        if (currentHealth<=0)
        {
            GameEvents.OnScoreChanged?.Invoke(Data.score);
            currentHealth = Data.health;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Attack the player when the flying enemy collides with them.
        if (other.CompareTag("Player"))
        {
            Attack(other.transform.GetComponent<Player>());
            gameObject.SetActive(false);
        }
    }
}