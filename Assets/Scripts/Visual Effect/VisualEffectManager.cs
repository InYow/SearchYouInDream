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
            GameObject effectInstance = Instantiate(effectPrefab, transform.position, transform.rotation);
            Vector3 vector3 = transform.localScale;
            vector3.x = transform.lossyScale.x;
            transform.localScale = vector3;
            //Debug.Log($"特效 {VFXName} 已生成，位置设置为 {transform.position}");
        }
        else
        {
            Debug.LogError("特效资源未找到: " + VFXName);
        }
    }
}
