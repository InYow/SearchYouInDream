using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    public CinemachineImpulseSource recoilImpulseSource;
    public CinemachineImpulseSource explosionImpulseSource;
    private float recoilShakeTime = 0f;
    public float recoilShakeTimeDefault = 0f; // 默认震动时间
    private float explosionShakeTime = 0f;
    public float explosionShakeTimeDefault = 0f; // 默认震动时间
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

    private void Start()
    {
        CinemachineImpulseManager.Instance.IgnoreTimeScale = true;
        recoilShakeTimeDefault = recoilImpulseSource.m_ImpulseDefinition.m_ImpulseDuration;
        explosionShakeTimeDefault = explosionImpulseSource.m_ImpulseDefinition.m_ImpulseDuration;
    }

    public static void ShakeRecoil(Vector3 velocity, float force)
    {
        if (instance.recoilShakeTime > 0f)
        {
            instance.recoilImpulseSource.m_ImpulseDefinition.m_ImpulseDuration = instance.recoilShakeTime;
            instance.recoilShakeTime = 0f;
        }
        else
        {
            instance.recoilImpulseSource.m_ImpulseDefinition.m_ImpulseDuration = instance.recoilShakeTimeDefault;
        }
        instance.recoilImpulseSource.m_DefaultVelocity = velocity;
        instance.recoilImpulseSource.GenerateImpulseWithForce(force);
    }
    public static void ShakeExplosion(Vector3 velocity, float force)
    {
        if (instance.explosionShakeTime > 0f)
        {
            instance.explosionImpulseSource.m_ImpulseDefinition.m_ImpulseDuration = instance.explosionShakeTime;
            instance.explosionShakeTime = 0f;
        }
        else
        {
            instance.explosionImpulseSource.m_ImpulseDefinition.m_ImpulseDuration = instance.explosionShakeTimeDefault;
        }
        instance.explosionImpulseSource.m_DefaultVelocity = velocity;
        instance.explosionImpulseSource.GenerateImpulseWithForce(force);
    }

    public static void SetRecoilShakeTime(float time)
    {
        instance.recoilShakeTime = time;
    }

    public static void SetExplosionShakeTime(float time)
    {
        instance.explosionShakeTime = time;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.M))
        {
            ShakeExplosion(new Vector3(1, 0, 0), 0.3f);
        }
#endif
    }
}
