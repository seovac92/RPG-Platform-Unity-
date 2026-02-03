using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();
    public override void TakeDamage(float damage, Transform damageDealer)
    {
        if (damageDealer.GetComponent<Player>() != null)
        {
            enemy.TryEnterBattleMode(damageDealer);
        }

        base.TakeDamage(damage, damageDealer);
    }
}
