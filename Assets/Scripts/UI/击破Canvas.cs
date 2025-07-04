using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 击破Canvas : MonoBehaviour
{
    public static 击破Canvas Instance;
    private Animator _animator;
    public RadialBlurData radialBlurData;

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
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
    }

    public static void ReSetAnimation()
    {
        Instance._animator.Play("无");
        Instance._animator.SetBool("continue", false);
    }

    public static void PlayAnimation()
    {
        Instance._animator.Play("前半段");
        Instance._animator.SetBool("continue", false);
    }

    public static void ContinueAnimation()
    {
        //Instance._animator.SetBool("continue", true);

        Instance._animator.Play("后半段");

        // SFX
        SoundManager_New.Play("面部特写");
        // SoundManager_New.Get("击破")[0].Stop();

        //开启径向模糊
        RadialBlur.RadialBlurUseData(Instance.radialBlurData);
        //        Debug.LogError("测试");
    }

    public static bool IfFinish前半段()
    {
        return Instance._animator.GetCurrentAnimatorStateInfo(0).IsName("前半段") &&
             Instance._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }

    public static bool IfFinish后半段()
    {
        return Instance._animator.GetCurrentAnimatorStateInfo(0).IsName("后半段") &&
             Instance._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }
}
