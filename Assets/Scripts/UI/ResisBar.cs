using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResisBar : MonoBehaviour
{
    public SpriteRenderer SR_Resis;

    public Entity entity;

    private void Update()
    {
        SR_Resis.size = new Vector2(entity.resis / entity.resis_Max, 0.1f);
    }
}
