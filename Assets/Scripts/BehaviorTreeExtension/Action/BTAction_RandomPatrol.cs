using BehaviorDesigner.Runtime.Tasks;
using Pathfinding;
using UnityEngine;

namespace BehaviorTreeExtension
{
    public class BTAction_RandomPatrol : Action
    {
        public float PatrolRange = 5.0f;

        private Enemy entity;
        private AIPath aiPath;
        private Animator animator;
        private float randomAngle = 20.0f;
        private Vector3 lastMoveDirection;

        private bool bStuck = false;
        private float stuckTime;

        public override void OnAwake()
        {
            base.OnAwake();
            entity = GetComponent<Enemy>();
            aiPath = entity.aiPath;
            animator = entity.GetComponent<Animator>();
            lastMoveDirection = Vector3.zero;
        }

        public override void OnStart()
        {
            base.OnStart();

            Vector3 reflected = GetReflectedDirectionFromBoundary(entity.transform.position);
            if (reflected == Vector3.zero)
            {
                // 非边界位置，使用随机方向
                Vector2 random = Random.insideUnitCircle.normalized;
                lastMoveDirection = new Vector3(random.x, random.y, 0.0f);
            }
            else
            {
                lastMoveDirection = reflected; //Vector3.Reflect(lastMoveDirection,reflected).normalized;
            }

            Vector3 targetPos = entity.transform.position + lastMoveDirection * Random.Range(0.5f, 1.0f) * PatrolRange;
            aiPath.destination = targetPos;
            aiPath.canMove = true;

            animator.SetFloat("MoveSpeed", aiPath.maxSpeed);
        }

        public override TaskStatus OnUpdate()
        {
            if (aiPath.reachedEndOfPath)
            {
                return TaskStatus.Success;
            }

            if (!bStuck && aiPath.desiredVelocity.magnitude <= 0.001f)
            {
                bStuck = true;
                stuckTime = Time.time;
            }
            if (bStuck)
            {
                if (Time.time - stuckTime >= 0.45f)
                {
                    return TaskStatus.Success;
                }
            }

            //Vector3 dir = (aiPath.destination - entity.transform.position);
            animator.SetFloat("MoveSpeed", aiPath.maxSpeed);
            float moveX = aiPath.desiredVelocity.x;
            if (Mathf.Abs(moveX) > 0.01f) // 防止抖动
            {
                entity.transform.localScale = new Vector3(Mathf.Sign(moveX), 1, 1);
            }

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            aiPath.canMove = false;
            animator.SetFloat("MoveSpeed", 0);

            base.OnEnd();
        }

        /// <summary>
        /// 如果敌人在边界上，返回一个反射方向（远离边界）；否则返回 Vector3.zero。
        /// </summary>
        private Vector3 GetReflectedDirectionFromBoundary(Vector3 position)
        {
            var gridGraph = AstarPath.active.data.gridGraph;
            var nodeInfo = AstarPath.active.GetNearest(position);
            var gridNode = nodeInfo.node as GridNode;

            if (gridNode == null) return Vector3.zero;

            int x = gridNode.XCoordinateInGrid;
            int z = gridNode.ZCoordinateInGrid;

            int width = gridGraph.width;
            int depth = gridGraph.depth;

            Vector3 reflectDir = Vector3.zero;

            // 检查四周格子是否越界（不可访问）
            if (x <= 0) reflectDir += Vector3.right;
            else if (x >= width - 1) reflectDir += Vector3.left;

            if (z <= 0) reflectDir += Vector3.up;
            else if (z >= depth - 1) reflectDir += Vector3.down;

            reflectDir = reflectDir.normalized;

            float t = Random.value; // [0, 1] 均匀分布
            t = Mathf.Sqrt(t);      // 变成 [0, 1] 的「两头高中间低」分布
            float angleOffset = Mathf.Lerp(-randomAngle, randomAngle, t); // 映射回角度区间

            Quaternion q = Quaternion.AngleAxis(angleOffset, Vector3.forward);
            reflectDir = q * reflectDir;

            return reflectDir.normalized;
        }
    }
}
