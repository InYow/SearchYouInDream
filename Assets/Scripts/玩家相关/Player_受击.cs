using UnityEngine;
using UnityEngine.Playables;

//有持续时间，时间到了自然解除
public class Player_受击 : State
{
    public HitFly hitFly;
    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);

        Player player = (Player)entity;
        //硬直时长归零
        player.time_Angry = player.time_Stun;
        player.time_Stun = 0f;
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        hitFly = GetComponent<HitFly>();

        //速度被击飞
        Player player = (Player)entity;
        hitFly.FlyStart(player._rb);
        //硬直时长初始化
        player.time_Stun = player.time_StunMax;
    }

    public override void UPStateBehaviour(Entity entity)
    {
        Player player = (Player)entity;
        player.time_Stun -= Time.deltaTime;

    }

    public override void UPStateInit(Entity entity)
    {
    }

    //--------------------方法--------------------

    public override bool Finished(Entity entity)
    {
        return base.Finished(entity);
        // Player player = (Player)entity;
        // if (player.time_Stun <= 0f)
        // {
        //     return true;
        // }
        // else
        // {
        //     return false;
        // }
    }
}
