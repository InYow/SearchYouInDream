using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFPlayerGuideBreakAttack : Buff
{
    public Entity e_target;
    public override void StartBuff(Entity entity)
    {
        base.StartBuff(entity);

        //慢动作、近镜头
        SlowMotion.StartSlow();
    }
    public override void UpdateBuff(Entity entity)
    {
        //流逝时间
        if (time <= 0f)
        {
            FinishBuff(entity);
        }
        else
        {
            time = Mathf.Clamp(time - Time.unscaledDeltaTime, 0f, time);
        }
    }

    public override void FinishBuff(Entity entity)
    {
        base.FinishBuff(entity);
        e_target = null;


        //关闭慢动作、近镜头
        SlowMotion.FinishSlow();
    }
}
