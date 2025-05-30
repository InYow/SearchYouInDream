using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoUIList : MonoBehaviour
{
    public static EnemyInfoUIList instance;
    public Dictionary<Enemy, EnemyInfoUI> enemyInfoUIs = new Dictionary<Enemy, EnemyInfoUI>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddEnemyInfoUI(Enemy enemy)
    {
        if (enemyInfoUIs.ContainsKey(enemy))
        {
            EnemyInfoUI.SustainTime(EnemyInfoUIList.instance.enemyInfoUIs[enemy]);
            return;
        }
        //add to dictionary
        enemyInfoUIs.Add(enemy, EnemyInfoUI.InstantiateOne(enemy));
    }

    public void RemoveEnemyInfoUI(EnemyInfoUI enemyInfoUI)
    {
        if (enemyInfoUIs.ContainsKey(enemyInfoUI.enemy))
        {
            enemyInfoUIs.Remove(enemyInfoUI.enemy);
        }
    }
}
