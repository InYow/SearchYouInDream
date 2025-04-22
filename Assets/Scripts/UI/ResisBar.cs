using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResisBar : MonoBehaviour
{
    public SpriteRenderer SR_Resis;

    public Entity entity;

    private void Update()
    {
        SR_Resis.size = new Vector2(1 - entity.resis / entity.resis_Max, 0.1f);
        if (entity.breakAttackTimes > 0 && entity.beingBreakStun)
        {
            SR_Resis.color = Color.yellow;
        }
        else
        {
            SR_Resis.color = Color.gray;
        }
    }
}
