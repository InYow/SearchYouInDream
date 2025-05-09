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

    public float originSpeed;

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
            _t += Time.unscaledDeltaTime;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(StartLenOrthoSize, EndLenOrthoSize, _t / t);
        }
        else
        {
            _t += Time.unscaledDeltaTime;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(EndLenOrthoSize, StartLenOrthoSize, _t / t);
        }
    }

    void Set()
    {
        SetLenOrthoSize(Instance.testcamera, Instance.testsize);
    }

    void Back()
    {
        OriginalLenOrthoSize();
    }

    public static void SetLenOrthoSize(CinemachineVirtualCamera camera, float targetSize)
    {
        Instance.setting = true;
        Instance.virtualCamera = camera;
        Instance.StartLenOrthoSize = camera.m_Lens.OrthographicSize;
        Instance.EndLenOrthoSize = targetSize;
        Instance.t = Mathf.Abs(Instance.StartLenOrthoSize - Instance.EndLenOrthoSize) / Instance.speed;
        Instance._t = 0f;
    }

    public static void OriginalLenOrthoSize()
    {
        Instance.setting = false;
        Instance.EndLenOrthoSize = Instance.virtualCamera.m_Lens.OrthographicSize;
        Instance.t = Mathf.Abs(Instance.StartLenOrthoSize - Instance.EndLenOrthoSize) / Instance.originSpeed;
        Instance._t = 0f;
    }

    public static void SetSpeed(float speed)
    {

    }
}
