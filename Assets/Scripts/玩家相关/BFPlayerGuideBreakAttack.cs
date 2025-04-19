using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFPlayerGuideBreakAttack : Buff
{
    public GameObject guideGO;  //引导UI
    public Entity e_target;     //破防的敌人
    public override void StartBuff(Entity entity)
    {
        base.StartBuff(entity);
        //值
        Player player = entity as Player;
        e_target = player.eTarget_击破;

        //TODO 慢动作、近镜头、UI提示
        SlowMotion.StartSlow();
        //UI
        guideGO.transform.SetParent(null);      //无视scale
        guideGO.transform.localScale = Vector3.one;
        guideGO.SetActive(true);
        guideGO.transform.position = e_target.transform.position;
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


        //关闭慢动作、近镜头、UI
        SlowMotion.FinishSlow();
        //UI
        guideGO.SetActive(false);
        guideGO.transform.localPosition = Vector3.zero;
    }
}
