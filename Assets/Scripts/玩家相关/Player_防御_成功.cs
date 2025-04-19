using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Player_防御_成功 : State
{
    public Entity target;
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

        //降低目标耐性
        target = player.transDefend_achieve_TgtEntity;
        target.resis -= player.attackResisValue * 1.5f;

        //转换条件
        player.transDefend_Achieve = false;
        player.transDefend_achieve_TgtEntity = null;

        //no defend-cd
        player.cd_DefendOrAngry = 0f;
    }

    public override void UPStateBehaviour(Entity entity)
    {

    }


    public override void UPStateInit(Entity entity)
    {
    }
}
