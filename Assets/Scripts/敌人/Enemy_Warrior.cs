using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Warrior : Entity
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
        if (stateCurrentName == "STEnemy_WarriorAttack")    //攻击
        {
            if (StateCurrent.Finished())                    //待机
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
        else if (stateCurrentName == "STEnemy_WarriorStun")
        {

        }

    }
}
