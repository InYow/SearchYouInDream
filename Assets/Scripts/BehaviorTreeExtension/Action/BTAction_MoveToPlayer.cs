using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Pathfinding;
using UnityEngine;

public class BTAction_MoveToPlayer : Action
{
    private Enemy entity;
    private AIPath aiPath;
    private Animator animator;
    private Vector3 destinationCache;
    private string walkState = "STEmpty";


    public SharedTransform targetPosition;
    [UnityEngine.Tooltip("距离目标多远时会停下")]public float tolerance = 0.1f;
    [UnityEngine.Tooltip("与TargetPosition的距离")]public float distanceWithTarget;
    [UnityEngine.Tooltip("Y轴上的偏移量，>0")]public float yOffset = 0.15f;
    
    
    
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
        entity.StateCurrent = entity.InstantiateState(walkState);
        aiPath.canMove = true;
        destinationCache = CalculateTargetPosition();
        aiPath.destination = destinationCache; 
        animator.SetFloat("MoveSpeed", aiPath.maxSpeed);
    }

    public override TaskStatus OnUpdate()
    {
        //达到位置后或玩家脱战后结束任务
        if (aiPath.reachedEndOfPath || targetPosition.Value == null)
        {
            return TaskStatus.Success;
        }
        //更新位置
        if (aiPath.destination != destinationCache)
        {
            destinationCache = CalculateTargetPosition();
            aiPath.destination = destinationCache; 
        }

        Vector3 distance = aiPath.destination - entity.transform.position;
        if (distance.magnitude <= tolerance)
        {
            return TaskStatus.Success;
        }
        animator.SetFloat("MoveSpeed", aiPath.maxSpeed);
        Vector3 dir = Vector3.Normalize(distance);
        if (dir.x < 0)
        {
            entity.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (dir.x > 0)
        {
            entity.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        aiPath.canMove = false;
        animator.SetFloat("MoveSpeed", 0);
        base.OnEnd();
    }

    private Vector3 CalculateTargetPosition()
    {
        Vector3 targetPos = targetPosition.Value.position; 
        Vector3 targetDir = targetPosition.Value.position - entity.transform.position;
        float offsetX = targetDir.x > 0 ?  -distanceWithTarget : distanceWithTarget;
        return targetPos+new Vector3(offsetX,-yOffset,0);
    }
}
