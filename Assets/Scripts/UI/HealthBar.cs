using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    public bool legacy = true;
    public bool isPlayer = false;

    public SpriteRenderer SR_Health;

    public SpriteRenderer SR_HealthGray;

    private float amount;

    public Image Image_Health_Color;

    public Image Image_Health_Cover;

    public Color colorHealthAdd = Color.green;

    public Color colorHealthAddEnd = Color.white;

    public Color colorHealthLose = Color.red;

    public Color colorHealthLoseEnd = Color.gray;

    public float timeAdd;

    public float timeLose;

    public Entity entity;


    private void Update()
    {
        if (legacy)
        {
            //healthBar
            SR_Health.size = new Vector2(entity.health / entity.health_Max, 0.1f);

            //healthGrayBar
            Player player = entity as Player;
            // if (player != null)
            // {
            //     SR_HealthGray.size = new Vector2(player.healthGray / entity.health_Max, 0.1f);
            // }
        }
        // else if (!isPlayer)
        // {
        //     Image_Health_Color.fillAmount = entity.health / entity.health_Max;
        // }
        else
        {
            //healthBar
            float a = entity.health / entity.health_Max;

            if (a != amount)
            {
                //health increase
                if (a > amount)
                {
                    amount = a;
                    // Debug.Log("to be green");
                    Image_Health_Color.fillAmount = a;
                    // tween
                    Image_Health_Color.DOKill();
                    Image_Health_Cover.DOKill();
                    Image_Health_Color.color = colorHealthAdd;
                    Image_Health_Color.DOColor(colorHealthAddEnd, timeAdd)
                        .OnComplete(() => Image_Health_Cover.fillAmount = a);
                }
                //health decrease
                else
                {
                    amount = a;
                    // Debug.Log("to be red");
                    Image_Health_Cover.fillAmount = a;
                    // tween
                    Image_Health_Color.DOKill();
                    Image_Health_Cover.DOKill();
                    Image_Health_Color.color = colorHealthLose;
                    Image_Health_Color.DOColor(colorHealthLoseEnd, timeLose)
                        .OnComplete(() => Image_Health_Color.fillAmount = a);
                }
            }
        }
    }
}
