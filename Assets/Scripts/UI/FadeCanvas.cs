using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{
    private static FadeCanvas m_instance;
    public static FadeCanvas instance=>m_instance;

    public CanvasGroup m_canvasGroup;
    public Image m_image;
    public float fadeInTime = 0.5f;
    public AnimationCurve m_fadeInCurve;
    public float fadeOutTime = 0.5f;
    public AnimationCurve m_fadeOutCurve;
    
    private bool m_isFadingIn = false;
    private bool m_isFadingOut = false;
    
    public bool IsFadingIn => m_isFadingIn;
    public bool IsFadingOut => m_isFadingOut;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        m_instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        CloseFadeCover();
    }

    public IEnumerator FadeIn()
    {
        m_isFadingIn = true;
        OpenFadeCover();
        float t = 0;
        while (t < fadeInTime)
        {
            float step = t / fadeInTime;
            float alpha = m_fadeInCurve.Evaluate(step);
            m_canvasGroup.alpha = alpha;
            t += Time.deltaTime;
            yield return null;
        }
        m_isFadingIn = false;
    }

    public IEnumerator FadeOut()
    {
        m_isFadingOut = true;
        float t = 0;
        while (t < fadeOutTime)
        {
            float step = t / fadeOutTime;
            float alpha = m_fadeOutCurve.Evaluate(step); ;
            m_canvasGroup.alpha = alpha;
            t += Time.deltaTime;
            yield return null;
        }
        CloseFadeCover();
        m_isFadingOut = false;
    }

    private void OpenFadeCover()
    {
        m_image.enabled = true;
        m_canvasGroup.blocksRaycasts = true;
        m_canvasGroup.interactable = true;
    }
    
    private void CloseFadeCover()
    {
        m_canvasGroup.alpha = 0f;
        m_image.enabled = false;
        m_canvasGroup.blocksRaycasts = false;
        m_canvasGroup.interactable = false;
    }
}
