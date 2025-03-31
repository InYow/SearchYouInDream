using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CheckBoxType
{
    attack = 0,
    pick,
    attack_throwitem,
}

public class CheckBox : MonoBehaviour
{
    public CheckBoxType checkBoxType;
    public Entity entity_master;
    [Header("attacktype")]
    public Vector2 boxSize = new Vector2(5f, 5f); //矩形区域大小
    public LayerMask attackLayer;   //目标层
    public string descrition;       //攻击类型描述
    public List<Entity> entities = new();
    [Header("picktype")]
    public LayerMask pickLayer;
    public bool picked;
    [Header("attack_throwitem")]
    public PickableItem pickableItem_master;
    private void OnEnable()
    {
        //初始化值
        //通用
        entity_master = GetComponentInParent<Entity>();
        //attack
        if (checkBoxType == CheckBoxType.attack)
        {
            entities.Clear();
            if (entity_master == null)
            {
                entity_master = transform.parent.GetComponentInParent<Entity>();
            }
        }
        //pick
        else if (checkBoxType == CheckBoxType.pick)
        {
            picked = false;
        }
        //attack_throwitem
        else if (checkBoxType == CheckBoxType.attack_throwitem)
        {
            pickableItem_master = GetComponentInParent<PickableItem>();
            entity_master = pickableItem_master.entity_master;
        }
    }

    void Update()
    {
        Vector2 boxCenter = transform.position; // 以当前物体为检测中心

        if (checkBoxType == CheckBoxType.attack)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f, attackLayer);
            //处理区域内List<entity>
            if (hits.Length > 0)
            {
                foreach (Collider2D col in hits)
                {
                    var e = col.gameObject.GetComponent<Entity>();
                    if (e != null && e != entity_master && !entities.Contains(e))
                    {
                        entity_master.Hurt(e, this);
                        entities.Add(e);
                    }
                }
            }
        }
        else if (checkBoxType == CheckBoxType.pick)
        {
            if (picked)
                return;
            Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f, pickLayer);
            //处理区域PickableItem
            if (hits.Length > 0)
            {
                foreach (Collider2D col in hits)
                {
                    var i = col.gameObject.GetComponent<PickableItem>();
                    if (i != null)
                    {
                        i.Picked(entity_master);
                        picked = true;
                        break;
                    }
                }
            }
        }
        else if (checkBoxType == CheckBoxType.attack_throwitem)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f, attackLayer);
            //处理区域内List<entity>
            if (hits.Length > 0)
            {
                bool b = false; //只攻击一次
                foreach (Collider2D col in hits)
                {
                    var e = col.gameObject.GetComponent<Entity>();
                    if (e != null && e != entity_master)
                    {
                        entity_master.Hurt(e, this);
                        b = true;
                    }
                }
                if (b)
                {
                    pickableItem_master.Stop();             //攻击飞行停止
                    gameObject.SetActive(false);            //关闭攻击检测框
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
        //清空值
        //通用
        entity_master = null;
        //attack
        if (checkBoxType == CheckBoxType.attack)
        {
            entities.Clear();
        }
        //pick
        else if (checkBoxType == CheckBoxType.pick)
        {
            picked = false;
        }
        //attack_throwitem
        else if (checkBoxType == CheckBoxType.attack_throwitem)
        {
            pickableItem_master = null;
            entity_master = null;
        }
    }
}
