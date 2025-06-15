using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipXOnEnable : MonoBehaviour
{
    public bool flipXOnEnable;
    public SpriteRenderer sr;
    public bool Random;

    private void OnValidate()
    {
        if (!sr)
            sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        if (!flipXOnEnable || !sr)
            return;

        if (Random)
        {
            sr.flipX = UnityEngine.Random.Range(0, 2) == 0;
        }
        else
        {
            sr.flipX = true;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
