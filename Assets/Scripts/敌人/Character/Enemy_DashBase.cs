using BehaviorDesigner.Runtime;
using UnityEngine;

public class Enemy_DashBase : Enemy
{
    public bool dashEnable = true;
    public float dashCD = 5.0f;
    public float dashSpeed;
    public float dashDistance;

    private float dashCDStartTime;
    public float stopDistance;

    public override void UPStateBehaviour()
    {
        base.UPStateBehaviour();
        if (!dashEnable)
        {
            float duration = Time.time - dashCDStartTime;
            if (duration >= dashCD)
            {
                dashEnable = true;
                behaviourTree.SetVariableValue("bInCD",!dashEnable);
                EnemyController.instance.EnemyEndCD(this);
            }
        }
    }

    public void StartDashCD()
    {
        dashEnable = false;
        behaviourTree.SetVariableValue("bInCD",!dashEnable);
        EnemyController.instance.EnemyStartCD(this);
        
        dashCDStartTime = Time.time;
        
        var pTrans = behaviourTree.GetVariable("PlayerTransform") as SharedTransform;
        target = pTrans?.Value;
    }
}
