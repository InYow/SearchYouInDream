using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class STEnemy_WarriorExecutionFly : State
{
    public Entity damageSourceEntity;
    public CheckBox damageSourceAttackBox;
    public float speed_fly;
    public float time_fly;
    public float timeMax_fly;
    public Vector2 forward_fly;

    private Enemy enemy;
    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
        time_fly = 0f;
        //停止飞行
        enemy = (Enemy)entity;
        enemy._rb.velocity = Vector2.zero;
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        //值
        damageSourceEntity = entity.transExecution_DamageSourceEntity;
        damageSourceAttackBox = entity.transExecution_AttackBox;
        entity.transExecution = false;
        enemy.behaviourTree.SetVariableValue("bCanExecute",entity.transExecution);
        entity.transExecution_AttackBox = null;
        entity.transExecution_DamageSourceEntity = null;
        entity.transExecution_Type = null;
        time_fly = timeMax_fly;
        forward_fly = Vector2.zero;
        //计算飞行方向
        Vector3 scale = damageSourceAttackBox.transform.lossyScale;
        if (scale.x >= 0f)  //向右
        {
            forward_fly.x = 1f;
        }
        else                //向左
        {
            forward_fly.x = -1f;
        }
        //开始飞行
        {
            Enemy enemy = (Enemy)entity;
            enemy._rb.velocity = forward_fly * speed_fly;
        }

    }

    public override void UPStateBehaviour(Entity entity)
    {
        //飞行
        Enemy enemy = (Enemy)entity;
        enemy._rb.velocity = forward_fly * speed_fly;
        //流逝 飞行时间
        time_fly -= Time.deltaTime;
    }

    public override void UPStateInit(Entity entity)
    {
    }

    //--------------------方法--------------------
    public override bool Finished(Entity entity)
    {
        if (time_fly <= 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
