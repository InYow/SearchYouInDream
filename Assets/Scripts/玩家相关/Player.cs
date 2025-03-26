using Sirenix.OdinInspector;
using UnityEngine;

public class Player : Entity
{
    public float healthGray;
    public float healthGray_LoseSpeed;
    public Rigidbody2D _rb;
    public float speed_walk;
    public float attackResisValue;

    [Header("状态机全局变量")]
    public Vector2 dic_Input;   //四键方向
    public bool transStun;      //进入stun布尔值
    public bool transDefend_Achieve;//进入 弹反 布尔值
    public Entity transDefend_achieve_TgtEntity;//弹反目标
    [ReadOnly] public float time_Stun;  //硬直时长
    public float time_Angry;            //解除的硬直时长
    public float time_StunMax;
    public float cd_DefendOrAngry;      //格挡or暴怒CD
    public float cdMax_DefendOrAngry;
    public Entity eTarget_BreakAttack;  //引导击破攻击的目标

    public override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
        MessageManager.ACBreakStun += GuideBreakAttack;
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

        //进入技能输入时间
        if (Input.GetKeyDown(KeyCode.I) && !BuffContain("BFPlayerGuideBreakAttack") && stateCurrentName != "STPlayerStun")
        {
            BuffAdd("BFPlayerSkillInputSlowMotion");
        }
        //技能输入时间期间
        if (BuffContain("BFPlayerSkillInputSlowMotion"))
        {
            if (Input.GetKey(KeyCode.A))
            {
                SkillChooseUI.ChooseSkill("A");
            }
            if (Input.GetKey(KeyCode.W))
            {
                SkillChooseUI.ChooseSkill("W");
            }
            if (Input.GetKey(KeyCode.S))
            {
                SkillChooseUI.ChooseSkill("S");
            }
            if (Input.GetKey(KeyCode.D))
            {
                SkillChooseUI.ChooseSkill("D");
            }
        }

