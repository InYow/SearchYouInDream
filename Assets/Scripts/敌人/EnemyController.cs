using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public struct EnemyStateParem
{
    public EnemyType enemyType;
    public bool isInCD;
    public float distanceFromPlayer;
}

/// <summary>
/// 敌人集群管理器
/// 只管理发现玩家的敌人
/// </summary>
public class EnemyController : MonoBehaviour
{
    public static EnemyController instance => m_instance;
    public float tickInterval = 1.0f;
    public int maxAttackAllowance = 2;
    private float lastUpdateTime;
    
    private static EnemyController m_instance;
    [ShowInInspector]private Dictionary<Enemy,float> EnemiesNotInCD = new();
    [ShowInInspector]private HashSet<Enemy> EnemiesInCD= new();

    private List<KeyValuePair<Enemy,float>> AllowAttackEnemy= new();
    private HashSet<Enemy> TrashEnemiesSet = new();
    
    protected virtual void Awake()
    {
        if (m_instance != null)
        {
            Debug.LogWarning($"Has Multiple Instances Of {m_instance.GetType().Name}");
            Destroy(m_instance.gameObject);
        }
        m_instance = this;
    }

    private void Update()
    {
        if (Time.time - lastUpdateTime >= tickInterval)
        {
            UpdateEnemyState();

            SendAttackAllowance();
            
            lastUpdateTime = Time.time;
        }
    }

    private void SendAttackAllowance()
    {
        for (int i = 0;i<maxAttackAllowance;i++)
        {
            if (i<AllowAttackEnemy.Count)
            {
                AllowAttackEnemy[i].Key.AllowEnemyAttack(true);
            }
        }
    }

    /// <summary>
    /// 更新敌人与玩家的距离
    /// </summary>
    private void UpdateEnemyState()
    {
        foreach (var enemy in EnemiesNotInCD.Keys.ToList())
        {
            EnemyStateParem state = enemy.GetEnemyState();
            if (state.isInCD)
            {
                EnemiesNotInCD[enemy] = state.distanceFromPlayer;   
            }
            else
            {
                TrashEnemiesSet.Add(enemy);
            }
        }

        AllowAttackEnemy = EnemiesNotInCD.OrderBy((e)=>e.Value).ToList();
        foreach (var e in TrashEnemiesSet)
        {
            EnemiesNotInCD.Remove(e);
        }
        TrashEnemiesSet.Clear();
    }

    public void EnemyStartCD(Enemy enemy)
    {
        if (EnemiesNotInCD.ContainsKey(enemy))
        {
            EnemiesNotInCD.Remove(enemy);
        }
        EnemiesInCD.Add(enemy);
    }

    public void EnemyEndCD(Enemy enemy)
    {
        if (EnemiesInCD.Contains(enemy))
        {
            EnemiesInCD.Remove(enemy);
        }
        enemy.AllowEnemyAttack(false);
        var param = enemy.GetEnemyState();
        EnemiesNotInCD.Add(enemy, param.distanceFromPlayer);
    }
    
    /// <summary>
    /// 添加新敌人
    /// </summary>
    /// <param name="enemy"></param>
    public void RegisterEnemy(Enemy enemy)
    {
        //FIXME: Sensor中触发多次，为避免报错暂时这么写
        UnregisterEnemy(enemy);
        
        EnemyStateParem state = enemy.GetEnemyState();
        if (state.isInCD && 
            (state.enemyType == EnemyType.Combat ||
             state.enemyType == EnemyType.DashEnemy ))
        {
            EnemiesInCD.Add(enemy);
        }
        else if(!state.isInCD)
        {
            EnemiesNotInCD.Add(enemy,state.distanceFromPlayer);
        }
    }

    /// <summary>
    /// 移除敌人
    /// </summary>
    /// <param name="enemy"></param>
    public void UnregisterEnemy(Enemy enemy)
    {
        EnemiesInCD.Remove(enemy);
        EnemiesNotInCD.Remove(enemy);
    }
}
