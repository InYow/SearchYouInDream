using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CheckBoxType
{
    attack = 0,
    pick,
    attack_throwitem,
    flash
}

public enum AttackType
{
    none = 0,
    poison
}

public enum CanBreakAttackType
{
    cannot = 0,
    can,
}

public class CheckBox : MonoBehaviour
{
    public CheckBoxType checkBoxType;
    private Entity entity_master;

    public Action<CheckBox, List<Entity>> OnHurtEntity;

    //Attack
    public Vector2 boxSize = new Vector2(5f, 5f); //矩形区域大小
    public LayerMask attackLayer;   //目标层
    public AttackType attacktype = AttackType.none;       //攻击类型描述    
    public List<Entity> entities = new();

    //Pick
    public LayerMask pickLayer;
    public bool picked;

    //PickableItem
    public ProjectileBase pickableItem_master;
    
    //Flash
    public UnityEvent OnHitEntity;//闪光类投射物触发回调

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
            pickableItem_master = GetComponentInParent<ProjectileBase>();
            entity_master = pickableItem_master.entity_master;
        }
        else if (checkBoxType == CheckBoxType.flash)
        {
            pickableItem_master = GetComponentInParent<ProjectileBase>();
            entity_master = pickableItem_master.entity_master;
        }

        OverlapBox();
    }

    void Update()
    {
        bool flowControl = OverlapBox();
        if (!flowControl)
        {
            return;
        }
    }

    private bool OverlapBox()
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

                        Debug.Log("伤害" + e.name + entity_master.stateCurrentName);


                        entity_master.Hurt(e, this);
                        entities.Add(e);
                    }
                }

                //调用外部方法
                if (entities.Count > 0)
                    OnHurtEntity?.Invoke(this, entities);
            }
        }
        else if (checkBoxType == CheckBoxType.pick)
        {
            if (picked)
                return false;
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
                        if (entity_master == null)
                            Debug.Log("entity_master是空的");
                        Debug.Log(entity_master.name + e.name + this.name);
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
        else if (checkBoxType == CheckBoxType.flash)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f, attackLayer);
            
            if (hits.Length > 0)
            {
                bool b = false; //只攻击一次
                foreach (Collider2D col in hits)
                {
                    var e = col.gameObject.GetComponent<Player>();
                    if (e != null && e != entity_master)
                    {
                        OnHitEntity.Invoke();
                        b = true;
                    }
                    if (b)
                    {
                        pickableItem_master.Stop();             //攻击飞行停止
                        //gameObject.SetActive(false);            //关闭攻击检测框
                    }
                }
            }
        }
        return true;
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
        else if (checkBoxType == CheckBoxType.flash)
        {
            OnHitEntity.RemoveAllListeners();
        }
    }
}
