using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 击破Canvas : MonoBehaviour
{
    public static 击破Canvas Instance;
    private Animator _animator;
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

    public static void PlayAnimation()
    {
        Instance._animator.Play("前半段");
        Instance._animator.SetBool("continue", false);
    }

    public static void ContinueAnimation()
    {
        //Instance._animator.SetBool("continue", true);

        Instance._animator.Play("后半段");
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
