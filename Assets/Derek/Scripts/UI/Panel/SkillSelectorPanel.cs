using System;
using System.Collections.Generic;
using Derek.Scripts.UI.PanelElements;
using UI.UISystem.UIFramework;
using UnityEngine;
using UnityEngine.Serialization;

namespace Derek.Scripts.UI.Panel
{
    [Serializable]
    public struct SkillUI
    {
        public int skillID;
        public SkillUISlot skillUI;
    }
    
    public class SkillSelectorPanel : UIPanel
    {
        [SerializeField]
        List<SkillUI> skillsUI = new List<SkillUI>();
        
        public void InitialSkillPanel(List<int> skills)
        {
            foreach (var skill in skillsUI)
            {
                skill.skillUI.SetSkillAvailable(false);
            }
            
            foreach (int skill in skills)
            {
                var availableSkill = skillsUI.Find(s => s.skillID == skill);
                availableSkill.skillUI.SetSkillAvailable(true);
            }
        }
    }
}