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
    //[UnityEngine.Tooltip("距离目标多远时会停下")] public float tolerance = 0.1f;
    [UnityEngine.Tooltip("与TargetPosition的距离")] public float distanceWithTarget;
    [UnityEngine.Tooltip("Y轴上的偏移量，>0")] public float yOffset = 0.15f;

    public bool bStuck = false;
    private float stuckTime;

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
        //tolerance = Mathf.Clamp(tolerance, 0.1f, tolerance);
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

        //Vector3 distance = aiPath.destination - entity.transform.position;
        if (aiPath.reachedEndOfPath ||
            aiPath.remainingDistance <= aiPath.endReachedDistance)
        {
            return TaskStatus.Success;
        }
        
        if (!bStuck && aiPath.velocity.magnitude <= 0.01f)
        {
            bStuck = true;
            stuckTime = Time.time;
        }
        if (bStuck)
        {
            if (Time.time - stuckTime >= 0.2f)
            {
                return TaskStatus.Success;
            }
        }

        animator.SetFloat("MoveSpeed", aiPath.maxSpeed);

        if (aiPath.desiredVelocity.x < 0)
        {
            entity.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (aiPath.desiredVelocity.x > 0)
        {
            entity.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        aiPath.canMove = false;
        animator.SetFloat("MoveSpeed", 0);
        bStuck = false;
        base.OnEnd();
    }

    private Vector3 CalculateTargetPosition()
    {
        Vector3 targetPos = targetPosition.Value.position;
        Vector3 targetDir = targetPosition.Value.position - entity.transform.position;

        float offsetX = distanceWithTarget;
        offsetX *= targetDir.x * 10.0f > 0 ? -1 : 1;

        // if (targetDir.magnitude <= (distanceWithTarget))
        // {
        //     tooCloseToPlayer = true;
        // }
        Vector3 pos = targetPos + new Vector3(offsetX, -yOffset, 0);
        var node = AstarPath.active.GetNearest(pos, NNConstraint.Walkable);
        return node.position;
    }
}
