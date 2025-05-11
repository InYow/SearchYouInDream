using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

public class Player : Entity
{
    [Header("玩家")]
    public float healthGray;
    public float healthGray_LoseSpeed;
    public float speed_walk;
    public float speed_fly;
    public float attackResisValue;
    public PickableItem pickableItem;   //拾取的物品
    public float speed_throw;           //投掷的物品的速度
    public float radius_outer = 5f;     //扇形内径
    public float radius_inner = 2f;     //扇形外径
    public float angle = 60f;           //扇形角度
    public LayerMask targetLayer;       //目标层
    public float distance_击破_气爆拳;   //击破_气爆拳_追击距离
    public float distance_击破_浩克掌;   //击破_浩克掌_追击距离
    [ReadOnly] public float time_Stun;  //硬直时长
    public float time_Angry;            //解除的硬直时长
    public float time_StunMax;
    public float cd_DefendOrAngry;      //格挡or暴怒CD
    public float cdMax_DefendOrAngry;

    [Header("状态机全局变量")]
    public Vector2 dic_Input;   //四键方向

    public bool transStun;      //进入stun布尔值
    public Entity transHitFly_SourceEntity;         //击飞源实体
    public CheckBox transHitFly_SourceAttackBox;    //击飞源攻击盒子
    public CheckBoxBehaviour transHitFly_SourceCheckBoxBehaviour; //击飞源攻击盒子

    public bool transDefend_Achieve;//进入 弹反 布尔值
    public Entity transDefend_achieve_TgtEntity;//弹反目标
    public Entity eTarget_击破;  //击破攻击的目标
    public Entity eTarget_普攻1_冲刺_阶段2;    //追击的目标


    public override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
        MessageManager.Instance.OnBreakStun += GuideBreakAttack;
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

