using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorTreeExtension.Sensor;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyType
{
    Shooter,
    DashEnemy,
    Combat,
    Boomer,
    Boss
}

public class Enemy : Entity
{
    public EnemyType enemyType;
    public BehaviorTree behaviourTree;
    public AIPath aiPath;
    public Transform target;
    public SensorBase sensor;
    public bool bFoundPlayer = false;
    public bool isGetHurt = false;
    public string bloodVFXName = "踩血特效1";

    public void OnEnable()
    {
        sensor = GetComponent<SensorBase>();
        sensor.OnTargetDetect += DetectPlayer;
        sensor.OnTargetLose += LosePlayer;
    }

    private void OnDisable()
    {
        sensor.OnTargetDetect -= DetectPlayer;
        sensor.OnTargetLose -= LosePlayer;
    }

    public void AllowEnemyAttack(bool allow)
    {
        behaviourTree.SetVariableValue("bCanAttack", allow);
    }

    protected virtual void DetectPlayer()
    {
        Debug.Log("FindPlayer");
        EnemyController.instance.RegisterEnemy(this);
    }

    protected virtual void LosePlayer()
    {
        Debug.Log("LosePlayer");
        EnemyController.instance.UnregisterEnemy(this);
    }

    public override void GetHurt(Entity entity, CheckBox attackBox)
    {
        Debug.Log(entity.name);
        if (!BuffContain("BFPlayerUnselected"))
        {
            health -= entity.attackValue;

            GetHurtVFX();
            SoundEffectManager.PlaySFX01(transform);

            if (transBreakStun || beingBreakStun)
            {
                isGetHurt = true;
                behaviourTree.SetVariableValue("bIsGetHurt", isGetHurt);
            }

            //死掉了
            if (health <= 0f)
            {
                Execution(entity, attackBox);
            }
        }
    }

    public override void GetHurt(Entity entity, CheckAttackBoxBehaviour checkBoxBehaviour)
    {
        if (!BuffContain("BFPlayerUnselected"))
        {
            health -= entity.attackValue;

            GetHurtVFX();
            SoundEffectManager.PlaySFX01(transform);

            if (transBreakStun || beingBreakStun)
            {
                isGetHurt = true;
                behaviourTree.SetVariableValue("bIsGetHurt", isGetHurt);
            }

            //死掉了
            if (health <= 0f)
            {
                Execution(entity, checkBoxBehaviour);
            }
        }
    }

    public EnemyStateParem GetEnemyState()
    {
        EnemyStateParem stateParam;
        var inCD = behaviourTree.GetVariable("bInCD") as SharedBool;
        stateParam.isInCD = inCD.Value;
        var disFromPlayer = behaviourTree.GetVariable("DistanceFromPlayer") as SharedFloat;
        stateParam.distanceFromPlayer = disFromPlayer.Value;
        stateParam.enemyType = enemyType;
        
        return stateParam;
    }
}
