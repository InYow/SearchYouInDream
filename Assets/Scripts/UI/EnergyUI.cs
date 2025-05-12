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

        foreach (var energyBean in energyBeans)
        {
            int number = 0;

            if (energyBeans.Count > 0)
            {
                number = energyBeans.IndexOf(energyBean);
            }
        }
    }
}
