using System;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class SequenceStepAction
{
    [LabelText("延迟（秒）")]
    public float delay = 0.5f;

    [LabelText("步骤说明")]
    public string description;

    // [LabelText("使用UnityEvent")]
    // public bool useUnityEvent;

    [LabelText("调用事件(UnityEvent)")]
    public UnityEvent onStep;

    // [LabelText("调用事件(ISequenceStep)")]
    // public ISequenceStep sequenceStep;
}
