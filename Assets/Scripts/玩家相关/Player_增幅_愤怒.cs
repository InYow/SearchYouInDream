using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//----------愤怒---------
// 攻速效果
public class Player_增幅_愤怒 : State
{
    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        //增加一个评级
        RankSystem.UpLevelRank();

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
