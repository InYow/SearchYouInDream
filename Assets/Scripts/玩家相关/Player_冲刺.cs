using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Player_冲刺 : State_攻击
{
    private Vector2 forward;

    public AnimationCurve animationCurve;

    public 残影Manager 残影Manager;

    public float t;

    public override void StateStart(Entity entity)
    {
        base.StateStart(entity);
        t = 0f;
        SkillManager.Dash();

        残影Manager = GameObject.Find("残影").GetComponent<残影Manager>();
        残影Manager.SetRender(true);

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

        if (forward == Vector2.zero)
        {
            forward = entity.GetFlipX();
        }

        forward = forward.normalized;
    }
    public override void UPStateBehaviour(Entity entity)
    {
        t += Time.deltaTime;
        Player player = (Player)entity;
        player._rb.velocity = animationCurve.Evaluate(t) * forward;
    }
    public override void StateExit(Entity entity)
    {
        base.StateExit(entity);
        残影Manager.SetRender(false);
    }
}
