using UI.UISystem.UIFramework;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : UICanvas
{
    public Toggle bagPackToggle;
    public Toggle skillToggle;
    public GameObject playerBagPack;
    public GameObject playerSkillSelector;
    
    public override void OnCanvasEnter(UIManager manager)
    {
        base.OnCanvasEnter(manager);
        bagPackToggle.onValueChanged.AddListener(EnableBagPackPanel);
        skillToggle.onValueChanged.AddListener(EnableSkillPanel);
        
        bagPackToggle.isOn = true;
        skillToggle.isOn = false;
    }

    public override void OnCanvasExit(UIManager manager)
    {
        base.OnCanvasExit(manager);
        bagPackToggle.onValueChanged.RemoveListener(EnableBagPackPanel);
        skillToggle.onValueChanged.RemoveListener(EnableSkillPanel);
    }

    private void EnableBagPackPanel(bool value)
    {
        playerBagPack.SetActive(value);
        
        skillToggle.SetIsOnWithoutNotify(!value);
        playerSkillSelector.SetActive(!value);
    }

    private void EnableSkillPanel(bool value)
    {
        playerSkillSelector.SetActive(value);
        
        bagPackToggle.SetIsOnWithoutNotify(!value);
        playerBagPack.SetActive(!value);
    }
}
