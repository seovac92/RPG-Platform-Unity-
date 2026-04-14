using UnityEngine;

public enum SkillUpgradeType
{
    None,
    //---------------------Dash tree---------------------------
    Dash,//dash to avoid damage
    Dash_CloneOnStart,//Create a clone when dash start
    Dash_CloneOnStartAndArrival,//Create a clone when dash start and ends
    Dash_ShardOnStart,//Create a shard when dash start
    Dash_ShardOnStartAndArrival,//Create a shard when dash start and ends
    //---------------------Shard tree---------------------------
    Shard, //The shard explodes when touched by an enemy or time goes up
    Shard_MoveToEnemy,//The shard will move toward nearest enemy
    Shard_TripleCast,//The shard ability can have up to N changes. You can cast them all in a raw
    Shard_Teleport,//You can swap places with the last you created 
    Shard_TeleportAndHeal,//When you swap places with the shard, your HP % is same as it was when you created shard
}
