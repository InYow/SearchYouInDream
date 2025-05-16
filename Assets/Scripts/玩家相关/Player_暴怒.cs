using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Player_暴怒 : State
{
    public CameraZoneData cameraZoneData;
    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
        SlowMotion.FinishSlow();
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        //增加一个评级
        RankSystem.UpLevelRank();

        //速度为0
        Player player = (Player)entity;
        player._rb.velocity = Vector2.zero;
        SlowMotion.StartSlow();

        //镜头推近
        CameraZone.CameraZoneUseData(cameraZoneData);
    }

    public override void UPStateBehaviour(Entity entity)
    {

    }

    public override void UPStateInit(Entity entity)
    {
    }
}
