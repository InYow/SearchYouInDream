using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class BreakableItem_破碎 : State
{
    public HitFly hitFly;
    public override void StateExit(Entity entity)
    {
        //销毁
        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        //根据BreakableItem.Timeline改变播放的Timeline

        //绑定Timeline-Animator
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();


        //速度为0
        BreakableItem breakableItem = (BreakableItem)entity;
        breakableItem._rb.velocity = Vector2.zero;
    }

    public override void UPStateBehaviour(Entity entity)
    {

    }
    public override void UPStateInit(Entity entity)
    {
    }
}