using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;


public class EnemyInfoUI : MonoBehaviour
{
    public Image image;
    public Image healthBar;
    public Image resisBar;

    public Enemy enemy;
    public float sustainTime_Max = 5f; // Time to sustain the UI before it disappears
    public float sustainTime; // Time to sustain the UI before it disappears

    public static EnemyInfoUI InstantiateOne(Enemy enemy)
    {
        //instantiate
        EnemyInfoUI enemyInfoUI = Instantiate(Resources.Load<EnemyInfoUI>("Prefabs/UICanvas/EnemyInfoUI"), EnemyInfoUIList.instance.transform.GetChild(0));
        //init values
        enemyInfoUI.enemy = enemy;
        enemyInfoUI.image.sprite = enemy.photo;
        enemyInfoUI.sustainTime = enemyInfoUI.sustainTime_Max;

        //images
        enemyInfoUI.healthBar.fillAmount = enemy.health / enemy.health_Max;
        enemyInfoUI.resisBar.fillAmount = enemy.resis / enemy.resis_Max;

        return enemyInfoUI;
    }

    public static void SustainTime(EnemyInfoUI enemyInfoUI)
    {
        enemyInfoUI.sustainTime = enemyInfoUI.sustainTime_Max; // Reset sustain time to 5 seconds 
    }

    private void Update()
    {
        sustainTime -= Time.deltaTime;
        if (sustainTime <= 0f)
        {
            EnemyInfoUIList.instance.RemoveEnemyInfoUI(this);
            Destroy(gameObject);
            return;
        }

        // Update health bar
        healthBar.fillAmount = enemy.health / enemy.health_Max;

        // Update resistance bar
        resisBar.fillAmount = enemy.resis / enemy.resis_Max;
    }
}
