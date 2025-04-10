using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Pathfinding;
using UnityEngine;

namespace BehaviorTreeExtension
{
    public class BTAction_MoveToProperPosition : Action
    {
        public SharedTransform playerTransform;
        public float moveMaxDistance;//与敌人自己当前位置的最大距离
        public float moveMinDistance;//与敌人自己当前位置的最小距离
        
        private Enemy entity;
        private AIPath aiPath;
        private AIDestinationSetter aiDestinationSetter;
        private Animator animator;
        
        private Vector3 playerPosTemp;
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
            playerPosTemp = playerTransform.Value.position;
            aiPath.canMove = true;
            animator.SetFloat("MoveSpeed",aiPath.maxSpeed);
        }

        private Vector3 GetTargetPosition()
        {
            Vector3 dir = Vector3.Normalize(entity.transform.position - playerTransform.Value.position);
            Vector3 right = Vector3.Normalize(Vector3.Cross( Vector3.forward,dir));
            
            float theta = Random.Range(0f, Mathf.PI);
            float dist = Random.Range(moveMinDistance, moveMaxDistance);
            Vector3 offset = dist*(right*Mathf.Cos(theta)+dir*Mathf.Sin(theta));
            
            return entity.transform.position + offset;
        }

        public override TaskStatus OnUpdate()
        {
            //达到位置后或玩家脱战后结束任务
            if (aiPath.reachedEndOfPath || playerTransform.Value == null)
            {
                return TaskStatus.Success;
            }
            //更新位置
            if (playerPosTemp != playerTransform.Value.position)
            {
                aiPath.destination = GetTargetPosition();
                playerPosTemp = playerTransform.Value.position;
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
}