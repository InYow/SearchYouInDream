using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Pathfinding;
using UnityEngine;

public class Enemy : Entity
{
    public BehaviorTree behaviourTree;
    public AIPath aiPath;
    public Transform target;
    public bool bFoundPlayer = false;
    public bool isGetHurt = false;
    public string bloodVFXName = "踩血特效1";

    public override void GetHurt(Entity entity, CheckBox attackBox)
    {
        Debug.Log(entity.name);
        if (!BuffContain("BFPlayerUnselected"))
        {
            health -= entity.attackValue;

            GetHurtVFX();
            SoundEffectManager.PlaySFX("命中", transform);

            if (transBreakStun || beingBreakStun)
            {
                isGetHurt = true;
                behaviourTree.SetVariableValue("bIsGetHurt", isGetHurt);
            }

            //死掉了
            if (health <= 0f)
            {
                Execution(entity, attackBox);
            }
        }
    }

    public override void GetHurt(Entity entity, CheckBoxBehaviour checkBoxBehaviour)
    {
        if (!BuffContain("BFPlayerUnselected"))
        {
            health -= entity.attackValue;

            GetHurtVFX();
            SoundEffectManager.PlaySFX("命中", transform);

            if (transBreakStun || beingBreakStun)
            {
                isGetHurt = true;
                behaviourTree.SetVariableValue("bIsGetHurt", isGetHurt);
            }

            //死掉了
            if (health <= 0f)
            {
                Execution(entity, checkBoxBehaviour);
            }
        }
    }
}
