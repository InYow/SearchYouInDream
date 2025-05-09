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

    public override void GetHurt(Entity entity, CheckBox attackBox)
    {
        Debug.Log(entity.name);
        if (!BuffContain("BFPlayerUnselected"))
        {
            health -= entity.attackValue;

            FlowBlood();

            isGetHurt = true;
            behaviourTree.SetVariableValue("bIsGetHurt", isGetHurt);

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
            FlowBlood();

            isGetHurt = true;
            //behaviourTree.SetVariableValue("bIsGetHurt", isGetHurt);

            //死掉了
            if (health <= 0f)
            {
                Execution(entity, checkBoxBehaviour);
            }
        }
    }
}
