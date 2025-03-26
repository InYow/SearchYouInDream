using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFPlayerSkillInputSlowMotion : Buff
{
    public override void StartBuff(Entity entity)
    {
        base.StartBuff(entity);

        //慢动作
        SlowMotion.StartSlow(9999f);

        //释放技能指示UI
        SkillChooseUI.Open();
    }

    public override void FinishBuff(Entity entity)
    {
        base.FinishBuff(entity);

        //关闭慢动作
        SlowMotion.FinishSlow();
        //关闭释放技能指示UI
        SkillChooseUI.Close();
    }
}

