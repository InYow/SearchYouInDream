using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnScale : MonoBehaviour
{
    public Transform unscaleTransform;
    private void Update()
    {
        if (unscaleTransform.lossyScale.x == -1)
        {
            unscaleTransform.localScale = new Vector3(-unscaleTransform.localScale.x, unscaleTransform.localScale.y, unscaleTransform.localScale.z);
        }
    }
}
