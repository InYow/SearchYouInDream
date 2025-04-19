using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

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
