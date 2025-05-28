using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum VisualEffectFinishType
{
    Destroy,
    Disable,
    Sustain,
}
public class VisualEffect : MonoBehaviour
{
    public VisualEffectFinishType finishType = VisualEffectFinishType.Destroy;
    public Animator _animator;
    public UnityEvent finishEvent;
    private void OnValidate()
    {
        //
        _animator = GetComponent<Animator>();
        if (_animator == null)
            _animator = GetComponentInChildren<Animator>(true);
        if (_animator == null)
            Debug.LogError("没有找到Animator组件，请检查！", this);
        //

    }
    private void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Finish();
            finishEvent?.Invoke();
        }
    }
    private void Finish()
    {
        //这里可以根据需要选择销毁或者禁用
        switch (finishType)
        {
            case VisualEffectFinishType.Destroy:
                {
                    Destroy(gameObject);
                    break;
                }
            case VisualEffectFinishType.Disable:
                {
                    gameObject.SetActive(false);
                    break;
                }
            case VisualEffectFinishType.Sustain:
                {
                    //不做任何操作
                    break;
                }
            default:
                goto case VisualEffectFinishType.Destroy;
        }
    }
}
