using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STEmpty : State
{
    public override void UPStateBehaviour(Entity entity)
    {
    }

    public override void UPStateInit(Entity entity)
    {
    }

    public override void StateStart(Entity entity)
    {
    }

    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
    }
}
