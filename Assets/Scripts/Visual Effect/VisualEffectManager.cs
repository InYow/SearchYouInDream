using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectManager : MonoBehaviour
{
    public static VisualEffectManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void PlayEffect(string VFXName, Transform transform)
    {
        GameObject effectPrefab = Resources.Load<GameObject>("Prefabs/VFX/" + VFXName);
        if (effectPrefab != null)
        {
            GameObject effectInstance = Instantiate(effectPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        }
        else
        {
            Debug.LogError("特效资源未找到: " + VFXName);
        }
    }

    public static void PlayEffectWithoutRotation(string VFXName, Transform transform)
    {
        GameObject effectPrefab = Resources.Load<GameObject>("Prefabs/VFX/" + VFXName);
        if (effectPrefab != null)
        {
            GameObject effectInstance = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("特效资源未找到: " + VFXName);
        }
    }
}
