
    using UnityEngine;

    public class Weapon : MonoBehaviour
    {
        public int _damage;

        public virtual void Fire()
        {
            
        }

        public virtual void TakeDamage(IDamageable iDamageable)
        {
            iDamageable.TakeDamage(_damage);
        }
    }
