using UnityEngine;
using UnityEngine.Playables;

//有持续时间，时间到了自然解除
public class Player_受击 : State
{
    public HitFly hitFly;
    public float slowtime = 0.4f;
    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);

        Player player = (Player)entity;
        //硬直时长归零
        player.time_Angry = player.time_Stun;
        player.time_Stun = 0f;
        //停止飞行
        hitFly.FlyExit(player._rb);

        //SlowMotion.FinishSlow();
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        //值
        hitFly = GetComponent<HitFly>();
        Player player = (Player)entity;
        hitFly.sourceEntity = player.transHitFly_SourceEntity;
        hitFly.sourceAttackBox = player.transHitFly_SourceAttackBox;
        player.transHitFly_SourceEntity = null;
        player.transHitFly_SourceAttackBox = null;

        //被击飞
        hitFly.FlyStart(player._rb);
        //硬直时长初始化
        player.time_Stun = player.time_StunMax;
        SlowMotion.StartSlow(slowtime);
    }

    public override void UPStateBehaviour(Entity entity)
    {
        Player player = (Player)entity;
        player.time_Stun -= Time.deltaTime;

        hitFly.FlyBehaviour(player._rb);
    }

    public override void UPStateInit(Entity entity)
    {
    }

    //--------------------方法--------------------

    public override bool Finished(Entity entity)
    {
        return base.Finished(entity);
    }
}
