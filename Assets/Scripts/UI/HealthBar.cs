using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public SpriteRenderer SR_Health;

    public SpriteRenderer SR_HealthGray;

    public Entity entity;

    private void Update()
    {
        //healthBar
        SR_Health.size = new Vector2(entity.health / entity.health_Max, 0.1f);

        //healthGrayBar
        Player player = entity as Player;
        if (player != null)
        {
            SR_HealthGray.size = new Vector2(player.healthGray / entity.health_Max, 0.1f);
        }
    }
}
