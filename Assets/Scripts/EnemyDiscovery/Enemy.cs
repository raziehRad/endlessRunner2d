using DefaultNamespace;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData data;
    public EnemyData Data => data;

    public virtual void Attack(Player player)
    {
        player.TakeDamage((int)data.damage);
    }

    public virtual void TakeDamage(int damage)
    {
    }
}