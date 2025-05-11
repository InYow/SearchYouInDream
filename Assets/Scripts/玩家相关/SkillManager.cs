using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public Vector2 mono;

    public string skill1;       //AD
                                //Player_增幅_愤怒

    public string skill2;       //W
                                //Player_控制_裂地

    public string skill3;       //S
                                //Player_进攻_烈焰冲撞

    public string skill4;

    public string skill5;       //击破
                                //Player_击破_气爆拳
                                //Player_击破_浩克掌

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
        if (player.stateCurrentName != "Player_冲刺" && 冲刺恢复CD > 0)
        {
            冲刺恢复CD -= Time.deltaTime;
            if (冲刺恢复CD <= 0)
            {
                冲刺次数 = 冲刺次数Max;
            }
        }
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
            case 1://A
                {
                    return instance.skill1;
                }
            case 2://W
                {
                    return instance.skill2;
                }
            case 3://S
                {
                    return instance.skill3;
                }
            case 4://D
                {
                    return instance.skill4;
                }
            case 5://击破   
                {
                    return instance.skill5;
                }
            default:
                {
                    return null;
                }
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
