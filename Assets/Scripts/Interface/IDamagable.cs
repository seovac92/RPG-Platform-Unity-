using UnityEngine;

public interface IDamagable
{
    public bool TakeDamage(float damage, Transform damageDealer);
}
