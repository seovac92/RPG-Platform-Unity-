using UnityEngine;

public class Skill_Shard : Skill_Base
{
    private SkillObject_Shard currentShard;
    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private float detonationTime = 2;
    [Header("Moving Shard Upgrade")]
    [SerializeField] private float shardSpeed = 7;

    public override void TryUseSkill()
    {
        base.TryUseSkill();

        if (!CanUseSkill()) return;

        if (Unlocked(SkillUpgradeType.Shard))
        {
            HandleShardRegular();
        }
        if (Unlocked(SkillUpgradeType.Shard_MoveToEnemy))
        {
            HandleShardMoving();
        }
    }
    private void HandleShardMoving()
    {
        CreateShard();
        currentShard.MoveTowardsClosestTarget(shardSpeed);
        SetSkillOnCooldown();
    }
    private void HandleShardRegular()
    {
        CreateShard();
        SetSkillOnCooldown();
    }
    public void CreateShard()
    {
        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        currentShard = shard.GetComponent<SkillObject_Shard>();
        currentShard.SetupShard(detonationTime);
    }
}
