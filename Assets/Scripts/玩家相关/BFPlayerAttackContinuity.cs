using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//进入防御状态，然后弹反敌人的攻击。在那之后的一段时间内（buff持续时间内），发动普攻，会衔接更高级普攻。
public class BFPlayerAttackContinuity : Buff
{
    public int attackID;            //连第几段攻击
    private bool defend_achieve;    //成功弹反敌人攻击
    public float time_Sustain;      //激活后持续时间

    public override void StartBuff(Entity entity)
    {
        base.StartBuff(entity);
        defend_achieve = false;
    }

    //-----方法------

    /// <summary>
    /// 激活 续连招功能
    /// </summary>
    public void Active()
    {
        defend_achieve = true;
        time = time_Sustain;
    }

    public bool IfActive()
    {
        return defend_achieve;
    }
}
