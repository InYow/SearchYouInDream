using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FightCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public static FightCamera Instance;

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
        Instance.virtualCamera.Follow = transform;
    }
}
