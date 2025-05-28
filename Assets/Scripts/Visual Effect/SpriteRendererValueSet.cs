using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererValueSet : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public string sortingLayerName;
    public int sortingOrder;

    public void SetValue()
    {
        spriteRenderer.sortingLayerID = SortingLayer.NameToID(sortingLayerName); // Replace "Default" with your desired layer name
        spriteRenderer.sortingOrder = sortingOrder; // Replace 0 with your desired sorting order
    }

    private void OnValidate()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
