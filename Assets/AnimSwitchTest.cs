using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSwitchTest : MonoBehaviour
{
    public Animator animator;

    [Range(0,1)]public float weight = 0.0f;
    public int firstLayerIndex;
    public int secondLayerIndex;
    private void Start()
    {
        animator = GetComponent<Animator>();
        
        firstLayerIndex = animator.GetLayerIndex("FirstLayer");
        secondLayerIndex = animator.GetLayerIndex("SecondLayer");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetLayerWeight(firstLayerIndex,1.0f-weight);
        animator.SetLayerWeight(secondLayerIndex,weight);
    }
}
