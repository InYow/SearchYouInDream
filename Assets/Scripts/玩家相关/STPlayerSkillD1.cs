using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//----------烈焰冲锋---------
//向前冲锋
//伤害碰到的敌人
//期间无敌
//任意状态下发动
public class STPlayerSkillD1 : State
{
    public float speed;
    public Vector2 forward;
    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
        forward = Vector2.zero;

        Player player = (Player)entity;
        //结束无法选中
        player.BuffRemove("BFPlayerUnselected");
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        Player player = (Player)entity;

        //求出冲锋方向
        //REVIEW 如果释放技能想换个方向怎么办？
        //给人的感觉是——AWSD选择释放对应的技能？；还是AWSD带有面朝方向的感觉？。
        forward = Vector2.zero;
        // if (player.dic_Input.x != 0f)   //有输入
        // {
        //     forward.x = Mathf.Sign(player.dic_Input.x);
        //     forward = forward.normalized;
        // }
        // else                            //无输入
        // {
        forward.x = player.transform.lossyScale.x;
        forward = forward.normalized;
        // }

        //翻转
        if (forward.x > 0f)
        {
            player.FlipX(false);
        }
        else if (forward.x < 0f)
        {
            player.FlipX(true);
        }

        //设置速度
        player._rb.velocity = forward * speed;

        //无法选中
        player.BuffAdd("BFPlayerUnselected");
    }


    public override void UPStateBehaviour(Entity entity)
    {
        Player player = (Player)entity;
        player._rb.velocity = forward * speed;

    }

    public override void UPStateInit(Entity entity)
    {
    }
}
