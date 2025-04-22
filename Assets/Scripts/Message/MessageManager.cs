using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance;

    public event Action<Entity> OnBreakStun;

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

    public static void BreakStun(Entity entity)
    {
        if (entity == null || entity.gameObject == null)
        {
            Debug.LogWarning("尝试调用 BreakStun，但目标 Entity 已被销毁。");
            return;
        }
        if (entity.breakAttackTimes > 0)
        {
            entity.breakAttackTimes--;
            Instance.OnBreakStun?.Invoke(entity);
        }

    }
}
