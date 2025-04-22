using System.Collections.Generic;
using System.Security.Cryptography;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShadowReceiveObject : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Transform povit;
    private bool isShadowReceive = false;
    private Color nonshadowColor;
    public float time = 1f;
    [ReadOnly]
    public float t;
    public Color shadowColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    public LayerMask shadowCastLayerMask = 1 << 10; // Assuming layer 8 is the shadow cast layer

    private void Awake()
    {
        if (isShadowReceive)
        {
            t = 0f;
        }
        else
        {
            t = time;
        }
    }

    private void Start()
    {
        nonshadowColor = spriteRenderer.color;
    }

    private void Update()
    {
        Vector2 point = povit.position;
        Collider2D[] colliders = Physics2D.OverlapPointAll(point, shadowCastLayerMask);

        isShadowReceive = false;
        foreach (var collider in colliders)
        {
            Debug.Log("Point overlaps with Trigger: " + collider.name);
            isShadowReceive = true;
            break;
        }

        if (isShadowReceive)
        {
            t = Mathf.Clamp(t - Time.deltaTime, 0f, time);
        }
        else
        {
            t = Mathf.Clamp(t + Time.deltaTime, 0f, time);
        }

        spriteRenderer.color = Color.Lerp(shadowColor, nonshadowColor, t / time);
    }

    private void OnValidate()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (spriteRenderer == null)
        {
            Debug.LogError($"spriteRenderer没有赋值", this);
        }

        if (povit == null)
        {
            povit = transform;
        }
        if (povit == null)
        {
            Debug.LogError($"povit没有赋值", this);
        }
    }
}