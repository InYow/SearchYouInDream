using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class Player_击破 : State_攻击
{
    public override void StateExit(Entity entity)
    {
        base.StateExit(entity);
        SlowMotion.FinishSlow();
        entity.BuffRemove("BFPlayerGuideBreakAttack");
    }

    public override void StateStart(Entity entity)
    {
        base.StateStart(entity);
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
