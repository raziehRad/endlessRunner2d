using System;
using DefaultNamespace;
using UnityEngine;


public class ShotGun : Weapon
{
    [SerializeField] private int speed;
    [SerializeField] private GameObject _bolletPrefab;
    [SerializeField] private Transform _instancePosition;
   


    public void FireButton()
    {
        Fire();
    }
    public override void Fire()
    {
        var bullet = Instantiate(_bolletPrefab, _instancePosition.position, Quaternion.identity, transform);
        bullet.GetComponent<Bullet>().SetDamage(Damage);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.right * speed;
    }

    public override void TakeDamage(IDamageable target)
    {
        target.TakeDamage(Damage);
    }
}