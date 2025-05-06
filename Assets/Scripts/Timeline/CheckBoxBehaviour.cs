using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class CheckBoxBehaviour : PlayableBehaviour
{
    public Vector2 boxSize = new Vector2(1f, 0.5f); //矩形区域大小
    public Vector2 boxOffset = new Vector2(0f, 0f);
    public LayerMask attackLayer = 1 << 6;   //目标层
    public AttackType attacktype = AttackType.none;       //攻击类型描述    
    public CanBreakAttackType canBreakAttackType = CanBreakAttackType.cannot; //打断眩晕类型
    public Entity entity_master;


    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);

        if (Application.isPlaying)
        {
            // 运行时模式的逻辑
            OnBehaviourPlay_Runtime(playable, info);
        }
        else
        {
            // 编辑器模式的逻辑
            OnBehaviourPlay_Editor(playable, info);
        }
    }

    /// <summary>
    /// 在运行时模式中执行的逻辑
    /// </summary>
    private void OnBehaviourPlay_Runtime(Playable playable, FrameData info)
    {
        Vector2 boxCenter = (Vector2)entity_master.transform.position + new Vector2(boxOffset.x * entity_master.GetFlipX().x, boxOffset.y);
        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f, attackLayer);
        // 绘制检测区域的线框，仅用于可视化
        DrawWireCube.SetWireCubePropty(boxCenter, boxSize);

        // 处理区域内的 List<Entity>
        if (hits.Length > 0)
        {
            foreach (Collider2D col in hits)
            {
                var e = col.gameObject.GetComponent<Entity>();
                if (e != null && e != entity_master)
                {
                    Debug.Log("伤害" + e.name + entity_master.stateCurrentName);
                    entity_master.Hurt(e, this);

                    RankSystem.Attack(); //增加评值
                }
            }
        }
    }

    /// <summary>
    /// 在编辑器模式中执行的逻辑
    /// </summary>
    private void OnBehaviourPlay_Editor(Playable playable, FrameData info)
    {
        Vector2 boxCenter = (Vector2)entity_master.transform.position + new Vector2(boxOffset.x * entity_master.GetFlipX().x, boxOffset.y);

        // 绘制检测区域的线框，仅用于可视化
        DrawWireCube.SetWireCubePropty(boxCenter, boxSize);
        Debug.Log("编辑器模式下的 OnBehaviourPlay 被调用，绘制检测区域。");
    }


    public override void OnPlayableCreate(Playable playable)
    {
        base.OnPlayableCreate(playable);
        GameObject ownerGameObject = null;
        if (playable.GetGraph().GetResolver() is PlayableDirector director)
        {
            ownerGameObject = director.gameObject;
        }
        entity_master = ownerGameObject.transform.GetComponentInParent<Entity>();
        if (entity_master == null)
        {
            Debug.LogError("entity_master is null, please check the entity master in the inspector.", ownerGameObject);
        }
    }
}
