using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Player_冲刺 : State_攻击
{
    private Vector2 forward;
    public override void StateStart(Entity entity)
    {
        base.StateStart(entity);

        forward = entity.GetFlipX();
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            forward = entity.GetFlipX();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            forward = Vector2.left;
            entity.FlipX(true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            forward = Vector2.right;
            entity.FlipX(false);
        }
        else
        {
            forward = Vector2.zero;
        }

        if (Input.GetKey(KeyCode.W))
        {
            forward += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            forward += Vector2.down;
        }

        forward = forward.normalized;
    }
    public override void UPStateBehaviour(Entity entity)
    {
        Player player = (Player)entity;
        player._rb.velocity = player.speed_fly * forward;
    }
}
