using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    public Image energyBar;

    public float f1;
    public float f2;
    public float f3;
    public float f4;

    private void Update()
    {
        float fillAmount = 0f;
        int number = SkillManager.instance.energyBeans;
        if (number == 4)
        {
            fillAmount = f4;
        }
        else if (number == 3)
        {
            fillAmount = f3;
            fillAmount += SkillManager.instance.mono.x / SkillManager.instance.mono.y * (f4 - f3);
        }
        else if (number == 2)
        {
            fillAmount = f2;
            fillAmount += SkillManager.instance.mono.x / SkillManager.instance.mono.y * (f3 - f2);
        }
        else if (number == 1)
        {
            fillAmount = f1;
            fillAmount += SkillManager.instance.mono.x / SkillManager.instance.mono.y * (f2 - f1);
        }
        else if (number == 0)
        {
            fillAmount = 0f;
            fillAmount += SkillManager.instance.mono.x / SkillManager.instance.mono.y * (f1 - 0f);
        }

        energyBar.fillAmount = fillAmount;
    }
}
