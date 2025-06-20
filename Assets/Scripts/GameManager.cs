using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;
    public float freezeDelay;   //死亡后，等待多久后开始静止时间
    public float fadeInTime;
    public Image image;
    public Button continueBtn;
    public Image btnImage;
    public float btnFadeInTime;
    public GameObject ContinueCanvas;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Q))
        {
            SceneLoader.instance.LoadScene("开始界面");
        }
    }

    public void GameOver()
    {
        // 使用 DOTween 等待 freezeDelay 秒后执行特定方法
        DOVirtual.DelayedCall(freezeDelay, () =>
        {
            SlowMotion.StartSlow(9999f, 0f);
            GameOverLogic();
        });
    }

    private void GameOverLogic()
    {
        ContinueCanvas.SetActive(true);
        continueBtn.interactable = false;   //禁用按钮
        btnImage.color = new Color(1f, 1f, 1f, 0f);
        //黑色遮罩出现
        image.color = new(1f, 1f, 1f, 0f);
        image.DOColor(Color.white, fadeInTime)
            .From(new Color(1f, 1f, 1f, 0f))
            .SetUpdate(true)
            .OnComplete
            (
                () =>
                {
                    //按钮出现 
                    btnImage.DOColor(Color.white, btnFadeInTime)
                    .SetUpdate(true)
                    .From(new Color(1f, 1f, 1f, 0f))
                    .SetLoops(-1, LoopType.Restart);
                    continueBtn.interactable = true;    //启用按钮
                }
            );
    }

    public void GameContinue()
    {
        image.DOKill();
        btnImage.DOKill();
        btnImage.color = Color.white;
        ContinueCanvas.SetActive(false);
        player.dead = false;
        player.StateCurrent = player.InstantiateState("Player_复活");
        player.health = player.health_Max;
    }
}
