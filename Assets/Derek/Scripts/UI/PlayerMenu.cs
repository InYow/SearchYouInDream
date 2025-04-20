using System.Collections.Generic;
using Derek.Scripts.UI.Panel;
using Inventory;
using UI.UISystem.UIFramework;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : UICanvas
{
    public Toggle bagPackToggle;
    public Toggle skillToggle;
    public GameObject playerBagPack;
    public GameObject playerSkillSelector;
    
    private SkillDataBase skillDataSingleton;
    private BagPackPanel bagPackPanel;
    private SkillSelectorPanel skillSelectorPanel;
    
    public override void OnCanvasEnter(UIManager manager)
    {
        base.OnCanvasEnter(manager);

        InitialCanvas();
        
        //Bind Event
        bagPackToggle.onValueChanged.AddListener(EnableBagPackPanel);
        skillToggle.onValueChanged.AddListener(EnableSkillPanel);
        //Set UI State
        bagPackToggle.isOn = true;
        skillToggle.isOn = false;
    }

    private void InitialCanvas()
    {
        skillDataSingleton = SkillDataBase.instance;
        
        if (skillSelectorPanel == null)
        {
            skillSelectorPanel = playerSkillSelector.GetComponent<SkillSelectorPanel>();
        }

        if (bagPackPanel == null)
        {
            bagPackPanel = playerBagPack.GetComponent<BagPackPanel>();
        }

        //var inventoryData= InventoryManager.instance.GetInventoryData();
        //bagPackPanel.InitialInventoryPanel(inventoryData);
    }

    public override void OnCanvasExit(UIManager manager)
    {
        base.OnCanvasExit(manager);
        //Unbind Event
        bagPackToggle.onValueChanged.RemoveListener(EnableBagPackPanel);
        skillToggle.onValueChanged.RemoveListener(EnableSkillPanel);
        
        skillDataSingleton = null;
    }

    private void EnableBagPackPanel(bool value)
    {
        playerBagPack.SetActive(value);
        
        skillToggle.SetIsOnWithoutNotify(!value);
        playerSkillSelector.SetActive(!value);
    }

    private void EnableSkillPanel(bool value)
    {
        List<int> skills = new List<int>();
        if (skillDataSingleton != null)
        { 
            skills = skillDataSingleton.GetAllAvailableSkills();
        }
        skillSelectorPanel.InitialSkillPanel(skills);
        
        playerSkillSelector.SetActive(value);
        
        bagPackToggle.SetIsOnWithoutNotify(!value);
        playerBagPack.SetActive(!value);
    }
}
