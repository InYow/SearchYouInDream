using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class Player_终结技 : State_攻击
{
    public override void StateExit(Entity entity)
    {
        base.StateExit(entity);
    }

    public override void StateStart(Entity entity)
    {
        base.StateStart(entity);

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
