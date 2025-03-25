using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class STEnemy_WarriorBreakStun : State
{
    private Entity e;
    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
        e.beingBreakStun = false;

        //清空e的引用
        e = null;
    }

    public override void StateStart(Entity entity)
    {
        //记录e的引用
        e = entity;

        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        entity.transBreakStun = false;
        e.beingBreakStun = true;
    }

    public override void UPStateBehaviour(Entity entity)
    {
        //回复耐力
        entity.resis = Mathf.Clamp(entity.resis + entity.resis_ResponSpeed * Time.deltaTime, 0, entity.resis_Max);
    }

    public override void UPStateInit(Entity entity)
    {
    }

    //----方法----

    /// <summary>
    /// 回满耐力了吗
    /// </summary>
    /// <returns></returns>
    public override bool Finished(Entity entity)
    {
        if (e.resis == e.resis_Max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
