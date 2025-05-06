using UnityEngine;



public class Player_普攻3 : State_攻击
{
    public override void StateExit(Entity entity)
    {
        base.StateExit(entity);
        BFPlayerAttackContinuity_New bFPlayerAttackContinuity_New = (BFPlayerAttackContinuity_New)entity.BuffAdd("BFPlayerAttackContinuity_New");
        bFPlayerAttackContinuity_New.attackID = 3;
    }
    public override void StateStart(Entity entity)
    {
        base.StateStart(entity);
        entity.BuffRemove("BFPlayerAttackContinuity_New");

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
        }
        else if (Input.GetKey(KeyCode.A))
        {
            entity.FlipX(true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            entity.FlipX(false);
        }
    }

}
