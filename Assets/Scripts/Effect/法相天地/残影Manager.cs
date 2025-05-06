using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 残影Manager : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    public void SetRender(bool b)
    {
        foreach (var sr in spriteRenderers)
        {
            sr.enabled = b;
        }
    }
}
