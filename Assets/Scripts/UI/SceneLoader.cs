using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader m_instance;
    public static SceneLoader instance=>m_instance;
    
    private Coroutine fadeInCoroutine;
    private Coroutine fadeOutCoroutine;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        m_instance = this;
    }
    
    public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single /*Don't change this value*/)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName,mode));
    }

    IEnumerator LoadSceneCoroutine(string sceneName, LoadSceneMode mode)
    {
        //FadeIn
        if (fadeInCoroutine == null) { 
            fadeInCoroutine = StartCoroutine(FadeCanvas.instance.FadeIn());
        }
        
        while (FadeCanvas.instance.IsFadingIn)
        {
            yield return null;
        }
        fadeInCoroutine = null;
        
        //LoadScene
        var oprat = SceneManager.LoadSceneAsync(sceneName, mode);
        oprat.allowSceneActivation = false;
        while (oprat.progress < 0.9f)
        {
            yield return null;
        }
        oprat.allowSceneActivation = true;
        yield return new WaitUntil(() => oprat.isDone);
        
        //FadeOut
        if (fadeOutCoroutine == null)
        {
            fadeOutCoroutine = StartCoroutine(FadeCanvas.instance.FadeOut());
        }

        while (FadeCanvas.instance.IsFadingOut)
        {
            yield return null;
        }
        fadeOutCoroutine = null;
    }
}
