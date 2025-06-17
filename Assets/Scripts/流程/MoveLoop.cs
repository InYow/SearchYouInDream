using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveLoop : MonoBehaviour
{
    private Tween tween;
    public Vector3 from;
    public Vector3 to;
    public float time;

    private void OnEnable()
    {
        tween = transform.DOMove(to, time).From(from).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        tween.Kill();
    }
}
