using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleFrameWork.Scripts.UIElementsExtern
{
    public class ResolutionDropdown : MonoBehaviour
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
            int selected = 0;
            int i = 0;
            foreach (var item in Screen.resolutions.OrderByDescending(o => o.width).ThenByDescending(o => o.height))
            {
                string option = item.width + "x" + item.height;
                
                if (m_Dropdown.options.All(o => o.text != option))
                {
                    m_Dropdown.options.Add(new Dropdown.OptionData(option));
                    if (item.width == Screen.currentResolution.width && item.height == Screen.currentResolution.height)
                    {
                        selected = i;
                    }

                    i++;
                }
            }

            OnUpdateResolution(selected);
        }
        
        private void OnUpdateResolution(int value)
        {
            string option = m_Dropdown.options[value].text;
            string[] values = option.Split('x');
            Screen.SetResolution(int.Parse(values[0]), int.Parse(values[1]), Screen.fullScreen);
        }
    }
}
