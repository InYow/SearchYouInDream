using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMotion : MonoBehaviour
{
    public static KillMotion Instance;

    [Header("击杀时刻")]
    public float slowMotionDelay;
    public float executeDuration;
    public float executeBackTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
