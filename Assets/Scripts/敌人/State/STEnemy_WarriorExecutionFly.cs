using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class STEnemy_WarriorExecutionFly : State
{
    public HitFly hitFly;
    public string[] hitSFXSet = Array.Empty<string>();
    
    private Enemy enemy;

    public override void StateExit(Entity entity)
    {
        //停止飞行
        hitFly.FlyExit(enemy._rb);

        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();
        if (hitSFXSet.Length>0)
        {
            PlayHitSFX();
        }
        
        //值
        hitFly = GetComponent<HitFly>();
        enemy = (Enemy)entity;
        //hitFly.sourceEntity = entity.transExecution_DamageSourceEntity;
        hitFly.sourceAttackBox = entity.transExecution_AttackBox;
        hitFly.checkBoxBehaviour = entity.transExecution_AttackBoxBehaviour;
        entity.transExecution_AttackBox = null;
        entity.transExecution_AttackBoxBehaviour = null;
        entity.transExecution = false;
        entity.transExecution_DamageSourceEntity = null;
        entity.transExecution_Type = null;

        enemy.isGetHurt = false;
        enemy.behaviourTree.SetVariableValue("bIsGetHurt", enemy.isGetHurt);

        hitFly.FlyStart(enemy._rb);

        UpdateFaceDirection(enemy._rb.velocity);
    }

    private void UpdateFaceDirection(Vector2 rbVelocity)
    {
        Debug.Log(rbVelocity);
        if (rbVelocity.x > 0)
        {
            enemy.transform.localScale = new Vector3(-1,1,1);
        }
        else if(rbVelocity.x < 0)
        {
            enemy.transform.localScale = Vector3.one;    
        }
    }

    public override void UPStateBehaviour(Entity entity)
    {
        //飞行
        hitFly.FlyBehaviour(enemy._rb);
    }

    public override void UPStateInit(Entity entity)
    {
    }

    private void PlayHitSFX()
    {
        int index = Random.Range(0,hitSFXSet.Length);
        SoundManager_New.PlayIfFinish(hitSFXSet[index]);
    }
    
    //--------------------方法--------------------
    public override bool Finished(Entity entity)
    {
        return base.Finished(entity);
    }
}
