using UnityEngine;

public class 硬直时间显示 : MonoBehaviour
{
    public SpriteRenderer SR_Time;
    public GameObject renderGO;

    public Player player;

    private void Update()
    {
        if (player.time_Stun == 0f)
        {
            renderGO.SetActive(false);
        }
        else
        {
            renderGO.SetActive(true);
            SR_Time.size = new Vector2(player.time_Stun / player.time_StunMax, 0.1f);
        }
    }
}