        //预输入
        if (StateCurrent.GetCurrentStateInputWindType() == InputWindType.inputpre)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                InputManager.LogPreInput(KeyCode.J);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                InputManager.LogPreInput(KeyCode.Space);
            }
        }
        if (BuffContain("BFPlayerGuideBreakAttack"))
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                InputManager.LogPreInput(KeyCode.U);
            }
        }

        base.UPInput();
    }

    public override void UPCheckStateTrans()
    {
        base.UPCheckStateTrans();

        //-----------任意状态-----------
        //测试区
        if (Input.GetKeyDown(KeyCode.T))
        {
            StateCurrent = InstantiateState(SkillManager.GetSkillNameTestArea(0));
            return;
        }
        //硬直
        if (transStun)
        {
            transStun = false;
            StateCurrent = InstantiateState("Player_受击");
            return;
        }
        //冲刺
        if (stateCurrentName != "Player_冲刺" && stateCurrentName != "Player_受击" && SkillManager.IfCanDash() && Input.GetKeyDown(KeyCode.Space))
        {
            InputManager.ClearPreInput();
            StateCurrent = InstantiateState("Player_冲刺");
        }

        //击破攻击  拥有GuideBreakAttack的buff
        if (BuffContain("BFPlayerGuideBreakAttack") && 击破Canvas.IfFinish后半段())
        {
            if (Input.GetKeyDown(KeyCode.U) || InputManager.ReadPreInput(KeyCode.U))
            {
                InputManager.ClearPreInput();
                StateCurrent = InstantiateState("Player_击破");
                return;
            }
        }

        //-----------------------------

        //ST待机
        if (stateCurrentName == "Player_待机")
        {
            if (Input.GetKeyDown(KeyCode.J))        //攻击
            {
                BFPlayerAttackContinuity_New buff_atk_continuity_New = BuffGet("BFPlayerAttackContinuity_New") as BFPlayerAttackContinuity_New;
                //BFPlayerAngryEnhanceAttack01 buff_atk01_enhance = BuffGet("BFPlayerAngryEnhanceAttack01") as BFPlayerAngryEnhanceAttack01;
                if (buff_atk_continuity_New == null || (buff_atk_continuity_New.attackID == 4 /*&& buff_atk01_enhance == null*/))    //攻击01
                {
                    StateCurrent = InstantiateState("Player_普攻1");
                }
                // else if (buff_atk01_enhance != null)          //强化攻击01
                // {
                //     StateCurrent = InstantiateState("Player_普攻1_强化");
                //     BuffRemove("BFPlayerAngryEnhanceAttack01");
                // }
                //连携普攻_New
                else if (buff_atk_continuity_New.attackID == 1)                 //攻击02
                {
                    StateCurrent = InstantiateState("Player_普攻2");
                }
                else if (buff_atk_continuity_New.attackID == 2)                 //攻击03
                {
                    StateCurrent = InstantiateState("Player_普攻3");
                }
                else if (buff_atk_continuity_New.attackID == 3)                 //攻击04
                {
                    StateCurrent = InstantiateState("Player_普攻4");
                }
            }
            else if (dic_Input != Vector2.zero)                             //移动
            {
                StateCurrent = InstantiateState("Player_跑步");
            }
            else if (Input.GetKeyDown(KeyCode.L))   //拾取、投掷
            {
                if (pickableItem == null)   //拾取
                {
                    StateCurrent = InstantiateState("Player_拾起");
                }
                else                        //投掷
                {
                    StateCurrent = InstantiateState("Player_投掷");
                }
            }
        }
        //ST移动
        else if (stateCurrentName == "Player_跑步")
        {
            if (Input.GetKeyDown(KeyCode.J))        //攻击
            {
                //Enemy enemy_closest = MethodFight.GetAtkFTarget(transform.position, GetFlipX(), radius_outer, radius_inner, angle, targetLayer);  //获取扇形所有Enemy
                BFPlayerAttackContinuity_New buff_atk_continuity_New = BuffGet("BFPlayerAttackContinuity_New") as BFPlayerAttackContinuity_New;
                //BFPlayerAngryEnhanceAttack01 buff_atk01_enhance = BuffGet("BFPlayerAngryEnhanceAttack01") as BFPlayerAngryEnhanceAttack01;
                if (buff_atk_continuity_New == null || (buff_atk_continuity_New.attackID == 4 /*&& buff_atk01_enhance == null*/))    //攻击01
                {
                    StateCurrent = InstantiateState("Player_普攻1");
                }
                // else if (enemy_closest != null &&/*追击*/(buff_atk_continuity == null || (buff_atk_continuity.IfActive() && buff_atk_continuity.attackID == 4)) && buff_atk01_enhance == null)
                // {
                //     eTarget_普攻1_冲刺_阶段2 = enemy_closest;
                //     StateCurrent = InstantiateState("Player_普攻1_冲刺_阶段1");
                // }

                //连携普攻_New
                else if (buff_atk_continuity_New.attackID == 1)                 //攻击02
                {
                    StateCurrent = InstantiateState("Player_普攻2");
                }
                else if (buff_atk_continuity_New.attackID == 2)                 //攻击03
                {
                    StateCurrent = InstantiateState("Player_普攻3");
                }
                else if (buff_atk_continuity_New.attackID == 3)                 //攻击04
                {
                    StateCurrent = InstantiateState("Player_普攻4");
                }
            }
            else if (dic_Input == Vector2.zero)      //待机
            {
                StateCurrent = InstantiateState("Player_待机");
            }
            else if (Input.GetKeyDown(KeyCode.L))   //拾取、投掷
            {
                if (pickableItem == null)   //拾取
                {
                    StateCurrent = InstantiateState("Player_拾起");
                }
                else                        //投掷
                {
                    StateCurrent = InstantiateState("Player_投掷");
                }
            }
            else if (Input.GetKey(KeyCode.I) && //技能
            (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))        //技能
            {
                if (Input.GetKey(KeyCode.W))
                {
                    StateCurrent = InstantiateState(SkillManager.GetSkillName(2));
                }
                else if (Input.GetKey(KeyCode.S))
                {

                    StateCurrent = InstantiateState(SkillManager.GetSkillName(3));
                }
                else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    StateCurrent = InstantiateState(SkillManager.GetSkillName(4));
                }
            }
        }
        //ST攻击01
        else if (stateCurrentName == "Player_普攻1")
        {
            if (StateCurrent.Finished(this))            //待机 or 移动
            {
                if (dic_Input == Vector2.zero)      //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero) //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
            else if ((Input.GetKeyDown(KeyCode.J) || InputManager.ReadPreInput(KeyCode.J)) && StateCurrent.GetCurrentStateInputWindType() == InputWindType.inputable)    //攻击02
            {
                InputManager.ClearPreInput();
                StateCurrent = InstantiateState("Player_普攻2");
            }
        }
        //ST攻击02
        else if (stateCurrentName == "Player_普攻2")
        {
            if (StateCurrent.Finished(this))            //待机 or 移动
            {
                if (dic_Input == Vector2.zero)      //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero) //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
            else if ((Input.GetKeyDown(KeyCode.J) || InputManager.ReadPreInput(KeyCode.J)) && StateCurrent.GetCurrentStateInputWindType() == InputWindType.inputable)    //攻击03
            {
                InputManager.ClearPreInput();
                StateCurrent = InstantiateState("Player_普攻3");
            }
        }
        //ST攻击03
        else if (stateCurrentName == "Player_普攻3")
        {
            if (StateCurrent.Finished(this))            //待机 or 移动
            {
                if (dic_Input == Vector2.zero)      //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero) //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
            else if ((Input.GetKeyDown(KeyCode.J) || InputManager.ReadPreInput(KeyCode.J)) && StateCurrent.GetCurrentStateInputWindType() == InputWindType.inputable)    //攻击04
            {
                InputManager.ClearPreInput();
                StateCurrent = InstantiateState("Player_普攻4");
            }
        }
        //ST攻击04
        else if (stateCurrentName == "Player_普攻4")
        {
            if (StateCurrent.Finished(this))            //待机 or 移动
            {
                if (dic_Input == Vector2.zero)      //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero) //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
            else if ((Input.GetKeyDown(KeyCode.J) || InputManager.ReadPreInput(KeyCode.J)) && StateCurrent.GetCurrentStateInputWindType() == InputWindType.inputable)    //攻击01
            {
                InputManager.ClearPreInput();
                StateCurrent = InstantiateState("Player_普攻1");
            }
        }
        //受击硬直
        else if (stateCurrentName == "Player_受击")
        {
            if (StateCurrent.Finished(this))             //待机 or 移动
            {
                if (dic_Input == Vector2.zero)          //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero)     //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
            else if (Input.GetKeyDown(KeyCode.K) && cd_DefendOrAngry <= 0f && (StateCurrent as Player_受击).time >= 0f)    //暴怒
            {
                StateCurrent = InstantiateState("Player_暴怒");
            }
        }
        //ST暴怒
        else if (stateCurrentName == "Player_暴怒")
        {
            if (Input.GetKeyDown(KeyCode.J))            //攻击
            {
                //BFPlayerAngryEnhanceAttack01 buff_atk01_enhance = BuffGet("BFPlayerAngryEnhanceAttack01") as BFPlayerAngryEnhanceAttack01;
                //if (buff_atk01_enhance == null)         //攻击01
                //{
                StateCurrent = InstantiateState("Player_普攻1");
                //}
                //else if (buff_atk01_enhance != null)    //强化攻击01
                //{
                //    StateCurrent = InstantiateState("Player_普攻1_强化");
                //    BuffRemove("BFPlayerAngryEnhanceAttack01");
                //}
            }
            else if (dic_Input != Vector2.zero) //移动
            {
                StateCurrent = InstantiateState("Player_跑步");
            }
            else if (dic_Input == Vector2.zero && StateCurrent.Finished(this))//待机
            {
                StateCurrent = InstantiateState("Player_待机");
            }
        }
        //ST拾取
        else if (stateCurrentName == "Player_拾起")
        {
            if (StateCurrent.Finished(this))             //待机 or 移动
            {
                if (dic_Input == Vector2.zero)      //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero) //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
        }
        //ST投掷
        else if (stateCurrentName == "Player_投掷")
        {
            if (StateCurrent.Finished(this))             //待机 or 移动
            {
                if (dic_Input == Vector2.zero)      //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero) //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
        }
        //ST冲刺
        else if (stateCurrentName == "Player_冲刺")
        {
            if (Input.GetKeyDown(KeyCode.J))        //攻击
            {
                InputManager.ClearPreInput();
                BFPlayerAttackContinuity_New buff_atk_continuity_New = BuffGet("BFPlayerAttackContinuity_New") as BFPlayerAttackContinuity_New;
                if (buff_atk_continuity_New == null || (buff_atk_continuity_New.attackID == 4))    //攻击01
                {
                    StateCurrent = InstantiateState("Player_普攻1");
                }
                else if (buff_atk_continuity_New.attackID == 1)                 //攻击02
                {
                    StateCurrent = InstantiateState("Player_普攻2");
                }
                else if (buff_atk_continuity_New.attackID == 2)                 //攻击03
                {
                    StateCurrent = InstantiateState("Player_普攻3");
                }
                else if (buff_atk_continuity_New.attackID == 3)                 //攻击04
                {
                    StateCurrent = InstantiateState("Player_普攻4");
                }
            }
            else if (SkillManager.IfCanDash() && (Input.GetKeyDown(KeyCode.Space) || InputManager.ReadPreInput(KeyCode.Space)) && StateCurrent.GetCurrentStateInputWindType() == InputWindType.inputable)
            {
                InputManager.ClearPreInput();
                StateCurrent = InstantiateState("Player_冲刺");
            }
            else if (StateCurrent.Finished(this))             //待机 or 移动
            {
                if (dic_Input == Vector2.zero)      //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero) //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
        }

        //-----------------技能------------------
        else if (stateCurrentName == "Player_增幅_愤怒")
        {
            if (StateCurrent.Finished(this))                //待机 or 移动
            {
                if (dic_Input == Vector2.zero)              //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero)         //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
        }
        else if (stateCurrentName == "Player_控制_裂地")
        {
            if (StateCurrent.Finished(this))                //待机 or 移动
            {
                if (dic_Input == Vector2.zero)              //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero)         //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
        }
        else if (stateCurrentName == "Player_进攻_烈焰冲撞")
        {
            if (StateCurrent.Finished(this))                //待机 or 移动
            {
                if (dic_Input == Vector2.zero)              //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero)         //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
        }
        //ST击破攻击
        else if (stateCurrentName == "Player_击破_践踏_阶段2")
        {
            if (StateCurrent.Finished(this))                //待机 or 移动
            {
                if (dic_Input == Vector2.zero)              //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero)         //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
        }
        //ST Player_击破_浩克掌_起飞
        else if (stateCurrentName == "Player_击破_浩克掌_起飞")
        {
            if (StateCurrent.Finished(this))                //Player_击破_浩克掌_拍掌
            {
                StateCurrent = InstantiateState("Player_击破_浩克掌_拍掌");
            }
        }
        //ST Player_击破_浩克掌_拍掌
        else if (stateCurrentName == "Player_击破_浩克掌_拍掌")
        {
            if (StateCurrent.Finished(this))                //待机 or 移动
            {
                if (dic_Input == Vector2.zero)              //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero)         //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
        }
        //ST Player_击破_气爆拳_准备
        else if (stateCurrentName == "Player_击破_气爆拳_准备")
        {
            if (StateCurrent.Finished(this))
            {
                if ((transform.position - eTarget_击破.transform.position).magnitude > distance_击破_气爆拳)//Player_击破_气爆拳_起跳
                {
                    StateCurrent = InstantiateState("Player_击破_气爆拳_起跳");
                }
                else
                {
                    StateCurrent = InstantiateState("Player_击破_气爆拳_攻击");//Player_击破_气爆拳_攻击
                }
            }
        }
        //ST Player_击破_气爆拳_起跳
        else if (stateCurrentName == "Player_击破_气爆拳_起跳")
        {
            if (StateCurrent.Finished(this))                //Player_击破_气爆拳_飞行
            {
                StateCurrent = InstantiateState("Player_击破_气爆拳_飞行");
            }
        }
        //ST Player_击破_气爆拳_飞行
        else if (stateCurrentName == "Player_击破_气爆拳_飞行")
        {
            if (StateCurrent.Finished(this))                //Player_击破_气爆拳_攻击
            {
                StateCurrent = InstantiateState("Player_击破_气爆拳_攻击");
            }
        }
        //ST Player_击破_气爆拳_攻击
        else if (stateCurrentName == "Player_击破_气爆拳_攻击")
        {
            if (StateCurrent.Finished(this))                //待机 or 移动
            {
                if (dic_Input == Vector2.zero)              //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero)         //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
        }
        //ST Player_击破
        else if (stateCurrentName == "Player_击破")
        {
            if (StateCurrent.Finished(this))                //待机 or 移动
            {
                if (dic_Input == Vector2.zero)              //待机
                {
                    StateCurrent = InstantiateState("Player_待机");
                }
                else if (dic_Input != Vector2.zero)         //移动
                {
                    StateCurrent = InstantiateState("Player_跑步");
                }
            }
        }

        //-------------default/测试区域-------------
        //测试区域-0
        else if (SkillManager.instance.skillList.Count > 1 && stateCurrentName == SkillManager.GetSkillNameTestArea(0))
        {
            if (StateCurrent.Finished(this))
            {
                StateCurrent = InstantiateState(SkillManager.GetSkillNameTestArea(1));
            }
        }
        //测试区域-1
        else if (SkillManager.instance.skillList.Count > 2 && stateCurrentName == SkillManager.GetSkillNameTestArea(1))
        {
            if (StateCurrent.Finished(this))
            {
                StateCurrent = InstantiateState(SkillManager.GetSkillNameTestArea(2));
            }
        }
        //测试区域-2
        else if (SkillManager.instance.skillList.Count > 3 && stateCurrentName == SkillManager.GetSkillNameTestArea(2))
        {
            if (StateCurrent.Finished(this))
            {
                StateCurrent = InstantiateState(SkillManager.GetSkillNameTestArea(3));
            }
        }
        //测试区域-3
        else if (SkillManager.instance.skillList.Count > 4 && stateCurrentName == SkillManager.GetSkillNameTestArea(3))
        {
            if (StateCurrent.Finished(this))
            {
                StateCurrent = InstantiateState(SkillManager.GetSkillNameTestArea(4));
            }
        }
        //测试区域-4
        else if (SkillManager.instance.skillList.Count > 5 && stateCurrentName == SkillManager.GetSkillNameTestArea(4))
        {
            if (StateCurrent.Finished(this))
            {
                StateCurrent = InstantiateState(SkillManager.GetSkillNameTestArea(5));
            }
        }
        //测试区域-5
        else if (SkillManager.instance.skillList.Count > 6 && stateCurrentName == SkillManager.GetSkillNameTestArea(5))
        {
            if (StateCurrent.Finished(this))
            {
                StateCurrent = InstantiateState(SkillManager.GetSkillNameTestArea(6));
            }
        }
        //default
        else if (dic_Input != Vector2.zero)          //移动
        {
            StateCurrent = InstantiateState("Player_跑步");
        }
        else if (StateCurrent.Finished(this))   //待机
        {
            StateCurrent = InstantiateState("Player_待机");
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
    public override void Hurt(Entity entity, CheckBox attackBox)
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

    public override void Hurt(Entity entity, CheckBoxBehaviour checkBoxBehaviour)
    {
        base.Hurt(entity, checkBoxBehaviour);

        //治疗灰色生命
        float h = healthGray - health;
        if (h > 0f)
        {
            health = Mathf.Clamp(health + attackValue, health, healthGray);
        }

        //降低目标耐性
        entity.DetectResis(this, checkBoxBehaviour);
    }

    public override void GetHurt(Entity entity, CheckBox attackBox)
    {
        if (stateCurrentName == "Player_防御")
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
            base.GetHurt(entity, attackBox);    //扣health
            CameraShake.ShakeRecoil(new Vector3(1, 0, 0), 1f);  //镜头抖动
            受伤Canvas.Instance.PlayAnimation();        //播放受伤UI动画
            transHitFly_SourceAttackBox = attackBox;
            transHitFly_SourceEntity = entity;
            transStun = true;//进入硬直}
        }
    }

    public override void GetHurt(Entity entity, CheckBoxBehaviour checkBoxBehaviour)
    {
        if (stateCurrentName == "Player_防御")
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
            base.GetHurt(entity, checkBoxBehaviour);    //扣health
            CameraShake.ShakeRecoil(new Vector3(1, 0, 0), 1f);  //镜头抖动
            受伤Canvas.Instance.PlayAnimation();        //播放受伤UI动画
            transHitFly_SourceCheckBoxBehaviour = checkBoxBehaviour;
            transHitFly_SourceEntity = entity;
            transStun = true;//进入硬直}
        }
    }

    /// <summary>
    /// 开始引导击破攻击。绑定到message manager了。
    /// </summary>
    /// <param name="entity"></param>
    public void GuideBreakAttack(Entity entity)
    {
        eTarget_击破 = entity;
        //VFX
        VisualEffectManager.PlayEffect("击破闪光", entity.hitVFX_Pivot);
        //Buff
        Buff buff = BuffAdd("BFPlayerGuideBreakAttack");
    }

    public void Log()
    {

    }

    //--------Gizmos--------
    private void OnDrawGizmosSelected()
    {
        DrawSector(transform.position, transform.forward, radius_outer, radius_inner, angle);
    }

    void DrawSector(Vector3 center, Vector3 forward, float radius_outer, float radius_inner, float angle)
    {
        Gizmos.color = Color.red;
        //绘制弧
        Gizmos.DrawWireSphere(center, radius_outer);
        //绘制弧
        Gizmos.DrawWireSphere(center, radius_inner);
        //计算弧边界点
        Vector3 lastPoint1 = new(center.x + radius_outer * Mathf.Cos(Mathf.Deg2Rad * angle / 2f), center.y + radius_outer * Mathf.Sin(Mathf.Deg2Rad * angle / 2f), center.z);
        Vector3 lastPoint2 = new(center.x + radius_outer * Mathf.Cos(-Mathf.Deg2Rad * angle / 2f), center.y + radius_outer * Mathf.Sin(-Mathf.Deg2Rad * angle / 2f), center.z);
        //绘制半径
        Gizmos.DrawLine(center, lastPoint1);
        Gizmos.DrawLine(center, lastPoint2);
    }
}