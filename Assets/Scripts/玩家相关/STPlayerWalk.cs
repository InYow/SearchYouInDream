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

        //翻转人物
        if (dic_input.x > 0f)
        {
            Vector3 v = player.gameObject.transform.localScale;
            v.x = 1f;
            player.gameObject.transform.localScale = v;
            //unscale
            v.x = 1f;
            player.transform.GetChild(0).localScale = v;
        }
        else if (dic_input.x < 0f)
        {
            Vector3 v = player.gameObject.transform.localScale;
            v.x = -1f;
            player.gameObject.transform.localScale = v;
            //unscale
            v.x = -1f;
            player.transform.GetChild(0).localScale = v;
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
