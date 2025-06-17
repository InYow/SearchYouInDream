using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//----------裂地---------
//TODO 减速效果
public class Player_控制_裂地 : State_攻击
{
    public override void StateStart(Entity entity)
    {
        base.StateStart(entity);
        SoundManager_New.Play("裂地");
    }

    public override void UPStateInit(Entity entity)
    {

    }
}
