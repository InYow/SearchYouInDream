using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewRankUI : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();

    public Image image;

    private void Start()
    {
        RankSystem.rankSystem.ACIntoC += OpenGO_C;
        RankSystem.rankSystem.ACIntoB += OpenGO_B;
        RankSystem.rankSystem.ACIntoA += OpenGO_A;
    }

    private void Update()
    {
        image.fillAmount = RankSystem.GetCurrentRankHasValue() / RankSystem.GetRankVolumn(RankSystem.GetRank());
    }

    private void OpenGO_C()
    {
        image.sprite = sprites[0];
    }
    private void OpenGO_B()
    {
        image.sprite = sprites[1];
    }
    private void OpenGO_A()
    {
        image.sprite = sprites[2];
    }
}
