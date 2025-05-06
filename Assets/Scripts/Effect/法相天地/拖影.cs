using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 拖影 : MonoBehaviour
{
    public SpriteRenderer originRenderer;
    public Transform originTransform;

    public SpriteRenderer copyRenderer;
    public Transform copyTransform;

    public List<Vector2> vector2s;
    public List<Sprite> spriteRenderers;
    public List<Vector3> scales;

    public int number;

    public float intervalTime;

    public float _intervalTime;

    private void Update()
    {
        if (_intervalTime > 0)
        {
            _intervalTime -= Time.deltaTime;
        }
        else
        {
            _intervalTime = intervalTime;
            LogInfo();
        }

        if (spriteRenderers.Count >= number)
            copyRenderer.sprite = spriteRenderers[0];
        if (vector2s.Count >= number)
            copyTransform.position = vector2s[0];
        if (scales.Count >= number)
            copyTransform.localScale = scales[0];
    }

    public void LogInfo()
    {
        vector2s.Add(originTransform.position);
        if (vector2s.Count > number)
        {
            vector2s.RemoveAt(0);
        }

        spriteRenderers.Add(originRenderer.sprite);
        if (spriteRenderers.Count > number)
        {
            spriteRenderers.RemoveAt(0);
        }

        scales.Add(originTransform.localScale);
        if (scales.Count > number)
        {
            scales.RemoveAt(0);
        }
    }

}
