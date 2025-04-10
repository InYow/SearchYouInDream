using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class Enemy_Warrior : Enemy
{
    
    public override void Start()
    {
        base.Start();
        
    }
    public override void Update()
    {
        base.Update();
    }

    // 帧初始化
    public override void UPInit()
    {
        base.UPInit();
    }

    // 帧输入
    public override void UPInput()
    {
        base.UPInput();
    }

    public override void UPCheckStateTrans()
    {
        base.UPCheckStateTrans();
    
        //--------任意状态------
        if (transExecution)                 //死亡处决
        {
            Debug.Log("死亡处决");
            if (transExecution_Type == "fly")  //被击飞
            {
                Debug.Log("被击飞");
                StateCurrent = InstantiateState("STEnemy_WarriorExecutionFly");
                return;
            }
        }
        if (transBreakStun)         //破防硬直
        {
            StateCurrent = InstantiateState("STEnemy_WarriorBreakStun");
            return;
        }
        //----------------------
        
        if (stateCurrentName == "STEnemy_WarriorAttack")        //攻击
        {
            if (StateCurrent.Finished(this))                    //待机
            {
                StateCurrent = InstantiateState("STEnemy_WarriorIdle");
            }
        }
        else if (stateCurrentName == "STEnemy_WarriorIdle")     //待机
        {
            if (((STEnemy_WarriorIdle)StateCurrent).Attack())   //攻击
            {
                StateCurrent = InstantiateState("STEnemy_WarriorAttack");
            }
        }
        else if (stateCurrentName == "STEnemy_WarriorBreakStun")    //破防硬直
        {
            if (StateCurrent.Finished(this))                        //待机
            {
                StateCurrent = InstantiateState("STEnemy_WarriorIdle");
            }
        }
        else if (stateCurrentName == "STEnemy_WarriorExecutionFly")     //处决击飞
        {
            if (StateCurrent.Finished(this))                            //待机
            {
                StateCurrent = InstantiateState("STEnemy_WarriorDieLayOn");
            }
        }
    
    }
}
