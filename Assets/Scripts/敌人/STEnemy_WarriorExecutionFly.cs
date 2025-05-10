using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class STEnemy_WarriorExecutionFly : State
{
    public HitFly hitFly;
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

        //值
        hitFly = GetComponent<HitFly>();
        this.enemy = (Enemy)entity;
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
    }

    public override void UPStateBehaviour(Entity entity)
    {
        //飞行
        hitFly.FlyBehaviour(enemy._rb);
    }

    public override void UPStateInit(Entity entity)
    {
    }

    //--------------------方法--------------------
    public override bool Finished(Entity entity)
    {
        return base.Finished(entity);
    }
}
