using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 操作指南Canvas : MonoBehaviour
{
    public GameObject go;
    public bool opening;

    private void Start()
    {
        Close();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!opening)
                Open();
            else
                Close();
        }
    }

    public void Open()
    {
        opening = true;
        go.SetActive(true);
        SlowMotion.StartSlow(9999f, 0f);
    }

    public void Close()
    {
        opening = false;
        go.SetActive(false);
        SlowMotion.FinishSlow();
    }
}
