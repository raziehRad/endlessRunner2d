using DefaultNamespace;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ItemData data;

    [SerializeField] private SpriteRenderer gunImage;
    public ItemData Data => data;
    private bool isPowerUp;
    private int powerUpDamage;
    protected int Damage => isPowerUp ? powerUpDamage : data.value;
    public virtual void Fire() { }

    public virtual void TakeDamage(IDamageable iDamageable)
    {
        iDamageable.TakeDamage(Damage);
    }

    public void SetPowerUp(ItemData itemData, bool enable)
    {
        isPowerUp = enable;
        powerUpDamage = itemData.value;
        gunImage.sprite = enable? itemData.icon: data.icon;
    }
}