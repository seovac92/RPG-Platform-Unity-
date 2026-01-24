using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;

    public virtual void TakeDamage(float damage)
    {
        if (isDead) return;
        ReduceHp(damage);
    }
    protected void ReduceHp(float damage)
    {
        maxHp -= damage;
        if (maxHp < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
    }
}
