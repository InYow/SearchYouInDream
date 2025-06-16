using UnityEngine;


public class Player_死亡 : State
{
    public HitFly hitFly;
    public float slowtime = 0.4f;
    public float slowfactor = 0.5f;
    public float time;
    public CameraZoneData cameraZoneData;
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

        //死亡逻辑
        GameManager.Instance.GameOver();

        //值
        hitFly = GetComponent<HitFly>();
        Player player = (Player)entity;
        //hitFly.sourceEntity = player.transHitFly_SourceEntity;
        hitFly.sourceAttackBox = player.transHitFly_SourceAttackBox;
        hitFly.checkBoxBehaviour = player.transHitFly_SourceCheckBoxBehaviour;
        player.transHitFly_SourceEntity = null;
        player.transHitFly_SourceAttackBox = null;
        player.transHitFly_SourceCheckBoxBehaviour = null;
        time = slowtime;

        //被击飞
        hitFly.FlyStart(player._rb);
        //硬直时长初始化
        player.time_Stun = player.time_StunMax;
        SlowMotion.StartSlow(slowtime, slowfactor);

        // //红色闪光
        // VisualEffectManager.PlayEffect("红色闪光", entity.hitVFX_Pivot);

        //镜头推近
        CameraZone.CameraZoneUseData(cameraZoneData);
    }

    public override void UPStateBehaviour(Entity entity)
    {
        Player player = (Player)entity;
        player.time_Stun -= Time.deltaTime;
        time -= Time.unscaledDeltaTime;

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
