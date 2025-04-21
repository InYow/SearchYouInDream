using UnityEngine;

public class HitFly : MonoBehaviour
{
    [Header("击飞参数")]
    public Entity sourceEntity;         //来源
    public CheckBox sourceAttackBox;    //来源
    public float flySpeed;              //速度
    public Vector2 flyDirection;        //方向
    public float timeMax_fly;           //时长
    public float time_fly;

    public void FlyStart(Rigidbody2D rb)
    {
        rb.velocity = flyDirection * flySpeed;

        time_fly = timeMax_fly;
        flyDirection = Vector2.zero;
        //计算飞行方向
        Vector3 scale = sourceAttackBox.transform.lossyScale;
        if (scale.x >= 0f)  //向右
        {
            flyDirection.x = 1f;
        }
        else                //向左
        {
            flyDirection.x = -1f;
        }
        //开始飞行
        {
            rb.velocity = flyDirection * flySpeed;
        }
    }

    public void FlyBehaviour()
    {

    }

    public void FlyExit(Rigidbody2D rb)
    {
        rb.velocity = Vector2.zero;
    }
}