        base.UPInput();
    }

    public override void UPCheckStateTrans()
    {
        base.UPCheckStateTrans();

        //-----------任意状态-----------
        //硬直
        if (transStun)
        {
            transStun = false;
            StateCurrent = InstantiateState("STPlayerStun");
            return;
        }

        //技能释放
        if (BuffContain("BFPlayerSkillInputSlowMotion") && Input.GetKeyUp(KeyCode.I))
        {
            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("释放终结技");
                //StateCurrent = InstantiateState(SkillManager.GetSkillName(1));
            }
            if (Input.GetKey(KeyCode.W))
            {
                StateCurrent = InstantiateState(SkillManager.GetSkillName(2));
            }
            if (Input.GetKey(KeyCode.S))
            {
                StateCurrent = InstantiateState(SkillManager.GetSkillName(3));
            }
            if (Input.GetKey(KeyCode.D))
            {
                StateCurrent = InstantiateState(SkillManager.GetSkillName(4));
            }
            BuffRemove("BFPlayerSkillInputSlowMotion");
            return;
        }

        //拥有GuideBreakAttack的buff        //击破攻击
        if (BuffContain("BFPlayerGuideBreakAttack") && Input.GetKeyDown(KeyCode.U))
        {
            StateCurrent = InstantiateState("STPlayerBreakAttack");
            BuffRemove("BFPlayerGuideBreakAttack");
            return;
        }
        //-----------------------------

        //待机
        if (stateCurrentName == "STPlayerIdle")
        {
            if (Input.GetKeyDown(KeyCode.J))        //攻击
            {
                BFPlayerAttackContinuity buff_atk_continuity = BuffGet("BFPlayerAttackContinuity") as BFPlayerAttackContinuity;
                BFPlayerAngryEnhanceAttack01 buff_atk01_enhance = BuffGet("BFPlayerAngryEnhanceAttack01") as BFPlayerAngryEnhanceAttack01;
                if ((buff_atk_continuity == null || (buff_atk_continuity.IfActive() && buff_atk_continuity.attackID == 4)) && buff_atk01_enhance == null)    //攻击01
                {
                    StateCurrent = InstantiateState("STPlayerAttack01");
                }
                else if (buff_atk01_enhance != null)          //强化攻击01
                {
                    StateCurrent = InstantiateState("STPlayerAttack01_Enhance");
                    BuffRemove("BFPlayerAngryEnhanceAttack01");
                }
                //连携普攻
                else if (buff_atk_continuity.IfActive() && buff_atk_continuity.attackID == 1)                 //攻击02
                {
                    StateCurrent = InstantiateState("STPlayerAttack02");
                }
                else if (buff_atk_continuity.IfActive() && buff_atk_continuity.attackID == 2)                 //攻击03
                {
                    StateCurrent = InstantiateState("STPlayerAttack03");
                }
                else if (buff_atk_continuity.IfActive() && buff_atk_continuity.attackID == 3)                 //攻击04
                {
                    StateCurrent = InstantiateState("STPlayerAttack04");
                }
            }
            else if (dic_Input != Vector2.zero) //移动
            {
                StateCurrent = InstantiateState("STPlayerWalk");
            }
            else if (Input.GetKeyDown(KeyCode.K) && cd_DefendOrAngry <= 0f)//格挡
            {
                StateCurrent = InstantiateState("STPlayerDefend");
            }
        }
        //ST移动
        else if (stateCurrentName == "STPlayerWalk")
        {
            if (Input.GetKeyDown(KeyCode.J))        //攻击
            {
                BFPlayerAttackContinuity buff_atk_continuity = BuffGet("BFPlayerAttackContinuity") as BFPlayerAttackContinuity;
                BFPlayerAngryEnhanceAttack01 buff_atk01_enhance = BuffGet("BFPlayerAngryEnhanceAttack01") as BFPlayerAngryEnhanceAttack01;
                if ((buff_atk_continuity == null || (buff_atk_continuity.IfActive() && buff_atk_continuity.attackID == 4)) && buff_atk01_enhance == null)    //攻击01
                {
                    StateCurrent = InstantiateState("STPlayerAttack01");
                }
                else if (buff_atk01_enhance != null)          //强化攻击01
                {
                    StateCurrent = InstantiateState("STPlayerAttack01_Enhance");
                    BuffRemove("BFPlayerAngryEnhanceAttack01");
                }
                //连携普攻
                else if (buff_atk_continuity.IfActive() && buff_atk_continuity.attackID == 1)                 //攻击02
                {
                    StateCurrent = InstantiateState("STPlayerAttack02");
                }
                else if (buff_atk_continuity.IfActive() && buff_atk_continuity.attackID == 2)                 //攻击03
                {
                    StateCurrent = InstantiateState("STPlayerAttack03");
                }
                else if (buff_atk_continuity.IfActive() && buff_atk_continuity.attackID == 3)                 //攻击04
                {
                    StateCurrent = InstantiateState("STPlayerAttack04");
                }
            }
            else if (Input.GetKeyDown(KeyCode.K) && cd_DefendOrAngry <= 0f)//格挡
            {
                StateCurrent = InstantiateState("STPlayerDefend");
            }
            else if (dic_Input == Vector2.zero)      //待机
            {
                StateCurrent = InstantiateState("STPlayerIdle");
            }
            else if (Input.GetKeyDown(KeyCode.K) && cd_DefendOrAngry <= 0f)    //格挡
            {
                StateCurrent = InstantiateState("STPlayerDefend");
            }
        }
        //攻击01
        else if (stateCurrentName == "STPlayerAttack01")
        {
            if (StateCurrent.Finished(this))            //待机 or 移动
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
            else if (Input.GetKeyDown(KeyCode.K) && cd_DefendOrAngry <= 0f)   //格挡
            {
                StateCurrent = InstantiateState("STPlayerDefend");
                //格挡不打断普攻连携
                BFPlayerAttackContinuity buff = BuffAdd("BFPlayerAttackContinuity") as BFPlayerAttackContinuity;
                buff.attackID = 1;
            }
        }
        //ST强化攻击01
        else if (stateCurrentName == "STPlayerAttack01_Enhance")
        {
            if (StateCurrent.Finished(this))            //待机 or 移动
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
            else if (Input.GetKeyDown(KeyCode.K) && cd_DefendOrAngry <= 0f)   //格挡
            {
                StateCurrent = InstantiateState("STPlayerDefend");
                //格挡不打断普攻连携
                BFPlayerAttackContinuity buff = BuffAdd("BFPlayerAttackContinuity") as BFPlayerAttackContinuity;
                buff.attackID = 1;
            }
        }
        //攻击02
        else if (stateCurrentName == "STPlayerAttack02")
        {
            if (StateCurrent.Finished(this))            //待机 or 移动
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
            else if (Input.GetKeyDown(KeyCode.K) && cd_DefendOrAngry <= 0f)   //格挡
            {
                StateCurrent = InstantiateState("STPlayerDefend");
                //格挡不打断普攻连携
                BFPlayerAttackContinuity buff = BuffAdd("BFPlayerAttackContinuity") as BFPlayerAttackContinuity;
                buff.attackID = 2;
            }
        }
        //攻击03
        else if (stateCurrentName == "STPlayerAttack03")
        {
            if (StateCurrent.Finished(this))            //待机 or 移动
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
            else if (Input.GetKeyDown(KeyCode.K) && cd_DefendOrAngry <= 0f)   //格挡
            {
                StateCurrent = InstantiateState("STPlayerDefend");
                //格挡不打断普攻连携
                BFPlayerAttackContinuity buff = BuffAdd("BFPlayerAttackContinuity") as BFPlayerAttackContinuity;
                buff.attackID = 3;
            }
        }
        //攻击04
        else if (stateCurrentName == "STPlayerAttack04")
        {
            if (StateCurrent.Finished(this))            //待机 or 移动
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
            else if (Input.GetKeyDown(KeyCode.K) && cd_DefendOrAngry <= 0f)   //格挡
            {
                StateCurrent = InstantiateState("STPlayerDefend");
                //格挡不打断普攻连携
                BFPlayerAttackContinuity buff = BuffAdd("BFPlayerAttackContinuity") as BFPlayerAttackContinuity;
                buff.attackID = 4;
            }
        }
        //击破攻击
        else if (stateCurrentName == "STPlayerBreakAttack")
        {
            if (StateCurrent.Finished(this))                //待机 or 移动
            {
                if (dic_Input == Vector2.zero)              //待机
                {
                    StateCurrent = InstantiateState("STPlayerIdle");
                }
                else if (dic_Input != Vector2.zero)         //移动
                {
                    StateCurrent = InstantiateState("STPlayerWalk");
                }
            }
        }
        //受击硬直
        else if (stateCurrentName == "STPlayerStun")
        {
            if (StateCurrent.Finished(this))             //待机 or 移动
            {
                if (dic_Input == Vector2.zero)          //待机
                {
                    StateCurrent = InstantiateState("STPlayerIdle");
                }
                else if (dic_Input != Vector2.zero)     //移动
                {
                    StateCurrent = InstantiateState("STPlayerWalk");
                }
            }
            else if (Input.GetKeyDown(KeyCode.K) && cd_DefendOrAngry <= 0f)    //暴怒
            {
                StateCurrent = InstantiateState("STPlayerAngry");
                BuffAdd("BFPlayerAngryEnhanceAttack01");        //强化普攻buff
                if (BuffContain("BFPlayerAttackContinuity"))    //移除普攻连携buff
                {
                    BuffRemove("BFPlayerAttackContinuity");
                }
            }
        }
        //ST暴怒
        else if (stateCurrentName == "STPlayerAngry")
        {
            if (Input.GetKeyDown(KeyCode.J))            //攻击
            {
                BFPlayerAngryEnhanceAttack01 buff_atk01_enhance = BuffGet("BFPlayerAngryEnhanceAttack01") as BFPlayerAngryEnhanceAttack01;
                if (buff_atk01_enhance == null)         //攻击01
                {
                    StateCurrent = InstantiateState("STPlayerAttack01");
                }
                else if (buff_atk01_enhance != null)    //强化攻击01
                {
                    StateCurrent = InstantiateState("STPlayerAttack01_Enhance");
                    BuffRemove("BFPlayerAngryEnhanceAttack01");
                }
            }
            else if (dic_Input != Vector2.zero) //移动
            {
                StateCurrent = InstantiateState("STPlayerWalk");
            }
            else if (Input.GetKeyDown(KeyCode.K) && cd_DefendOrAngry <= 0f)//格挡
            {
                StateCurrent = InstantiateState("STPlayerDefend");
            }
            else if (dic_Input != Vector2.zero) //移动
            {
                StateCurrent = InstantiateState("STPlayerWalk");
            }
            else if (dic_Input == Vector2.zero && StateCurrent.Finished(this))//待机
            {
                StateCurrent = InstantiateState("STPlayerIdle");
            }
            else if (Input.GetKeyDown(KeyCode.K) && cd_DefendOrAngry <= 0f)   //格挡
            {
                StateCurrent = InstantiateState("STPlayerDefend");
            }
        }
        //格挡
        else if (stateCurrentName == "STPlayerDefend")
        {
            if (StateCurrent.Finished(this))             //待机 or 移动
            {
                //未弹反，断普攻连招
                BuffRemove("BFPlayerAttackContinuity");
                if (dic_Input == Vector2.zero)      //待机
                {
                    StateCurrent = InstantiateState("STPlayerIdle");
                }
                else if (dic_Input != Vector2.zero) //移动
                {
                    StateCurrent = InstantiateState("STPlayerWalk");
                }
            }
            else if (transDefend_Achieve)            //弹反
            {
                //弹反成功，buff期间能续普攻连招
                (BuffGet("BFPlayerAttackContinuity") as BFPlayerAttackContinuity).Active();
                StateCurrent = InstantiateState("STPlayerDefend_Achieve");
            }
        }
        //弹反
        else if (stateCurrentName == "STPlayerDefend_Achieve")
        {
            if (StateCurrent.Finished(this) && dic_Input == Vector2.zero)//待机
            {
                StateCurrent = InstantiateState("STPlayerIdle");
            }
            else if (Input.GetKeyDown(KeyCode.J))        //攻击
            {
                BFPlayerAttackContinuity buff_atk_continuity = BuffGet("BFPlayerAttackContinuity") as BFPlayerAttackContinuity;
                BFPlayerAngryEnhanceAttack01 buff_atk01_enhance = BuffGet("BFPlayerAngryEnhanceAttack01") as BFPlayerAngryEnhanceAttack01;
                if ((buff_atk_continuity == null || (buff_atk_continuity.IfActive() && buff_atk_continuity.attackID == 4)) && buff_atk01_enhance == null)    //攻击01
                {
                    StateCurrent = InstantiateState("STPlayerAttack01");
                }
                else if (buff_atk01_enhance != null)          //强化攻击01
                {
                    StateCurrent = InstantiateState("STPlayerAttack01_Enhance");
                    BuffRemove("BFPlayerAngryEnhanceAttack01");
                }
                //连携普攻
                else if (buff_atk_continuity.IfActive() && buff_atk_continuity.attackID == 1)                 //攻击02
                {
                    StateCurrent = InstantiateState("STPlayerAttack02");
                }
                else if (buff_atk_continuity.IfActive() && buff_atk_continuity.attackID == 2)                 //攻击03
                {
                    StateCurrent = InstantiateState("STPlayerAttack03");
                }
                else if (buff_atk_continuity.IfActive() && buff_atk_continuity.attackID == 3)                 //攻击04
                {
                    StateCurrent = InstantiateState("STPlayerAttack04");
                }
            }
            else if (Input.GetKeyDown(KeyCode.K) && cd_DefendOrAngry <= 0f)//格挡
            {
                StateCurrent = InstantiateState("STPlayerDefend");
            }
            else if (dic_Input != Vector2.zero)         //移动
            {
                StateCurrent = InstantiateState("STPlayerWalk");
            }

        }

        //-----技能-----
        else if (stateCurrentName == "STPlayerSkillW1")
        {
            if (StateCurrent.Finished(this))                //待机 or 移动
            {
                if (dic_Input == Vector2.zero)              //待机
                {
                    StateCurrent = InstantiateState("STPlayerIdle");
                }
                else if (dic_Input != Vector2.zero)         //移动
                {
                    StateCurrent = InstantiateState("STPlayerWalk");
                }
            }
        }
        else if (stateCurrentName == "STPlayerSkillS1")
        {
            if (StateCurrent.Finished(this))                //待机 or 移动
            {
                if (dic_Input == Vector2.zero)              //待机
                {
                    StateCurrent = InstantiateState("STPlayerIdle");
                }
                else if (dic_Input != Vector2.zero)         //移动
                {
                    StateCurrent = InstantiateState("STPlayerWalk");
                }
            }
        }
        else if (stateCurrentName == "STPlayerSkillD1")
        {
            if (StateCurrent.Finished(this))                //待机 or 移动
            {
                if (dic_Input == Vector2.zero)              //待机
                {
                    StateCurrent = InstantiateState("STPlayerIdle");
                }
                else if (dic_Input != Vector2.zero)         //移动
                {
                    StateCurrent = InstantiateState("STPlayerWalk");
                }
            }
        }
    }

    public override void UPStateBehaviour()
    {
        base.UPStateBehaviour();

        //通用行为

        if (time_Stun <= 0f && time_Angry <= 0f)    //流失灰色生命
        {
            healthGray = Mathf.Clamp(healthGray - healthGray_LoseSpeed * Time.deltaTime, health, health_Max);
        }
        //流失 解除硬直获得的angry时间
        time_Angry = Mathf.Clamp(time_Angry - Time.deltaTime, 0f, time_Angry);
        //流逝CD
        cd_DefendOrAngry = Mathf.Clamp(cd_DefendOrAngry - Time.deltaTime, 0f, cd_DefendOrAngry);
    }
    //-----------------方法-----------------
    public override void Hurt(Entity entity, AttackBox attackBox)
    {
        base.Hurt(entity, attackBox);

        //治疗灰色生命
        float h = healthGray - health;
        if (h > 0f)
        {
            health = Mathf.Clamp(health + attackValue, health, healthGray);
        }

        //降低目标耐性
        entity.DetectResis(this);
    }

    public override void GetHurt(Entity entity, AttackBox attackBox)
    {
        if (stateCurrentName == "STPlayerDefend")
        {
            transDefend_Achieve = true;//进入弹反
            transDefend_achieve_TgtEntity = entity;
        }
        else if (BuffContain("BFPlayerUnselected"))
        {
            //不操作
        }
        else
        {
            base.GetHurt(entity, attackBox);//扣health
            transStun = true;//进入硬直}
        }
    }

    /// <summary>
    /// 开始引导击破攻击。绑定到message manager了。
    /// </summary>
    /// <param name="entity"></param>
    public void GuideBreakAttack(Entity entity)
    {
        eTarget_BreakAttack = entity;
        Buff buff = BuffAdd("BFPlayerGuideBreakAttack");
    }

}