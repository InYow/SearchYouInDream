using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    public CinemachineImpulseSource cinemachineImpulseSource;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public static void Shake(Vector3 velocity, float force)
    {
        instance.cinemachineImpulseSource.m_DefaultVelocity = velocity;
        instance.cinemachineImpulseSource.GenerateImpulseWithForce(force);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Shake(new Vector3(1, 0, 0), 1);
        }
    }
}
