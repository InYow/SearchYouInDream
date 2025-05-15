using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public static CameraZone Instance;

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
    }



    public static void SetSpeed(float speed)
    {

    }
}
