using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class STEnemy_WarriorDieLayOn : State
{
    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        Enemy enemy = (Enemy)entity;
        enemy._rb.simulated = false;
    }

    public override void UPStateBehaviour(Entity entity)
    {

    }

    public override void UPStateInit(Entity entity)
    {
    }
}
