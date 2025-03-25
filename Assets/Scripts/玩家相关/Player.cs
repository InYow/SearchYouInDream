using UnityEngine;

public class Player : Entity
{
    public float healthGray;
    public Rigidbody2D _rb;
    public float speed_walk;

    [Header("状态机全局变量")]
    public Vector2 dic_Input;
    public bool getHurt;

    public override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void Update()
    {
        base.Update();
    }

    // 帧初始化
    public override void UPInit()
    {
        dic_Input = Vector2.zero;
        base.UPInit();
    }

    // 帧输入
    public override void UPInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            dic_Input += Vector2.left;
        }
        if (Input.GetKey(KeyCode.W))
        {
            dic_Input += Vector2.up;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dic_Input += Vector2.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dic_Input += Vector2.down;
        }
        dic_Input = dic_Input.normalized;

        base.UPInput();
    }

    public override void UPCheckStateTrans()
    {
        base.UPCheckStateTrans();

        //任意状态
        if (getHurt)
        {
            getHurt = false;
            StateCurrent = InstantiateState("STPlayerStun");
            return;
        }

        //待机
        if (stateCurrentName == "STPlayerIdle")
        {
            if (Input.GetKeyDown(KeyCode.J))    //攻击01
            {
                StateCurrent = InstantiateState("STPlayerAttack01");
            }
            else if (dic_Input != Vector2.zero) //移动
            {
                StateCurrent = InstantiateState("STPlayerWalk");
            }
        }
        //移动
        else if (stateCurrentName == "STPlayerWalk")
        {
            if (Input.GetKeyDown(KeyCode.J))    //攻击01
            {
                StateCurrent = InstantiateState("STPlayerAttack01");
            }
            else if (dic_Input == Vector2.zero)      //待机
            {
                StateCurrent = InstantiateState("STPlayerIdle");
            }
        }
        //攻击01
        else if (stateCurrentName == "STPlayerAttack01")
        {
            if (StateCurrent.Finished())            //待机 or 移动
            {
                if (dic_Input == Vector2.zero)      //待机
                {
                    StateCurrent = InstantiateState("STPlayerIdle");
                }
                else if (dic_Input != Vector2.zero) //移动
                {
                    StateCurrent = InstantiateState("STPlayerWalk");
                }
            }
            else if (Input.GetKeyDown(KeyCode.J))    //攻击02
            {
                StateCurrent = InstantiateState("STPlayerAttack02");
            }
        }
        //攻击02
        else if (stateCurrentName == "STPlayerAttack02")
        {
            if (StateCurrent.Finished())            //待机 or 移动
            {
                if (dic_Input == Vector2.zero)      //待机
                {
                    StateCurrent = InstantiateState("STPlayerIdle");
                }
                else if (dic_Input != Vector2.zero) //移动
                {
                    StateCurrent = InstantiateState("STPlayerWalk");
                }
            }
            else if (Input.GetKeyDown(KeyCode.J))    //攻击03
            {
                StateCurrent = InstantiateState("STPlayerAttack03");
            }
        }
        //攻击03
        else if (stateCurrentName == "STPlayerAttack03")
        {
            if (StateCurrent.Finished())            //待机 or 移动
            {
                if (dic_Input == Vector2.zero)      //待机
                {
                    StateCurrent = InstantiateState("STPlayerIdle");
                }
                else if (dic_Input != Vector2.zero) //移动
                {
                    StateCurrent = InstantiateState("STPlayerWalk");
                }
            }
            else if (Input.GetKeyDown(KeyCode.J))    //攻击04
            {
                StateCurrent = InstantiateState("STPlayerAttack04");
            }
        }
        //攻击04
        else if (stateCurrentName == "STPlayerAttack04")
        {
            if (StateCurrent.Finished())            //待机 or 移动
            {
                if (dic_Input == Vector2.zero)      //待机
                {
                    StateCurrent = InstantiateState("STPlayerIdle");
                }
                else if (dic_Input != Vector2.zero) //移动
                {
                    StateCurrent = InstantiateState("STPlayerWalk");
                }
            }
            else if (Input.GetKeyDown(KeyCode.J))    //攻击01
            {
                StateCurrent = InstantiateState("STPlayerAttack01");
            }
        }
        //受击硬直
        else if (stateCurrentName == "STPlayerStun")
        {
            if (StateCurrent.Finished())             //待机 or 移动
            {
                if (dic_Input == Vector2.zero)      //待机
                {
                    StateCurrent = InstantiateState("STPlayerIdle");
                }
                else if (dic_Input != Vector2.zero) //移动
                {
                    StateCurrent = InstantiateState("STPlayerWalk");
                }
            }
            else if (Input.GetKeyDown(KeyCode.K))    //暴怒
            {
                StateCurrent = InstantiateState("STPlayerAngry");
            }
        }
        //暴怒
        else if (stateCurrentName == "STPlayerAngry")
        {
            if (Input.GetKeyDown(KeyCode.J))    //攻击01
            {
                StateCurrent = InstantiateState("STPlayerAttack01");
            }
            else if (dic_Input != Vector2.zero) //移动
            {
                StateCurrent = InstantiateState("STPlayerWalk");
            }
            else if (dic_Input == Vector2.zero && StateCurrent.Finished())      //待机
            {
                StateCurrent = InstantiateState("STPlayerIdle");
            }
        }
    }

    //-----------------方法-----------------
    public override void Hurt(Entity entity)
    {
        base.Hurt(entity);

        //灰色生命治疗
        float h = healthGray - health;
        if (h > 0f)
        {
            health = Mathf.Clamp(health + attackValue, health, healthGray);
        }
    }

    public override void GetHurt(Entity entity)
    {
        base.GetHurt(entity);//扣health
        getHurt = true;
    }
}
