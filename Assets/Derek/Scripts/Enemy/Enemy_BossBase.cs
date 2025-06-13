using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_BossBase : Enemy_DashBase
{
    private bool bInSecondState = false;
    
    // old version
    public override void GetHurt(Entity entity, CheckBox attackBox)
    {
        Debug.Log(entity.name);
        if (!BuffContain("BFPlayerUnselected"))
        {
            health -= entity.attackValue;
            EnemyInfoUIList.instance.AddEnemyInfoUI(this); //添加敌人信息UI

            GetHurtVFX();
            SoundEffectManager.PlaySFX01(transform);

            if (transBreakStun || beingBreakStun)
            {
                isGetHurt = true;
                behaviourTree.SetVariableValue("bIsGetHurt", isGetHurt);
            }

            //死掉了
            if (health <= 0f)
            {
                Execution(entity, attackBox);
                //behaviourTree.DisableBehavior();
            }
            else if (!bInSecondState && health <= health_Max*0.5f)
            {
                bInSecondState = true;
                behaviourTree.SetVariableValue("bSecondState",bInSecondState);
                //TODO: Broadcast Event 
            }
        }
    }

    // new version
    public override void GetHurt(Entity entity, CheckAttackBoxBehaviour checkBoxBehaviour)
    {
        if (!BuffContain("BFPlayerUnselected"))
        {
            health -= entity.attackValue;
            EnemyInfoUIList.instance.AddEnemyInfoUI(this); //添加敌人信息UI


            //播放受伤特效
            GetHurtVFX();
            SoundEffectManager.PlaySFX01(transform);

            if (transBreakStun || beingBreakStun)
            {
                isGetHurt = true;
                behaviourTree.SetVariableValue("bIsGetHurt", isGetHurt);
            }

            //死掉了
            if (health <= 0f)
            {
                Execution(entity, checkBoxBehaviour);
                //behaviourTree.DisableBehavior();
            }
            else if (!bInSecondState && health <= health_Max/2)
            {
                bInSecondState = true;
                behaviourTree.SetVariableValue("bSecondState",bInSecondState);
                //TODO: Broadcast Event
            }
        }
    }
}
