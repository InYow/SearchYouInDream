using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//进入时播放攻击技能资产(Timeline)
//特定帧有攻击检测框,进行攻击(用Timeline的animatrack做吧)
//前面几帧是前摇，后面几帧是后摇，攻击检测框在中间
//“攻击检测框生成了”则看作“A出来了”
//后摇可以被打断，当然打断逻辑写Player里
//提供一个方法，用于获取当前帧/状态总帧数
//提供一个方法，用于获取“A出来了”是第几帧
public class STPlayerAttack01 : State
{
    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        //根据评级，改变播放的Timeline
        //FIN 将根据评级变化的逻辑简化为设置枚举值
        //FIN 根据枚举值的不同，读取不同Timeline的自定义轨道信息
        RankABCD = RankSystem.GetRankABCD();

        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        //速度为0
        Player player = (Player)entity;
        player._rb.velocity = Vector2.zero;
    }

    public override void UPStateBehaviour(Entity entity)
    {

    }
    public override void UPStateInit(Entity entity)
    {
    }
}
