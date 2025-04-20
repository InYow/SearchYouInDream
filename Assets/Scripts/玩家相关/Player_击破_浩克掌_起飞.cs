using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class Player_击破_浩克掌_起飞 : State
{
    public Entity eTarget;
    public float 小于该距离视为抵达;
    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        //值
        Player player = (Player)entity;
        eTarget = player.eTarget_击破;
        player.eTarget_击破 = null;

        //根据评级，改变播放的Timeline
        RankABCD = RankSystem.GetRankABCD();

        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        //速度为0
        player._rb.velocity = Vector2.zero;
    }

    public override void UPStateBehaviour(Entity entity)
    {
        Player player = (Player)entity;
        //移动
        Vector2 forward = (eTarget.transform.position - player.transform.position).normalized;
        player._rb.velocity = player.speed_fly * forward;
    }
    public override void UPStateInit(Entity entity)
    {
    }
    public override bool Finished(Entity entity)
    {
        float distance = (eTarget.transform.position - entity.transform.position).magnitude;
        if (distance <= 小于该距离视为抵达)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
