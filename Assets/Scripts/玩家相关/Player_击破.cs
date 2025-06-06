using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;


public class Player_击破 : State_攻击
{
    public CameraZoneData cameraZoneData;
    public Vector2 offset;

    public override void StateExit(Entity entity)
    {
        base.StateExit(entity);
        SlowMotion.FinishSlow();
        entity.BuffRemove("BFPlayerGuideBreakAttack");
    }

    public override void StateStart(Entity entity)
    {
        base.StateStart(entity);
        //推近镜头
        CameraZone.CameraZoneUseData(cameraZoneData);

        //调整位置点
        Player player = entity as Player;
        Vector2 pos = (Vector2)player.eTarget_击破.transform.position + offset * player.GetFlipX().x;
        Debug.Log("击破位置: " + pos);
        //player._rb.MovePosition(pos);
        player._rb.position = pos;
    }

    public override void UPStateBehaviour(Entity entity)
    {
        base.UPStateBehaviour(entity);
    }
    public override void UPStateInit(Entity entity)
    {
        base.UPStateInit(entity);
    }
}
