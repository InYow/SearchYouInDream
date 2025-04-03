using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Derek.Scripts.UI.PanelElements
{
    public class SkillUISlot : MonoBehaviour
    {
        public Image iconImage;
        
        public void SetSkillAvailable(bool value)
        {
            iconImage.color = value ? Color.white : Color.grey;
        }
    }
}