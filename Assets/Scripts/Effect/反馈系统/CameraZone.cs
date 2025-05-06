using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public static CameraZone Instance;

    [Header("测试")]
    public CinemachineVirtualCamera testcamera;

    public float testsize;

    [Header("Debug")]
    public CinemachineVirtualCamera virtualCamera;

    public float StartLenOrthoSize;

    public float EndLenOrthoSize;

    public float speed;

    public bool setting = false;

    public float t = 0f;

    public float _t;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Set();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Back();
        }

        if (virtualCamera == null)
            return;
        if (setting)
        {
            _t += Time.deltaTime;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(StartLenOrthoSize, EndLenOrthoSize, _t / t);
        }
        else
        {
            _t += Time.deltaTime;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(EndLenOrthoSize, StartLenOrthoSize, _t / t);
        }
    }

    [ContextMenu("设置")]
    public void Set()
    {
        SetLenOrthoSize(testcamera, testsize);
    }

    [ContextMenu("撤销")]
    public void Back()
    {
        OriginalLenOrthoSize();
    }

    public void SetLenOrthoSize(CinemachineVirtualCamera camera, float targetSize)
    {
        setting = true;
        virtualCamera = camera;
        StartLenOrthoSize = camera.m_Lens.OrthographicSize;
        EndLenOrthoSize = targetSize;
        t = Mathf.Abs(StartLenOrthoSize - EndLenOrthoSize) / speed;
        _t = 0f;
    }

    public void OriginalLenOrthoSize()
    {
        setting = false;
        EndLenOrthoSize = virtualCamera.m_Lens.OrthographicSize;
        t = Mathf.Abs(StartLenOrthoSize - EndLenOrthoSize) / speed;
        _t = 0f;
    }

    public void SetSpeed(float speed)
    {

    }
}
