using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bullet : MonoBehaviour
    {
        private int _damage;

        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(gameObject,0.2f);
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player>().TakeDamage(_damage);
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
            }
        }

        private void Start()
        {
            Destroy(gameObject,1f);
        }
    }
}