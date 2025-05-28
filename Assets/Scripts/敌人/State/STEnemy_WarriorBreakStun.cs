using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class STEnemy_WarriorBreakStun : State
{
    private Enemy e;
    public override void StateExit(Entity entity)
    {
        e.beingBreakStun = false;

        e.isGetHurt = false;
        //e.behaviourTree.SetVariableValue("bIsGetHurt", e.isGetHurt);

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
        MessageManager.CallBreakStun(e);
        entity.beingBreakStun = true;

        //e.isGetHurt = false;
        //e.behaviourTree.SetVariableValue("bIsGetHurt",e.isGetHurt);

        //爆血特效
        string randomBloodVFXName = e.bloodVFXNameList[Random.Range(0, e.bloodVFXNameList.Count)];
        VisualEffectManager.PlayEffectWithoutRotation(randomBloodVFXName, e.transform);
        //踩血特效
        VisualEffectManager.PlayEffectWithoutRotation(e.bloodVFXName, e.transform);
        //出血音效
        SoundEffectManager.PlaySFX("出血", e.transform);
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
