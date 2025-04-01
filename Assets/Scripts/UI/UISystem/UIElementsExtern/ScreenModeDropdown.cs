using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleFrameWork.Scripts.UIElementsExtern
{
    public class ScreenModeDropdown : MonoBehaviour
    {
        private Dropdown m_Dropdown;

        public void OnEnable()
        {
            m_Dropdown = GetComponent<Dropdown>();
            if (m_Dropdown != null)
            {
                InitializeEntries();
                m_Dropdown.onValueChanged.AddListener(OnUpdateResolution);
            }
        }

        public void OnDisable()
        {
            m_Dropdown?.onValueChanged.RemoveListener(OnUpdateResolution);
        }

        private void InitializeEntries()
        {
            m_Dropdown.ClearOptions();
            m_Dropdown.options.Add(new Dropdown.OptionData("Full Screen (Exclusive)"));
            m_Dropdown.options.Add(new Dropdown.OptionData("Full Screen (Windowed)"));
            m_Dropdown.options.Add(new Dropdown.OptionData("Maximized Window"));
            m_Dropdown.options.Add(new Dropdown.OptionData("Window"));
            
            m_Dropdown.SetValueWithoutNotify((int)Screen.fullScreenMode);
        }
        
        private void OnUpdateResolution(int value)
        {
            Screen.fullScreenMode = (FullScreenMode)value;
        }
    }
}
