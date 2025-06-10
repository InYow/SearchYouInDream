using System;
using UnityEngine;

public class FlashUIBase : MonoBehaviour
{
    public float flashDuration = 1.5f;
    
    private float startTime;
    private void OnEnable()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - startTime > flashDuration)
        {
            Destroy(gameObject);
        }
    }
}
