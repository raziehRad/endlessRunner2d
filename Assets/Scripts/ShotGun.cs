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
        var bollet = Instantiate(_bolletPrefab, _instancePosition.position, Quaternion.identity, transform);
        bollet.GetComponent<Bullet>().SetDamage(Data.value);
        Rigidbody2D rb = bollet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.right * speed;
    }

    public override void TakeDamage(IDamageable iDamageable)
    {
        iDamageable.TakeDamage(Data.value);
    }
}