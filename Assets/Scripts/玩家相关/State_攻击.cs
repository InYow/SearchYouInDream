using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class State_攻击 : State
{
    public override void StateExit(Entity entity)
    {
        //解绑Entity.AttackBox.OnHurtEntity
        //entity.attackBox.OnHurtEntity -= PlayAttackVFX;
        //销毁
        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        //根据评级，改变播放的Timeline
        RankABCD = RankSystem.GetRankABCD();

        //绑定Timeline-Animator
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        //绑定Entity.AttackBox.OnHurtEntity
        //entity.attackBox.OnHurtEntity += PlayAttackVFX;

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