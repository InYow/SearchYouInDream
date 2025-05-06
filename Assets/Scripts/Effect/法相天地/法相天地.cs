using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 法相天地 : MonoBehaviour
{
    public SpriteRenderer originRenderer;
    public SpriteRenderer copyRenderer;
    void Update()
    {
        copyRenderer.sprite = originRenderer.sprite;
    }

    private void OnValidate()
    {
        copyRenderer.sprite = originRenderer.sprite;
    }
}
