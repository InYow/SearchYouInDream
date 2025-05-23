using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public bool legacy = true;

    public SpriteRenderer SR_Health;

    public SpriteRenderer SR_HealthGray;

    public Image Image_Health;

    public Entity entity;

    private void Update()
    {
        if (legacy)
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
        else
        {
            //healthBar
            Image_Health.fillAmount = entity.health / entity.health_Max;

            //healthGrayBar
            // Player player = entity as Player;
            // if (player != null)
            // {
            //     SR_HealthGray.size = new Vector2(player.healthGray / entity.health_Max, 0.1f);
            // }
        }
    }
}
