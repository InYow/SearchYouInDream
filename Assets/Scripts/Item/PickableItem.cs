using UnityEngine;

public class PickableItem : ProjectileBase
{
    public Animator _animator;
    public GameObject attack_throwitemGO;

    /// <summary>
    /// 被entity拾起
    /// </summary>
    /// <param name="entity"></param>
    public void Picked(Entity entity)
    {
        transform.Find("Render阴影").gameObject.SetActive(false); //关闭阴影 
        //手上没有东西
        Player player = (Player)entity;
        if (player.pickableItem == null)
        {
            transform.SetParent(player.pickableItem_Pivot, true);    //设置父对象
            transform.localPosition = Vector3.zero;         //设置位置
            transform.localRotation = Quaternion.identity;  //设置旋转
            _rb.simulated = false;                          //关闭物理模拟
            _animator.Play("idle");                         //播放动画
            player.pickableItem = this;                     //设置引用
            entity_master = player;                         //entity_master
        }
    }

    /// <summary>
    /// 被entity扔出
    /// </summary>
    /// <param name="entity"></param>
    public void Throw(Entity entity)
    {
        Player player = (Player)entity;
        entity_master = player;         //entity_master
        transform.SetParent(null);      //解除父对象 
        _rb.simulated = true;           //开启物理模拟
        GetComponent<BoxCollider2D>().enabled = false; //禁用碰撞器

        //计算投掷方向
        Vector3 scale = player.transform.lossyScale;
        Vector2 forward_throw = Vector2.zero;
        if (scale.x >= 0f)  //向右
        {
            forward_throw.x = 1f;
        }
        else                //向左
        {
            forward_throw.x = -1f;
        }

        //设置速度
        _rb.velocity = forward_throw * player.speed_throw;

        //开启攻击_throwitem检测框
        attack_throwitemGO.SetActive(true);

        player.pickableItem = null;     //清除引用
    }

    /// <summary>
    /// 飞行攻击停止
    /// </summary>
    /// <param name="entity">扔出的entity</param>
    public override void Stop()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0); //无旋转
        _animator.Play("stop");         //播放动画
        _rb.velocity = Vector2.zero;    //速度为0
    }
}
