using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Pathfinding;
using UnityEngine;

public class BTAction_MoveToPlayer : Action
{
    private Enemy entity;
    private AIPath aiPath;
    private Animator animator;

    public SharedTransform targetPosition;
    
    public override void OnAwake()
    {
        base.OnAwake();
        entity = GetComponent<Enemy>();
        aiPath = entity.aiPath;
        animator = entity.GetComponent<Animator>();
    }

    public override void OnStart()
    {
        base.OnStart();
        aiPath.canMove = true;
        aiPath.destination = targetPosition.Value.position;
        animator.SetFloat("MoveSpeed",aiPath.maxSpeed);
    }

    public override TaskStatus OnUpdate()
    {
        //达到位置后或玩家脱战后结束任务
        if (aiPath.reachedEndOfPath || targetPosition.Value == null)
        {
            return TaskStatus.Success;
        }
        //更新位置
        if (aiPath.destination != targetPosition.Value.position)
        {
            aiPath.destination = targetPosition.Value.position;
        }
        
        Vector3 dir = Vector3.Normalize(aiPath.destination - entity.transform.position);
        if (dir.x < 0)
        {
            entity.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(dir.x > 0)
        {
            entity.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        
        return TaskStatus.Running;
    }
    
    public override void OnEnd()
    {
        base.OnEnd();
        aiPath.canMove = false;
        animator.SetFloat("MoveSpeed",0);
    }
}
