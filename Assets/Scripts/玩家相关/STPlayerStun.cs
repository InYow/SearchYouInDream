using System.Collections;
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.ExpressionGraph.FunctionCompilers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

//有持续时间，时间到了自然解除
public class STPlayerStun : State
{
    [ReadOnly]
    public float time_Stun;
    public float time_StunMax;

    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        //硬直时长
        time_Stun = time_StunMax;
        //速度为0
        Player player = (Player)entity;
        player._rb.velocity = Vector2.zero;
    }

    public override void UPStateBehaviour(Entity entity)
    {
        time_Stun -= Time.deltaTime;
    }

    public override void UPStateInit(Entity entity)
    {
    }

    //--------------------方法--------------------

    public float GetStunTime()
    {
        return time_Stun;
    }

    public new bool Finished()
    {
        if (time_Stun <= 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
