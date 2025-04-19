using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//----------裂地---------
//TODO 减速效果
public class Player_控制_裂地 : State
{
    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        Player player = (Player)entity;
        //速度为0
        player._rb.velocity = Vector2.zero;
    }


    public override void UPStateBehaviour(Entity entity)
    {

    }


    public override void UPStateInit(Entity entity)
    {
    }
}
