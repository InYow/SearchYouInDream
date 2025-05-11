using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    public static CameraFollow Instance;

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

    public static void SetFollow(Transform transform)
    {
        var currentVirtualCamera = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;
        Instance.virtualCamera = currentVirtualCamera as Cinemachine.CinemachineVirtualCamera;
        Instance.virtualCamera.Follow = transform;
    }

    public static void EnableConfiner()
    {
        Instance.virtualCamera.GetComponent<CinemachineConfiner2D>().enabled = true;
    }

    public static void DisableConfiner()
    {
        Instance.virtualCamera.GetComponent<CinemachineConfiner2D>().enabled = false;
    }
}
