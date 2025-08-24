using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorTreeExtension.Sensor;
using DG.Tweening;
using Fungus;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using Pathfinding;
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
    public ExternalBehaviorTree externalBehavior;
    public AIPath aiPath;
    public Transform target;
    public SensorBase sensor;
    public bool bFoundPlayer = false;
    public bool isGetHurt = false;
    public string bloodVFXName = "踩血特效1";
    public List<string> bloodVFXNameList = new List<string> { "爆血1", "爆血2", "爆血3", "爆血4" };
    public Sprite photo; //大头照
    public bool isDead = false;

    public void OnEnable()
    {
        sensor = GetComponent<SensorBase>();
        sensor.OnTargetDetect += DetectPlayer;
        sensor.OnTargetLose += LosePlayer;

        StartCoroutine(DelayEnableBehaviourTree());
    }

    private void OnDisable()
    {
        sensor.OnTargetDetect -= DetectPlayer;
        sensor.OnTargetLose -= LosePlayer;
    }

    public override void Start()
    {
        base.Start();
    }

    public void AllowEnemyAttack(bool allow)
    {
        behaviourTree.SetVariableValue("bCanAttack", allow);
    }

    protected virtual void DetectPlayer()
    {
        EnemyController.instance.RegisterEnemy(this);
    }

    protected virtual void LosePlayer()
    {
        EnemyController.instance.UnregisterEnemy(this);
    }

    // old version
    public override void GetHurt(Entity entity, CheckBox attackBox)
    {
        //Debug.Log(entity.name);
        if (!BuffContain("BFPlayerUnselected"))
        {
            health -= entity.attackValue;
            EnemyInfoUIList.instance.AddEnemyInfoUI(this); //添加敌人信息UI

            if (transBreakStun || beingBreakStun)
            {
                isGetHurt = true;
                behaviourTree.SetVariableValue("bIsGetHurt", isGetHurt);
            }

            PlayOnHitEffect();

            //死掉了
            if (health <= 0f)
            {
                Execution(entity, attackBox);
                //behaviourTree.DisableBehavior();
            }
        }
    }

    // new version
    public override void GetHurt(Entity entity, CheckAttackBoxBehaviour checkBoxBehaviour, float attackValue)
    {
        if (!BuffContain("BFPlayerUnselected"))
        {
            if (attackValue == 0f)
            {
                Debug.Log("entity.attackValue" + entity.attackValue);
                health -= entity.attackValue;
            }
            else
            {
                Debug.Log("attackValue" + attackValue);
                health -= attackValue;
            }
            EnemyInfoUIList.instance.AddEnemyInfoUI(this); //添加敌人信息UI

            if (transBreakStun || beingBreakStun)
            {
                isGetHurt = true;
                behaviourTree.SetVariableValue("bIsGetHurt", isGetHurt);
            }

            PlayOnHitEffect();

            //死掉了
            if (health <= 0f)
            {
                Execution(entity, checkBoxBehaviour);
                //behaviourTree.DisableBehavior();
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

    private IEnumerator DelayEnableBehaviourTree()
    {
        behaviourTree.DisableBehavior();
        behaviourTree.ExternalBehavior = externalBehavior;
        yield return new WaitFrames();
        behaviourTree.EnableBehavior();

        behaviourTree.SetVariableValue("bIsDead", isDead);
    }

    protected void PlayOnHitEffect()
    {
        //播放受伤特效
        GetHurtVFX();
        // SoundEffectManager.PlaySFX01(transform);
        SoundManager_New.PlayOneshot("拳");

        if (!isGetHurt)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.SetUpdate(true);

            sequence.AppendCallback
            (() =>
                {
                    GetComponent<SpriteRenderer>().material.SetFloat("_PureColor", 1f);
                    GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
                }
            );
            //pure back 
            sequence.AppendInterval(0.2f); //interval
            sequence.AppendCallback
            (() =>
                {
                    GetComponent<SpriteRenderer>().material.SetFloat("_PureColor", 0f);
                    GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.black);
                }
            );
        }
    }

}
