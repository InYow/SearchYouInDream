using UnityEngine;

public enum HitFlyType
{
    Float,
    Curve,
}

public class HitFly : MonoBehaviour
{
    [Header("击飞参数")]
    public HitFlyType hitFlyType;       //击飞类型
    [HideInInspector]
    public CheckBox sourceAttackBox;    //来源
    [HideInInspector]
    public CheckBoxBehaviour checkBoxBehaviour; //来源
    [HideInInspector]
    public float flySpeed;              //速度
    [HideInInspector]
    public AnimationCurve flyCurve;     //速度曲线
    private Vector2 flyDirection;       //方向
    public float timeMax_fly;           //时长
    private float time_fly;

    public void FlyStart(Rigidbody2D rb)
    {
        time_fly = timeMax_fly;
        flyDirection = Vector2.zero;
        //飞行方向
        if (sourceAttackBox != null)
        {
            Vector3 scale = sourceAttackBox.transform.lossyScale;
            if (scale.x >= 0f)  //向右
            {
                flyDirection.x = 1f;
            }
            else                //向左
            {
                flyDirection.x = -1f;
            }
        }
        else if (checkBoxBehaviour != null)
        {
            Vector3 scale = checkBoxBehaviour.entity_master.transform.lossyScale;
            if (scale.x >= 0f)  //向右
            {
                flyDirection.x = 1f;
            }
            else                //向左
            {
                flyDirection.x = -1f;
            }
        }
        //开始飞行
        if (hitFlyType == HitFlyType.Float)
        {
            rb.velocity = flyDirection * flySpeed;
        }
        else if (hitFlyType == HitFlyType.Curve)
        {
            rb.velocity = flyDirection * flyCurve.Evaluate(timeMax_fly - time_fly);
        }
    }

    public void FlyBehaviour(Rigidbody2D rb)
    {
        if (time_fly > 0f)
        {
            if (hitFlyType == HitFlyType.Float)
            {
                rb.velocity = flyDirection * flySpeed;
            }
            else if (hitFlyType == HitFlyType.Curve)
            {
                rb.velocity = flyDirection * flyCurve.Evaluate(timeMax_fly - time_fly);
            }
            //流逝 飞行时间
            time_fly -= Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void FlyExit(Rigidbody2D rb)
    {
        rb.velocity = Vector2.zero;
    }
}
