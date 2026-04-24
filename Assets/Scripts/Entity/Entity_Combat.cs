using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;
    private Entity_Stats stats;

    public DamageScaleData basicAttackScale;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private LayerMask whatIsTarget;
    [Header("Status effect details")]
    [SerializeField] private float defaultDuration = 3f;
    [SerializeField] private float chillSlowMultiplier = 0.2f;
    [SerializeField] private float electrifyChargeBuildUp = 0.4f;
    [Space]
    [SerializeField] private float fireScale = 0.8f;
    [SerializeField] private float lightningScale = 2.5f;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }
    public void PerformAttack()
    {
        Collider2D[] targetColliders = GetDetectedColliders();

        foreach (var target in targetColliders)
        {
            IDamagable damagable = target.GetComponent<IDamagable>();
            if (damagable == null) continue;

            ElementalEffectData effectData = new ElementalEffectData(stats, basicAttackScale);

            float damage = stats.GetPhysicalDamage(out bool isCrit);
            float elementalDamage = stats.GetElementalDamage(out ElementType element);

            bool targetGotHit = damagable.TakeDamage(damage, elementalDamage, element, transform);

            if (element != ElementType.None)
            {
                target.GetComponent<Entity_StatusHandler>().ApplyStatusEffect(element, effectData);
            }

            if (targetGotHit)
            {
                vfx.UpdateOnHitColor(element);
                vfx.CreateOnHitVFX(target.transform, isCrit);
            }
        }
    }
    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
