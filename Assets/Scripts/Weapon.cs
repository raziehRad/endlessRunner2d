using DefaultNamespace;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ItemData data;

    public ItemData Data => data;

    public virtual void Fire() { }

    public virtual void TakeDamage(IDamageable iDamageable)
    {
        iDamageable.TakeDamage(Data.value);
    }
}