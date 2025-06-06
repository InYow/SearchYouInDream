using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WonderMonoUI : MonoBehaviour
{
    public Image image;

    private void Update()
    {
        image.fillAmount = SkillManager.instance.wonderMono.x / SkillManager.instance.wonderMono.y;
    }
}
