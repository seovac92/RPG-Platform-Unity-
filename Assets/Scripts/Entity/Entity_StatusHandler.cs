using System.Collections;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entityVFX;
    private Entity_Stats entityStats;
    private Entity_Health entityHealth;
    private ElementType currentEffect = ElementType.None;
    [Header("Shock effect details")]
    [SerializeField] private GameObject lightingStrikeVfx;
    [SerializeField] private float currentCharge;
    [SerializeField] private float maximumCharge = 1;
    private Coroutine shockCo;

    void Awake()
    {
        entity = GetComponent<Entity>();
        entityHealth = GetComponent<Entity_Health>();
        entityStats = GetComponent<Entity_Stats>();
        entityVFX = GetComponent<Entity_VFX>();
    }
    public void ApplyStatusEffect(ElementType element, ElementalEffectData effectData)
    {
        if (element == ElementType.Ice && CanBeApplied(ElementType.Ice))
        {
            ApplyChillEffect(effectData.chillDuration, effectData.chillSlowMultiplier);
        }
        if (element == ElementType.Fire && CanBeApplied(ElementType.Fire))
        {
            ApplyBurnEffect(effectData.burnDuration, effectData.totalBurnDamage);
        }
        if (element == ElementType.Lightning && CanBeApplied(ElementType.Lightning))
        {
            ApplyShockEffect(effectData.shockDuration, effectData.shockDamage, effectData.shockCharge);
        }
    }
    public void ApplyShockEffect(float duration, float damage, float charge)
    {
        float lightningResistance = entityStats.GetElementalResistance(ElementType.Lightning);
        float finalCharge = charge * (1 - lightningResistance);

        currentCharge += finalCharge;

        if (currentCharge >= maximumCharge)
        {
            DoLightningStrike(damage);
            StopShockEffect();
            return;
        }
        if (shockCo != null)
        {
            StopCoroutine(shockCo);
        }
        shockCo = StartCoroutine(ShockEffectCo(duration));
    }
    private void StopShockEffect()
    {
        currentEffect = ElementType.None;
        currentCharge = 0;
        entityVFX.StopAllVfx();
    }
    private void DoLightningStrike(float damage)
    {
        Instantiate(lightingStrikeVfx, transform.position, Quaternion.identity);
        entityHealth.ReduceHealth(damage);
    }
    private IEnumerator ShockEffectCo(float duration)
    {
        currentEffect = ElementType.Lightning;
        entityVFX.PlayOnStatusVfx(duration, currentEffect);

        yield return new WaitForSeconds(duration);

        StopShockEffect();
    }
    public void ApplyBurnEffect(float duration, float fireDamage)
    {
        float fireResistance = entityStats.GetElementalResistance(ElementType.Fire);
        float finalDamage = fireDamage * (1 - fireResistance);

        StartCoroutine(BurnEffectCo(duration, finalDamage));
    }
    private IEnumerator BurnEffectCo(float duration, float totalDamage)
    {
        currentEffect = ElementType.Fire;
        entityVFX.PlayOnStatusVfx(duration, currentEffect);

        int ticksPerSecond = 2;
        int tickCount = Mathf.RoundToInt(ticksPerSecond * duration);

        float damagePerTick = totalDamage / tickCount;
        float tickInterval = 1f / ticksPerSecond;

        for (int i = 0; i < tickCount; i++)
        {
            entityHealth.ReduceHealth(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }
        currentEffect = ElementType.None;
    }
    public void ApplyChillEffect(float duration, float slowMultiplier)
    {
        float iceResistance = entityStats.GetElementalResistance(ElementType.Ice);
        float finalDuration = duration * (1 - iceResistance);

        StartCoroutine(ChillEffectCo(finalDuration, slowMultiplier));
    }
    private IEnumerator ChillEffectCo(float duration, float slowMultiplier)
    {
        entity.SlowDownEntity(duration, slowMultiplier);
        currentEffect = ElementType.Ice;
        entityVFX.PlayOnStatusVfx(duration, currentEffect);

        yield return new WaitForSeconds(duration);

        currentEffect = ElementType.None;
    }
    public bool CanBeApplied(ElementType element)
    {
        if (element == ElementType.Lightning && currentEffect == ElementType.Lightning)
        {
            return true;
        }

        return currentEffect == ElementType.None;
    }
}
