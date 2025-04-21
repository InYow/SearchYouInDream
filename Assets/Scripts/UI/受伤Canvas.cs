using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 受伤Canvas : MonoBehaviour
{
    public static 受伤Canvas instance;
    public AnimationCurve animationCurve;
    public Image image;
    public float t = 0f;
    public bool isPlaying = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        t = 0f;
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying == false)
        {
            return;
        }
        Color color = image.color;
        color.a = animationCurve.Evaluate(t);
        image.color = color;
        t += Time.deltaTime;
        if (t > animationCurve.keys[animationCurve.length - 1].time)
        {
            t = 0f;
            isPlaying = false;
            color.a = 0f;
            image.color = color;
        }
    }

    public void PlayAnimation()
    {
        t = 0f;
        isPlaying = true;
    }
}
