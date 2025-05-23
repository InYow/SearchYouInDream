using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Player_投掷 : State
{
    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        //速度为0
        Player player = (Player)entity;
        player._rb.velocity = Vector2.zero;

        //投掷物品
        player.pickableItem.Throw(player);
    }

    public override void UPStateBehaviour(Entity entity)
    {

    }


    public override void UPStateInit(Entity entity)
    {
    }
}
