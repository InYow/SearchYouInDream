using UnityEngine;

namespace UI.UISystem.UIFramework
{
    public class UICanvas : MonoBehaviour
    {
        public UIType canvasType;
        public Canvas canvas;

        /// <summary>
        /// 设置Canvas的层级
        /// </summary>
        /// <param name="order">渲染层级</param>
        public void SetCanvasOrder(int order)
        {
            if (canvas == null)
            {
                Debug.LogError("UI canvas not assigned");
            }
            canvas.sortingOrder = order;
        }

        public void SetRenderCamera()
        {
            if (canvas == null)
            {
                Debug.LogError("UI canvas not assigned");
            }

            var renderType = canvas.renderMode;
            switch (renderType)
            {
                case RenderMode.WorldSpace:
                    canvas.worldCamera = Camera.main;
                    break;
                case RenderMode.ScreenSpaceCamera:
                    canvas.worldCamera = Camera.main;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 打开UI时调用
        /// </summary>
        /// <param name="manager"></param>
        public virtual void OnCanvasEnter(UIManager manager) { }
        public virtual void OnCanvasResume(UIManager manager) { }
        public virtual void OnCanvasPause(UIManager manager) { }
        /// <summary>
        /// 关闭UI时调用
        /// </summary>
        /// <param name="manager"></param>
        public virtual void OnCanvasExit(UIManager manager) { }
    }
}