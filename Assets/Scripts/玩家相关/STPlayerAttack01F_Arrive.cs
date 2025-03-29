using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//起步动作
public class STPlayerAttack01F_Arrive : State
{
    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        //根据评级，改变播放的Timeline
        RankABCD = RankSystem.GetRankABCD();

        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        //速度为0
        Player player = (Player)entity;
        player._rb.velocity = Vector2.zero;
    }

    public override void UPStateBehaviour(Entity entity)
    {
    }

    public override void UPStateInit(Entity entity)
    {
    }
}
