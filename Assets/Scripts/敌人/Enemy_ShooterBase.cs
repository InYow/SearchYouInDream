using BehaviorDesigner.Runtime;
using UnityEngine;


public class Enemy_ShooterBase : Enemy
{
    public Transform projectileSpawnTransform;
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

    public virtual void StartShootCD()
    {
        shootEnable = false;
        behaviourTree.SetVariableValue("bInShootCD",!shootEnable);
        dashCDStartTime = Time.time;
        
        var pTrans = behaviourTree.GetVariable("PlayerTransform") as SharedTransform;
        target = pTrans?.Value;
        var foundPlayer = behaviourTree.GetVariable("bFoundPlayer") as SharedBool;
        bFoundPlayer = foundPlayer.Value;
    }
    
    public Transform GetProjectileSpawnTransform()
    {
        return projectileSpawnTransform;
    }
}