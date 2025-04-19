using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public string skill1;//A
    public string skill2;//W
    public string skill3;//S
    public string skill4;//D

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
            default:
                {
                    return null;
                }
        }
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
