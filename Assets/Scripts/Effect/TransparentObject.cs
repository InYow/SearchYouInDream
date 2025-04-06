using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TransparentObject : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;
    [SerializeField]private float transparencyDuration = 0.3f;
    [SerializeField]private float targetTransparency = 0.4f;
    private Color m_OriginalColor;
    private Coroutine m_TransparencyCoroutine;
    private Coroutine m_SolidCoroutine;
    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_OriginalColor = m_SpriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            if (m_SolidCoroutine != null)
            {
                StopCoroutine(m_SolidCoroutine);
            }
            m_TransparencyCoroutine = StartCoroutine(TransparentSprite());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            if (m_TransparencyCoroutine != null)
            {
                StopCoroutine(m_TransparencyCoroutine);
            }
            m_SolidCoroutine = StartCoroutine(SolidSprite());
        }
    }

    IEnumerator TransparentSprite()
    {
        if (m_SpriteRenderer == null)
        {
            yield return null;
        }

        float tick = 0;
        Color temp = m_SpriteRenderer.color;
        while (tick < transparencyDuration)
        {
            yield return null;
            tick += Time.deltaTime;
            float alpha = Mathf.Lerp(temp.a, targetTransparency, tick / transparencyDuration);
            m_SpriteRenderer.color = new Color(temp.r,temp.g,temp.b,alpha);
        }
    }
    
    IEnumerator SolidSprite()
    {
        if (m_SpriteRenderer == null)
        {
            yield return null;
        }

        float tick = 0;
        Color temp = m_SpriteRenderer.color;
        while (tick < transparencyDuration)
        {
            yield return null;
            tick += Time.deltaTime;
            float alpha = Mathf.Lerp(temp.a, m_OriginalColor.a, tick / transparencyDuration);
            m_SpriteRenderer.color = new Color(temp.r,temp.g,temp.b,alpha);
        }
    }
}
