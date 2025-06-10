using System.Collections;
using NodeCanvas.Tasks.Actions;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FlashUIBase : MonoBehaviour
{
    public float flashDuration = 1.5f;
    public Sprite[] flashSprites;
    public float fadeInDuration = 0.3f;
    public AnimationCurve flashFadeInCurve;
    public float fadeOutDuration = 0.5f;
    public AnimationCurve flashFadeOutCurve;
    
    private float startTime;
    private Image flashImage;
    private CanvasGroup canvasGroup;
    private bool startFlash;
    private void OnEnable()
    {
        flashImage = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        startFlash = false;
        SetRandomFlashCover();
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        if (startFlash && Time.time - startTime > flashDuration)
        {
            StartCoroutine(FadeOut());
        }
    }

    private void SetRandomFlashCover()
    {
        if (flashImage)
        {
            flashImage.sprite = flashSprites[Random.Range(0,flashSprites.Length)];
            int xScale = Random.Range(0,2)*2-1;
            int yScale = Random.Range(0,2)*2-1;
            transform.localScale = new Vector3(xScale, yScale, 1);
        }
    }

    private IEnumerator FadeIn()
    {
        float t = 0;
        while (t<fadeInDuration)
        {
            float step = t / fadeInDuration;
            float alpha = flashFadeInCurve.Evaluate(step);
            canvasGroup.alpha = alpha;
            t += Time.deltaTime;
            yield return null;
        }
        startTime = Time.time;
        startFlash = true;
    }
    
    private IEnumerator FadeOut()
    {
        float t = 0;
        while (t<fadeOutDuration)
        {
            float step = t / fadeOutDuration;
            float alpha = flashFadeOutCurve.Evaluate(step);;
            canvasGroup.alpha = alpha;
            t += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
