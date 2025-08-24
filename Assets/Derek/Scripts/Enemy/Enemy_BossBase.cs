using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_BossBase : Enemy_DashBase
{
    private bool bInSecondState = false;

    public override void StartBreakStun(Entity entity)
    {
        transBreakStun = true;
        var enemy = this as Enemy;
        if (enemy)
        {
            enemy.behaviourTree.SetVariableValue("bStun", enemy.transBreakStun);
        }
        SoundManager_New.PlayIfFinish("Boss破防");
    }

    // old version
    public override void GetHurt(Entity entity, CheckBox attackBox)
    {
        //Debug.Log(entity.name);
        if (!BuffContain("BFPlayerUnselected"))
        {
            health -= entity.attackValue;
            EnemyInfoUIList.instance.AddEnemyInfoUI(this); //添加敌人信息UI

            if (transBreakStun || beingBreakStun)
            {
                isGetHurt = true;
                behaviourTree.SetVariableValue("bIsGetHurt", isGetHurt);
            }

            PlayOnHitEffect();

            //死掉了
            if (health <= 0f)
            {
                Execution(entity, attackBox);
                //behaviourTree.DisableBehavior();
            }
            else if (!bInSecondState && health <= health_Max * 0.5f)
            {
                bInSecondState = true;
                behaviourTree.SetVariableValue("bSecondState", bInSecondState);
                //TODO: Broadcast Event 
            }
        }
    }

    // new version
    public override void GetHurt(Entity entity, CheckAttackBoxBehaviour checkBoxBehaviour, float attackValue)
    {
        if (!BuffContain("BFPlayerUnselected"))
        {
            if (attackValue == 0f)
            {
                Debug.Log("entity.attackValue" + entity.attackValue);
                health -= entity.attackValue;
            }
            else
            {
                Debug.Log("attackValue" + attackValue);
                health -= attackValue;
            }
            EnemyInfoUIList.instance.AddEnemyInfoUI(this); //添加敌人信息UI

            if (transBreakStun || beingBreakStun)
            {
                isGetHurt = true;
                behaviourTree.SetVariableValue("bIsGetHurt", isGetHurt);
            }

            PlayOnHitEffect();

            //死掉了
            if (health <= 0f)
            {
                Execution(entity, checkBoxBehaviour);
                //behaviourTree.DisableBehavior();
            }
            else if (!bInSecondState && health <= health_Max / 2)
            {
                bInSecondState = true;
                behaviourTree.SetVariableValue("bSecondState", bInSecondState);
                //TODO: Broadcast Event
            }
        }
    }
}
