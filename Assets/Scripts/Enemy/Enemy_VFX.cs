using UnityEngine;

public class Enemy_VFX : Entity_VFX
{
    [Header("Counter Attack Window")]
    [SerializeField] private GameObject attackAlert;

    public void EnableAttackAlert(bool enable)
    {
        if (attackAlert == null) return;

        attackAlert.SetActive(enable);
    }
}
