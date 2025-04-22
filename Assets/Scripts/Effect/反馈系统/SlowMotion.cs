using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public static SlowMotion instance;

    [Header("设置")]
    public float DefaultSlowTime;       //默认慢放时长
    public float DefaultSlowFactor;     //默认慢放系数

    [Header("变量")]
    public float slowTime;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void Update()
    {
        //慢放结束
        if (slowTime <= 0f)
        {
            FinishSlow();
        }
        //流逝慢放时间
        else
        {
            slowTime -= Time.unscaledDeltaTime;
        }
    }

    /// <summary>
    /// 慢放开始
    /// </summary>
    public static void StartSlow()
    {
        StartSlow(instance.DefaultSlowTime, instance.DefaultSlowFactor);
    }


    /// <summary>
    /// 慢放开始
    /// </summary>
    public static void StartSlow(float time)
    {
        StartSlow(time, instance.DefaultSlowFactor);
    }


    /// <summary>
    /// 慢放开始
    /// </summary>
    public static void StartSlow(float time, float factor)
    {
        instance.slowTime = time;
        Time.timeScale = factor;
    }

    /// <summary>
    /// 慢放结束
    /// </summary>
    public static void FinishSlow()
    {
        instance.slowTime = 0f;
        Time.timeScale = 1f;
    }

}
