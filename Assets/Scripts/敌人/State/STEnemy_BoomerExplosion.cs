using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Pathfinding;
using UnityEngine;
using UnityEngine.Playables;

public class STEnemy_BoomerExplosion : State
{
    public float ExplosionRange;
    public float RunRange;
    public PlayableAsset boomerExplosionAsset;
    
    private Enemy enemy;
    private AIPath aiPath;
    private Animator animator;
    private Transform targetTransform;
    private bool isExploing = false;
    private float updateTargetTime; 
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
        updateTargetTime = Time.time;
    }

    public override void UPStateInit(Entity entity)
    {
        if (Time.time-updateTargetTime > 0.8f)
        {
            updateTargetTime = Time.time;
            aiPath.destination = targetTransform.position;
        }
    }

    public override void UPStateBehaviour(Entity entity)
    {
        Vector3 dir = (aiPath.destination - entity.transform.position);
        if (dir.magnitude <= ExplosionRange)
        {
            Vector3 playerDir = (targetTransform.position - entity.transform.position);
            if (playerDir.magnitude > ExplosionRange && !isExploing)
            {
                aiPath.destination = targetTransform.position;
                aiPath.canMove = true;
                animator.SetFloat("MoveSpeed", aiPath.maxSpeed);
                if (aiPath.desiredVelocity.x < 0)
                {
                    entity.transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (aiPath.desiredVelocity.x > 0)
                {
                    entity.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else if (!isExploing)
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
            if (aiPath.desiredVelocity.x < 0)
            {
                entity.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (aiPath.desiredVelocity.x > 0)
            {
                entity.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public override bool Finished(Entity entity)
    {
        if (!isExploing)
        {
            Vector3 dir = (aiPath.destination - entity.transform.position);
            if (dir.magnitude > RunRange)
            {
                return true;
            }
            return false;
        }
        return base.Finished(entity);
    }

    public override void StateExit(Entity entity)
    {
        if (isExploing)
        {
            Destroy(entity.gameObject);    
        }
    }
}
