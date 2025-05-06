using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//进入防御状态，然后弹反敌人的攻击。在那之后的一段时间内（buff持续时间内），发动普攻，会衔接更高级普攻。
public class BFPlayerAttackContinuity_New : Buff
{
    public int attackID;            //第几段攻击

    public override void StartBuff(Entity entity)
    {
        base.StartBuff(entity);
    }

    public override void UpdateBuff(Entity entity)
    {
        base.UpdateBuff(entity);
        if (entity.stateCurrentName != "Player_冲刺")
        {
            entity.BuffRemove("BFPlayerAttackContinuity_New");
        }
    }
}
