using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System;

[Serializable]
public class SequenceStep
{
    [LabelText("延迟（秒）")]
    public float delay = 0.5f;

    [LabelText("步骤说明")]
    public string description;

    [LabelText("调用事件")]
    public UnityEvent onStep;
}

public class VisualSequence : MonoBehaviour
{
    [LabelText("步骤序列")]
    [ListDrawerSettings(ShowFoldout = true)]
    public List<SequenceStep> steps = new();

    private Coroutine sequenceRoutine;
    private bool isPaused = false;
    private int currentStepIndex = 0;

    [Button("执行流程"), EnableIf("@UnityEngine.Application.isPlaying")]
    public void Play()
    {
        Stop(); // 防止重复启动
        sequenceRoutine = StartCoroutine(PlaySequence());
    }

    [Button("暂停流程"), EnableIf("@UnityEngine.Application.isPlaying")]
    public void Pause()
    {
        isPaused = true;
    }

    [Button("继续流程"), EnableIf("@UnityEngine.Application.isPlaying")]
    public void Resume()
    {
        isPaused = false;
    }

    [Button("停止流程"), EnableIf("@UnityEngine.Application.isPlaying")]
    public void Stop()
    {
        if (sequenceRoutine != null)
        {
            StopCoroutine(sequenceRoutine);
            sequenceRoutine = null;
        }
        isPaused = false;
        currentStepIndex = 0;
    }

    private IEnumerator PlaySequence()
    {
        for (currentStepIndex = 0; currentStepIndex < steps.Count; currentStepIndex++)
        {
            SequenceStep step = steps[currentStepIndex];

            // 延迟前暂停检测
            float timer = 0f;
            while (timer < step.delay)
            {
                if (!isPaused)
                    timer += Time.unscaledDeltaTime;

                yield return null;
            }

            // 执行事件
            step.onStep?.Invoke();

            // 暂停执行前，等下一帧
            yield return null;

            // 等待直到解除暂停
            while (isPaused)
            {
                yield return null;
            }
        }

        sequenceRoutine = null;
        Debug.Log("流程完成");
    }
}
