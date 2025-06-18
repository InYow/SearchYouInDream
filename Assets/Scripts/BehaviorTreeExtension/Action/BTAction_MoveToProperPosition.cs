using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviorTreeExtension
{
    public class BTAction_MoveToProperPosition : Action
    {
        public SharedTransform playerTransform;
        public float moveMaxDistance;//敌人与玩家的最大距离
        public float moveMinDistance;//敌人与玩家的最小距离

        private Enemy entity;
        private AIPath aiPath;
        private Animator animator;
        private bool bStuck = false;
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
            aiPath.destination = GetTargetPosition();

            aiPath.canMove = true;
            animator.SetFloat("MoveSpeed", aiPath.maxSpeed);
            bStuck = false;
        }

        private Vector3 GetTargetPosition()
        {
            // Vector3 dir =  Vector3.Normalize(entity.transform.position - playerTransform.Value.position);
            //
            // Vector3 right = Vector3.Normalize(Vector3.Cross( Vector3.forward,dir));
            //
            // float theta = Random.Range(0.25f * Mathf.PI, 0.75f * Mathf.PI);
            // float dist = Random.Range(moveMinDistance, moveMaxDistance);
            // Vector3 destination = playerTransform.Value.position + dist*(right*Mathf.Cos(theta)+dir*Mathf.Sin(theta));
            //
            // GraphNode node = AstarPath.active.GetNearest(destination, NNConstraint.Walkable).node;
            // node.
            //
            // return (Vector3)node.position;
            Vector3 enemyPos = transform.position;
            Vector3 playerPos = playerTransform.Value.position;
            Vector3 baseDir = (enemyPos - playerPos).normalized;

            float bestDistance = -1f;
            Vector3 bestTarget = transform.position;

            for (int i = 0; i < 8; i++)
            {
                // 扇形采样多个方向
                float angle = (360f / 8) * i;
                Vector3 dir = Quaternion.Euler(0, 0, angle) * baseDir;
                Vector3 samplePoint = enemyPos + dir * Random.Range(moveMinDistance, moveMaxDistance);

                var nearest = AstarPath.active.GetNearest(samplePoint);
                if (nearest.node != null && nearest.node.Walkable)
                {
                    float distanceToPlayer = Vector2.Distance(nearest.position, playerPos);

                    if (distanceToPlayer > bestDistance)
                    {
                        bestDistance = distanceToPlayer;
                        bestTarget = (Vector3)nearest.position;
                    }
                }
            }

            return bestTarget;
        }

        public override TaskStatus OnUpdate()
        {
            //达到位置后或玩家脱战后结束任务
            if (aiPath.reachedEndOfPath || playerTransform.Value == null)
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

            Vector3 dir = Vector3.Normalize(aiPath.destination - entity.transform.position);
            animator.SetFloat("MoveSpeed", aiPath.maxSpeed);
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

            if (playerTransform.Value != null)
            {
                Vector3 dir = Vector3.Normalize(playerTransform.Value.position - entity.transform.position);
                if (dir.x < 0)
                {
                    entity.gameObject.transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (dir.x > 0)
                {
                    entity.gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
            }

            base.OnEnd();
        }
    }
}