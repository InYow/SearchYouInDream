using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Pathfinding;
using UnityEngine;

public class STEnemy_WarriorMoveToPlayer : State
{
    private AIPath aiPath;
    private BehaviorTree behaviorTree;
    public override void StateStart(Entity entity)
    {
        Enemy enemy = entity as Enemy;
        if (enemy != null)
        {
            behaviorTree = enemy.behaviourTree;
            aiPath = enemy.aiPath;
            
            aiPath.canMove = true;
        }
        
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();
    }

    public override void UPStateInit(Entity entity) { }

    public override void UPStateBehaviour(Entity entity)
    {
        Vector3 dir = Vector3.Normalize(aiPath.destination - entity.transform.position);
        if (dir.x < 0)
        {
            entity.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(dir.x > 0)
        {
            entity.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public override void StateExit(Entity entity)
    {
        aiPath.canMove = false;
        Destroy(this.gameObject);
    }
}
