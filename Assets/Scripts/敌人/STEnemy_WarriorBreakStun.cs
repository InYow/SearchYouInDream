using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class STEnemy_WarriorBreakStun : State
{
    private Enemy e;
    public override void StateExit(Entity entity)
    {
        e.beingBreakStun = false;
        entity.transBreakStun = false;
        e.behaviourTree.SetVariableValue("bStun", entity.transBreakStun);
        //清空e的引用
        e = null;
        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        //记录e的引用
        e = entity as Enemy;

        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        //将信息传递出去
        MessageManager.BreakStun(e);
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
        else if (entity.transExecution)
        {
            e.behaviourTree.SetVariableValue("bCanExecute", entity.transExecution);
            return true;
        }

        return false;
    }

}
