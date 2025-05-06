using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class Player_普攻1 : State_攻击
{
    public override void StateExit(Entity entity)
    {
        base.StateExit(entity);
        BFPlayerAttackContinuity_New bFPlayerAttackContinuity_New = (BFPlayerAttackContinuity_New)entity.BuffAdd("BFPlayerAttackContinuity_New");
        bFPlayerAttackContinuity_New.attackID = 1;
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

    public override void UPStateBehaviour(Entity entity)
    {
        base.UPStateBehaviour(entity);
    }
    public override void UPStateInit(Entity entity)
    {
        base.UPStateInit(entity);
    }
}
