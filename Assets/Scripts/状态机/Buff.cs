using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---作用---
//-buff持续时间
//-buff效果（产生效果，消失效果）
//-供查找（entity上有没有特定buff）
//-实例化与销毁
public class Buff : MonoBehaviour
{
    public string BuffName
    {
        get
        {
            return GetType().Name;
        }
    }
    public float time;  //buff时间
    public float time_Max;//buff最大时间
    public int layer;   //buff层数

    /// <summary>
    /// buff的开始
    /// </summary>
    /// <param name="entity"></param>
    public virtual void StartBuff(Entity entity)
    {
        time = time_Max;
    }

    /// <summary>
    /// buff的帧更新
    /// </summary>
    /// <param name="entity"></param>
    public virtual void UpdateBuff(Entity entity)
    {
        //流逝时间
        if (time <= 0f)
        {
            FinishBuff(entity);
        }
        else
        {
            time = Mathf.Clamp(time - Time.deltaTime, 0f, time);
        }
    }

    /// <summary>
    /// buff的结束
    /// </summary>
    /// <param name="entity"></param>
    public virtual void FinishBuff(Entity entity)
    {
        Destroy(gameObject);
        time = 0f;
    }

    //-----------方法------------

    /// <summary>
    /// 又被添加了一遍
    /// </summary>
    /// <param name="entity"></param>
    public virtual void AddAgain(Entity entity)
    {
        time = time_Max;
    }
}
