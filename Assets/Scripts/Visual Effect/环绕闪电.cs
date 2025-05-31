using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 环绕闪电 : MonoBehaviour
{
    public Animator animator;
    private void Start()
    {
        RankSystem.rankSystem.ACIntoB += Open;
        RankSystem.rankSystem.ACIntoC += Close;
    }

    public void Open()
    {
        gameObject.SetActive(true);
        animator.Play("环绕闪电");
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
