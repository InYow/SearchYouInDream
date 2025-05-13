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
        
        private static bool firstRun = true;
        private static Vector3 lastMoveDirection; 
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
            if (firstRun)
            {
                float r = Random.Range(0,1);
                aiPath.destination = Random.insideUnitCircle*(r*PatrolRange);
                lastMoveDirection = (aiPath.destination - entity.transform.position).normalized;
                firstRun = false;
            }
            else
            {
                int type = IsInBoundGraphNode(entity.transform.position);
                switch (type)
                {
                    case 0:
                        aiPath.destination = lastMoveDirection*PatrolRange;
                        break;
                    case 1: //left
                        lastMoveDirection.x *= -1;
                        break;
                    case 2: //right
                        lastMoveDirection.x *= -1;
                        break;
                    case 3: //bottom
                        lastMoveDirection.y *= -1;
                        break;
                    case 4: //top
                        lastMoveDirection.y *= -1;
                        break;
                }
                aiPath.destination = lastMoveDirection*PatrolRange;
                lastMoveDirection = (aiPath.destination - entity.transform.position).normalized;
            }
            Debug.Log(lastMoveDirection);
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

        private int IsInBoundGraphNode(Vector3 position)
        {
            var nodeInfo = AstarPath.active.GetNearest(position);
            var gridGraph = AstarPath.active.data.gridGraph;
            uint nodeIndex = nodeInfo.node.NodeIndex;
            uint nodeY = nodeIndex % (uint)gridGraph.width;
            uint nodeX = (nodeIndex-nodeY) / (uint)gridGraph.width;
            uint height = (uint)gridGraph.size.y;
            
            if ((nodeX <= 1 || nodeX >= gridGraph.width) || 
                (nodeY <= 1 || nodeY >= height))
            {
                if (nodeX <= 1)
                {
                    return 1; //left
                }
                if (nodeX >= gridGraph.width)
                {
                    return 2;//right
                }
                if (nodeY <= 1)
                {
                    return 3;//bottom
                }
                return 4;//top
            }
            return 0;
        }
        
        public override void OnEnd()
        {
            aiPath.canMove = false;
            animator.SetFloat("MoveSpeed",0);
            
            base.OnEnd();
        }
    }
}
