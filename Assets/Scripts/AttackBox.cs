using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public Vector2 boxSize = new Vector2(5f, 5f); // 矩形区域大小
    public LayerMask targetLayer; // 目标层
    public Entity entity_master;

    public List<Entity> entities = new();
    private void OnEnable()
    {
        entities.Clear();
        entity_master = GetComponentInParent<Entity>();
        if (entity_master == null)
        {
            entity_master = transform.parent.GetComponentInParent<Entity>();
        }
    }
    void Update()
    {

        Vector2 boxCenter = transform.position; // 以当前物体为检测中心

        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f, targetLayer);

        //处理区域内List<entity>
        if (hits.Length > 0)
        {
            foreach (Collider2D col in hits)
            {
                var e = col.gameObject.GetComponent<Entity>();
                if (e != null && e != entity_master && !entities.Contains(e))
                {
                    entity_master.Hurt(e);
                    entities.Add(e);
                }
            }
        }
    }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
    private void OnDisable()
    {
        entity_master = null;
        entities.Clear();
    }
}
