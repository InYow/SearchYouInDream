using UnityEngine;
using UnityEngine.UI;

public class ResisBar : MonoBehaviour
{
    public bool legacy = true;
    public SpriteRenderer SR_Resis;
    public Image Image_Resis;
    public Entity entity;

    private void Update()
    {
        if (legacy)
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
        else
        {

            Image_Resis.fillAmount = 1 - entity.resis / entity.resis_Max;
        }
    }
}
