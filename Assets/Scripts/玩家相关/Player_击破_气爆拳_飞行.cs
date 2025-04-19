using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class Player_击破_气爆拳_飞行 : State
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
        eTarget = player.eTarget_普攻1_冲刺_阶段2;
        player.eTarget_普攻1_冲刺_阶段2 = null;

        //根据评级，改变播放的Timeline
        //FIN 将根据评级变化的逻辑简化为设置枚举值
        //FIN 根据枚举值的不同，读取不同Timeline的自定义轨道信息
        RankABCD = RankSystem.GetRankABCD();

        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

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
