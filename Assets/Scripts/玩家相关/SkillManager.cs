using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.Timeline;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public Vector2 mono;

    public Vector2 wonderMono;

    public float healthRestore;

    public int energyBeans; // 能量豆
    public int energyBeansMax; // 能量豆上限

    public string skill1;       //AD
                                //Player_进攻_烈焰冲撞

    public string skill2;       //W
                                //Player_进攻_幻影连斩

    public string skill3;       //S
                                //Player_控制_裂地拳

    public string skill4;

    public string skill5;       //击破
                                //Player_击破_气爆拳
                                //Player_击破_浩克掌

    public string skill6;       //无方向
                                //Player_增幅_愤怒

    public string skill7;       //冲刺期间
                                //Player_击破_浩克掌_拍掌

    [Header("冲刺")]
    public float 冲刺恢复CDMax;
    public float 冲刺恢复CD;
    public int 冲刺次数Max;
    public int 冲刺次数;

    [Header("角色")]
    public Player player;


    [Header("测试区域")]
    public List<string> skillList = new List<string>(); //测试区域

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        冲刺恢复CD = 0f;
        冲刺次数 = 冲刺次数Max;

        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        UpDataDashCD();
    }

    private void UpDataDashCD()
    {
        if (player.stateCurrentName != "Player_冲刺" && 冲刺恢复CD > 0)
        {
            冲刺恢复CD -= Time.deltaTime;
            if (冲刺恢复CD <= 0)
            {
                冲刺次数 = 冲刺次数Max;
            }
        }
    }

    public static void AddMono(float value)
    {
        if (instance.energyBeans == instance.energyBeansMax)
            return;

        instance.mono.x = Mathf.Clamp(instance.mono.x + value, 0f, instance.mono.y);

        if (instance.mono.x == instance.mono.y && instance.energyBeans < instance.energyBeansMax)
        {
            instance.mono.x = 0f;
            instance.energyBeans = Mathf.Clamp(instance.energyBeans + 1, 0, instance.energyBeansMax);
        }
    }

    public static void AddWonderMono(float value)
    {
        instance.wonderMono.x = Mathf.Clamp(instance.wonderMono.x + value, 0f, instance.wonderMono.y);
    }

    public static bool IfCanDash()
    {
        if (instance.冲刺次数 > 0)
            return true;
        else
            return false;
    }

    public static void Dash()
    {
        if (instance.冲刺次数 == instance.冲刺次数Max)
        {
            instance.冲刺次数--;
            instance.冲刺恢复CD = instance.冲刺恢复CDMax;
        }
        else
        {
            instance.冲刺次数--;
        }
    }

    /// <summary>
    /// 1234 AWSD
    /// </summary>
    /// <param name="skillID"></param>
    /// <returns></returns>
    public static string GetSkillName(int skillID)
    {
        switch (skillID)
        {
            case 1://大招
                {
                    if (instance.wonderMono.x == instance.wonderMono.y)
                    {
                        instance.wonderMono.x = 0f;
                        return instance.skill1; //返回大招
                    }
                    else
                    {
                        return "";
                    }
                }
            case 2://W
                {
                    if (instance.energyBeans > 0)
                    {
                        instance.energyBeans--;
                        RestoreHealth();
                        return instance.skill2;
                    }
                    else
                    {
                        return "";
                    }
                }
            case 3://S
                {
                    if (instance.energyBeans > 0)
                    {
                        instance.energyBeans--;
                        RestoreHealth();
                        return instance.skill3;
                    }
                    else
                    {
                        return "";
                    }
                }
            case 4://AD
                {
                    if (instance.energyBeans > 0)
                    {
                        instance.energyBeans--;
                        RestoreHealth();
                        return instance.skill4;
                    }
                    else
                    {
                        return "";
                    }
                }
            case 5://击破   
                {
                    return instance.skill5;
                }
            case 6://无方向
                {
                    return instance.skill6;
                }
            case 7://冲刺期间
                {
                    if (instance.energyBeans > 0)
                    {
                        instance.energyBeans--;
                        RestoreHealth();
                        return instance.skill7;
                    }
                    else
                    {
                        return "";
                    }
                }
            default:
                {
                    return null;
                }
        }

        void RestoreHealth()
        {
            instance.player.AddHealth(instance.healthRestore);
        }
    }

    public static string GetSkillNameTestArea(int testSkillID)
    {
        return instance.skillList[testSkillID];
    }

    public static void SetSkillName(int skillID, string skillName)
    {
        switch (skillID)
        {
            case 1:
                {
                    instance.skill1 = skillName;
                    break;
                }
            case 2:
                {
                    instance.skill2 = skillName;
                    break;
                }
            case 3:
                {
                    instance.skill3 = skillName;
                    break;
                }
            case 4:
                {
                    instance.skill4 = skillName;
                    break;
                }
            default:
                {
                    instance.skill1 = skillName;
                    break;
                }
        }
    }

}
