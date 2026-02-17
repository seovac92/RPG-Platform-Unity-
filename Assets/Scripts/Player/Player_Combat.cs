using UnityEngine;

public class Player_Combat : Entity_Combat
{
    [Header("Counter Attack details")]
    [SerializeField] private float counterRecovery = .1f;
    public bool CounterAttackPerformed()
    {
        bool hasPerformerdCounter = false;
        foreach (var target in GetDetectedColliders())
        {
            ICounterable counterable = target.GetComponent<ICounterable>();
            if (counterable == null) continue;

            if (counterable.CanBeCountered)
            {
                counterable.HandleCounter();
                hasPerformerdCounter = true;
            }
        }
        return hasPerformerdCounter;
    }
    public float GetCounterRecoveryDuration() => counterRecovery;
}
