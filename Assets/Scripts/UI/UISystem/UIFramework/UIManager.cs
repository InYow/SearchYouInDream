using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace UI.UISystem.UIFramework
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager m_Instance;
        public static UIManager instance => m_Instance;
        
        /// <summary>
        /// 预制体加载路径
        /// </summary>
        public string PrefabFilePath = "Prefabs/UICanvas";
        /// <summary>
        /// 记录打开的Canvas
        /// </summary>
        private Dictionary<UIType,UICanvas> uiCanvasSet = new Dictionary<UIType, UICanvas>();
        private Dictionary<UIType,string> loadedCanvasType2Name  = new Dictionary<UIType,string>();
        private Dictionary<string,UIType> loadedCanvasName2Type  = new Dictionary<string,UIType>();
        protected void Awake()
        {
            if (m_Instance != null)
            {
                Destroy(this.gameObject);
            }
            m_Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        /// <summary>
        /// 创建UI，确保预制体的最高层级物体为Canvas，并挂了UICanvas组件的子类。
        /// </summary>
        /// <param name="prefabName"></param>
        /// <param name="order"></param>
        /// 懒加载，用时加载一次，之后只要不销毁UI则通关关闭或启用UI完成UI控制
        public void CreateCanvas(string prefabName,int order = 0)
        {
            if (loadedCanvasName2Type.ContainsKey(prefabName))
            {
                var canvasType = loadedCanvasName2Type[prefabName];
                UICanvas canvas = uiCanvasSet[canvasType];
                canvas.SetRenderCamera();
                canvas.SetCanvasOrder(order);
                                
                canvas.gameObject.SetActive(true);
                canvas.OnCanvasEnter(this);
                return;
            }
            
            string loadPath = Path.Combine(PrefabFilePath, prefabName);
            GameObject uiPrefab = Resources.Load<GameObject>(loadPath);
            GameObject uiObj = Instantiate(uiPrefab,transform); 
            UICanvas uiPanel = uiObj.GetComponent<UICanvas>();
            if (uiPanel == null)
            {
                Debug.LogWarning("Canvas not found in Resources");
                return;
            }
            if (uiCanvasSet.ContainsKey(uiPanel.canvasType))
            {
                Debug.LogWarning("Canvas already created");
                Destroy(uiObj);
                return;
            }

            uiPanel.SetRenderCamera();
            uiPanel.SetCanvasOrder(order);
            loadedCanvasName2Type.Add(prefabName, uiPanel.canvasType);
            loadedCanvasType2Name.Add(uiPanel.canvasType,prefabName);
            uiCanvasSet.Add(uiPanel.canvasType, uiPanel);
            uiPanel.OnCanvasEnter(this);
        }
        
        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="canvasType"></param>
        public void CloseCanvas(UIType canvasType)
        {
            if (!uiCanvasSet.ContainsKey(canvasType))
            {
                Debug.LogWarning("Canvas not found");
            }

            var canvas = uiCanvasSet[canvasType];
            //uiCanvasSet.Remove(canvasType);
            canvas.OnCanvasExit(this);
            canvas.gameObject.SetActive(false);
            //Destroy(canvas.gameObject);
        }
        
        /// <summary>
        /// 销毁UI
        /// </summary>
        /// <param name="canvasType"></param>
        public void DestroyCanvas(UIType canvasType)
        {
            if (!uiCanvasSet.ContainsKey(canvasType))
            {
                Debug.LogWarning("Canvas not found");
            }

            var canvasName = loadedCanvasType2Name[canvasType];
            loadedCanvasName2Type.Remove(canvasName);
            loadedCanvasType2Name.Remove(canvasType);
            
            var canvas = uiCanvasSet[canvasType];
            uiCanvasSet.Remove(canvasType);
            
            canvas.OnCanvasExit(this);
            
            Destroy(canvas.gameObject);
        }
    }
}
