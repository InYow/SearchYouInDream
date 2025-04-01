using System.Collections;
using System.Collections.Generic;
using UI.UISystem.UIFramework;
using UnityEngine;

public class UITest : MonoBehaviour
{
    public string canvasName ="PlayerMenuCanvas";
    public UIType canvasType;
    private bool gate = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (gate)
            {
                UIManager.instance.CreateCanvas(canvasName);
                gate = false;
            }
            else
            {
                UIManager.instance.CloseCanvas(canvasType);
                gate = false;
            }
        }
    }
}
