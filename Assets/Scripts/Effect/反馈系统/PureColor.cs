using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PureColor : RankUpVisualEffect
{
    public static new PureColor instance;
    public float sustainTime;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
