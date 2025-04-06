using UnityEngine;
using UnityEngine.Playables;

//-处于当前状态时，播放移动动画
//-动画方向根据移动方向改变(翻转人物)
//-根据输入方向移动(rb.velocity)
public class STPlayerWalk : State
{
    public override void UPStateBehaviour(Entity entity)
    {
        Player player = (Player)entity;
        Vector2 dic_input = player.dic_Input;

        if (dic_input.x > 0f)
        {
            player.FlipX(false);
        }
        else if (dic_input.x < 0f)
        {
            player.FlipX(true);
        }

        //移动
        player._rb.velocity = player.speed_walk * dic_input;

    }

    public override void UPStateInit(Entity entity)
    {

    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();
    }

    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
    }
}
