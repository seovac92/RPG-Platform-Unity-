using UnityEngine;

public enum SkillUpgradeType
{
    Dash,//dash to avoid damage
    Dash_CloneOnStart,//Create a clone when dash start
    Dash_CloneOnStartAndArrival,//Create a clone when dash start and ends
    Dash_ShardOnStart,//Create a shard when dash start
    Dash_ShardOnStartAndArrival//Create a shard when dash start and ends
}
