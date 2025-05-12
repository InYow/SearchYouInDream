using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STEnemy_PlayTimelineAsset : State
{
    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();
    }

    public override void UPStateInit(Entity entity)
    { }

    public override void UPStateBehaviour(Entity entity)
    { }

    public override void StateExit(Entity entity)
    {
        Destroy(this.gameObject);
    }
}
