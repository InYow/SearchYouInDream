using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    public List<Image> energyBeans = new List<Image>(); // 能量豆

    public Image energyBar;

    private void Update()
    {
        energyBar.fillAmount = SkillManager.instance.mono.x / SkillManager.instance.mono.y;

        int number = SkillManager.instance.energyBeans;
        foreach (var energyBean in energyBeans)
        {
            if (number > 0)
            {
                number--;
                energyBean.gameObject.SetActive(true);
            }
            else
            {
                energyBean.gameObject.SetActive(false);
            }
        }
    }
}
