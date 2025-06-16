using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Player_复活 : State
{
    public CameraZoneData cameraZoneData;
    public override void StateExit(Entity entity)
    {
        Player player = (Player)entity;
        Destroy(gameObject);
        SlowMotion.FinishSlow();
        //渲染排序层级恢复
        player.GetComponent<SpriteRenderer>().sortingOrder = 0;
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();


        //速度为0
        Player player = (Player)entity;
        player._rb.velocity = Vector2.zero;

        //镜头推近
        CameraZone.CameraZoneUseData(cameraZoneData);

        //渲染排序层级靠前
        player.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    public override void UPStateBehaviour(Entity entity)
    {

    }

    public override void UPStateInit(Entity entity)
    {
    }
}
