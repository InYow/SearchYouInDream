using BehaviorDesigner.Runtime;
using UnityEngine;


public class Enemy_ShooterBase : Enemy
{
    public bool shootEnable = true;
    public float shootCD = 5.0f;
    public float shootDistance;

    private float dashCDStartTime;

    public override void UPStateBehaviour()
    {
        base.UPStateBehaviour();
        if (!shootEnable)
        {
            float duration = Time.time - dashCDStartTime;
            if (duration >= shootCD)
            {
                shootEnable = true;
                behaviourTree.SetVariableValue("bInShootCD",!shootEnable);
            }
        }
    }

    public void StartShootCD()
    {
        shootEnable = false;
        behaviourTree.SetVariableValue("bInShootCD",!shootEnable);
        dashCDStartTime = Time.time;
        var pTrans = behaviourTree.GetVariable("PlayerTransform") as SharedTransform;
        target = pTrans?.Value;
    }
}