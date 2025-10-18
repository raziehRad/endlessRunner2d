using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    public EnemyData data;
    // public int _damage=10;
    // public int _health = 100;
    // public int _score = 20;
   public virtual void Attack(Player player)
   {
      player.TakeDamage((int)data.damage);
   }

   public virtual void TakeDamage(int damage)
   {
     
   }
}
