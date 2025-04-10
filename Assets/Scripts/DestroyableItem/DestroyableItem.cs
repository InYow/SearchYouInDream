using System;
using System.Collections;
using System.Collections.Generic;
using ParadoxNotion;
using UnityEngine;

/// <summary>
/// 挂载该组件的物体在启用/生成时开启A*寻路碰撞
/// 在禁用/销毁时关闭A*寻路碰撞
/// </summary>
public class DestroyableItem : MonoBehaviour,IDestroyableItem
{
    private Collider2D collision;
    public LayerMask collisionLayer;
    //public LayerMask defaultLayer;

    private void OnEnable()
    {
        collision = gameObject.GetComponent<Collider2D>();
        EnableItem();
    }

    private void OnDisable()
    {
        DestroyItem();
    }

    /// <summary>
    /// 启用寻路障碍
    /// </summary>
    public void EnableItem()
    {
        if (collision == null) return;
        
        gameObject.layer = LayerMask.NameToLayer(collisionLayer.MaskToString());
        AstarPath.active.UpdateGraphs(collision.bounds);
    }
    
    /// <summary>
    /// 禁用寻路障碍
    /// </summary>
    public void DestroyItem()
    {
        if (collision == null) return;
        
        gameObject.layer = 0 << gameObject.layer;
        AstarPath.active?.UpdateGraphs(collision.bounds);
    }
}
