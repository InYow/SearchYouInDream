using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableItem : Entity
{
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
}
