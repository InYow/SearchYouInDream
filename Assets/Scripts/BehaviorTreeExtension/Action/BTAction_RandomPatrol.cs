using BehaviorDesigner.Runtime.Tasks;
using Pathfinding;
using UnityEngine;

namespace BehaviorTreeExtension
{
    public class BTAction_RandomPatrol : Action
    {
        public float PatrolRange;
        
        private Enemy entity;
        private AIPath aiPath;
        private Animator animator;
        
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
            
            aiPath.destination = Random.insideUnitCircle*PatrolRange;
            
            aiPath.canMove = true;
            animator.SetFloat("MoveSpeed",aiPath.maxSpeed);
        }

        public override TaskStatus OnUpdate()
        {
            //达到位置后结束任务
            if (aiPath.reachedEndOfPath)
            {
                return TaskStatus.Success;
            }
        
            Vector3 dir = Vector3.Normalize(aiPath.destination - entity.transform.position);
            animator.SetFloat("MoveSpeed",aiPath.maxSpeed);
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
            aiPath.canMove = false;
            animator.SetFloat("MoveSpeed",0);
            
            base.OnEnd();
        }
    }
}
