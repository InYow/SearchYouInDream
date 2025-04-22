using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class DrawWireCube : MonoBehaviour
{
    private static DrawWireCube Instance;
    [ReadOnly]
    public DrawWireCube instance;
    public Vector2 center;
    public Vector2 size;

    private void OnValidate()
    {
        if (Instance == null)
        {
            Instance = this;
            instance = Instance;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    public static void SetWireCubePropty(Vector2 center, Vector2 size)
    {
        Instance.center = center;
        Instance.size = size;
    }
}
