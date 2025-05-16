using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Pathfinding;
using UnityEngine;
using UnityEngine.Playables;

public class STEnemy_BoomerExplosion : State
{
    public float ExplosionRange;
    public PlayableAsset boomerExplosionAsset;
    
    private Enemy enemy;
    private AIPath aiPath;
    private Animator animator;
    private Transform targetTransform;
    private bool isExploing = false;
    
    public override void StateStart(Entity entity)
    {
        enemy = entity as Enemy;
        aiPath = enemy.aiPath;
        animator = enemy.GetComponent<Animator>();;
            
        var behaviourTree = enemy.behaviourTree;
        var target = behaviourTree.GetVariable("PlayerTransform") as SharedTransform;
        targetTransform = target.Value;
        
        aiPath.destination = targetTransform.position;
        aiPath.canMove = true;
        
        animator.SetFloat("MoveSpeed", aiPath.maxSpeed);
    }

    public override void UPStateInit(Entity entity)
    {
        
    }

    public override void UPStateBehaviour(Entity entity)
    {
        Vector3 dir = (aiPath.destination - entity.transform.position);
        if (dir.magnitude <= ExplosionRange)
        {
            if (!isExploing)
            {
                aiPath.canMove = false;
                animator.SetFloat("MoveSpeed", 0.0f); 
                
                playableDirector.playableAsset = boomerExplosionAsset;
                BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
                playableDirector.Play();
                isExploing = true;
            }
            
        }
        else
        {
            animator.SetFloat("MoveSpeed", aiPath.maxSpeed);
            dir = dir.normalized;
            if (dir.x < 0)
            {
                entity.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (dir.x > 0)
            {
                entity.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public override bool Finished(Entity entity)
    {
        if (!isExploing)
        {
            return false;
        }
        return base.Finished(entity);
    }

    public override void StateExit(Entity entity)
    {
        Destroy(entity.gameObject);
    }
}
