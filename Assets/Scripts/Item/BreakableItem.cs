using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableItem : Entity
{
    public Collider2D colli;
    public override void UPCheckStateTrans()
    {
        if (health <= 0f && stateCurrentName != "BreakableItem_破碎")
        {
            StateCurrent = InstantiateState("BreakableItem_破碎");
        }
    }

    public override State InstantiateState(string str)
    {
        GameObject StatePrb = Resources.Load<GameObject>("Prefabs/State/BreakableItem/" + str);
        Debug.Log(StatePrb.name);
        GameObject go = Instantiate(StatePrb, transform);
        return go.GetComponent<State>();
    }

    public override void Execution(Entity entity, CheckAttackBoxBehaviour checkBoxBehaviour)
    {
        if (checkBoxBehaviour.attacktype == AttackType.none)
        {
            gameObject.layer = 2;

            transExecution = true;
            transExecution_Type = "fly";
            transExecution_DamageSourceEntity = entity;
            transExecution_AttackBoxBehaviour = checkBoxBehaviour;
            bool b = checkBoxBehaviour.entity_master.transform.lossyScale.x == -1 ? true : false;
            FlipX(b);
        }
    }

    public void SetColliderFalse()
    {
        colli.gameObject.SetActive(false);
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
