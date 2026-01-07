using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;
    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;

        anim = enemy.anim;
        rb = enemy.rb;
    }
    public override void UpdateAnimationParametars()
    {
        base.UpdateAnimationParametars();

        float battleAnimSpeedMultiplier = enemy.battleMoveSpeed / enemy.moveSpeed;

        anim.SetFloat("battleAnimSpeedMultiplier", battleAnimSpeedMultiplier);
        anim.SetFloat("moveAnimSpeedMultiplier", enemy.moveAnimSpeedMultiplier);
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
    }
}
