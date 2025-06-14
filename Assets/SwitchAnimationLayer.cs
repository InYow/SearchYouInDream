using System;
using UnityEngine;

public class SwitchAnimationLayer : MonoBehaviour
{
    public Animator animator;
    
    public int firstLayerIndex;
    public int secondLayerIndex;
    
    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        
        firstLayerIndex = animator.GetLayerIndex("FirstLayer");
        secondLayerIndex = animator.GetLayerIndex("SecondLayer");
    }

    public void SwitchAnimation()
    {
        animator.SetLayerWeight(firstLayerIndex,0.0f);
        animator.SetLayerWeight(secondLayerIndex,1.0f);
    }
}
