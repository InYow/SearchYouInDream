using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public static Action<Entity> ACBreakStun;


    /// <summary>
    /// 有entity被破防了
    /// </summary>
    /// <param name="entity">target</param>
    public static void BreakStun(Entity entity)
    {
        ACBreakStun?.Invoke(entity);
    }
}
