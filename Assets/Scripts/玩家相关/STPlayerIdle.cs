using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

//状态行为
//-开始时播放待机动画
//转换条件
//-按下移动键，进入移动状态
public class STPlayerIdle : State
{

    public override void UPStateBehaviour(Entity entity)
    {

    }

    public override void UPStateInit(Entity entity)
    {
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        //速度为0
        Player player = (Player)entity;
        player._rb.velocity = Vector2.zero;
    }

    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
    }
}
