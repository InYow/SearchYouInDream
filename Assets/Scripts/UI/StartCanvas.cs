using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCanvas : MonoBehaviour
{
    private Coroutine fadeInCoroutine;
    private Coroutine fadeOutCoroutine;

    public void LoadPlayScene()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    IEnumerator LoadSceneCoroutine()
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
        var oprat = SceneManager.LoadSceneAsync("功能开发场景-美术勿动", LoadSceneMode.Single);
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
