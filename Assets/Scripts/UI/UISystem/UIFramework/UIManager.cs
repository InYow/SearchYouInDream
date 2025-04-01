using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace UI.UISystem.UIFramework
{
    // [Serializable]
    // public struct UIGameObjectPair
    // {
    //     public UIType uiType;
    //     public GameObject uiCanvasPrefab;
    // }
    
    public class UIManager : MonoBehaviour
    {
        private static UIManager m_Instance;
        public static UIManager instance => m_Instance;
        
        //[SerializeField]
        //private List<UIGameObjectPair> CanvasPairs;
        /// <summary>
        /// 预制体加载路径
        /// </summary>
        public string PrefabFilePath = "Prefabs/UICanvas";
        /// <summary>
        /// 记录打开的Canvas
        /// </summary>
        private Dictionary<UIType,UICanvas> uiCanvasSet = new Dictionary<UIType, UICanvas>();
        
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
        /// 创建UI，需确保预制体的最高层级物体为Canvas，并挂了UICanvas组件的子类。
        /// </summary>
        /// <param name="prefabName"></param>
        /// <param name="order"></param>
        public void CreateCanvas(string prefabName,int order = 0)
        {
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
                DestroyImmediate(uiObj);
                return;
            }

            uiPanel.SetRenderCamera();
            uiPanel.SetCanvasOrder(order);
            uiCanvasSet.Add(uiPanel.canvasType, uiPanel);
            uiPanel.OnCanvasEnter(this);
        }
        
        // public void CreateCanvas(UIType canvasType,int order = 0)
        // {
        //     var canvasPrefab = CanvasPairs.Find(c => c.uiType == canvasType);
        //     
        //     GameObject uiObj = Instantiate(canvasPrefab.uiCanvasPrefab,transform); 
        //     UICanvas uiCanvas = uiObj.GetComponent<UICanvas>();
        //     if (uiCanvas == null)
        //     {
        //         Debug.LogWarning("Canvas not found in Resources");
        //         return;
        //     }
        //     if (uiCanvasSet.ContainsKey(uiCanvas.canvasType))
        //     {
        //         Debug.LogWarning("Canvas already created");
        //         DestroyImmediate(uiObj);
        //         return;
        //     }
        //
        //     uiCanvas.SetRenderCamera();
        //     uiCanvas.SetCanvasOrder(order);
        //     uiCanvasSet.Add(uiCanvas.canvasType, uiCanvas);
        //     uiCanvas.OnCanvasEnter(this);
        // }

        public bool HasCanvasActivate(UIType canvasType)
        {
            return uiCanvasSet.ContainsKey(canvasType);
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
            uiCanvasSet.Remove(canvasType);
            canvas.OnCanvasExit(this);
            Destroy(canvas.gameObject);
        }
    }
}
