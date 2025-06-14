using System;
using Pathfinding;
using UnityEngine;

public class SwitchAnimationLayer : MonoBehaviour
{
    public Animator animator;
    public AIPath aiPath;
    public float SecondStateSpeed;
    public int firstLayerIndex;
    public int secondLayerIndex;
    
    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        aiPath = GetComponentInParent<AIPath>();
        
        firstLayerIndex = animator.GetLayerIndex("FirstLayer");
        secondLayerIndex = animator.GetLayerIndex("SecondLayer");
    }

    public void SwitchAnimation()
    {
        animator.SetLayerWeight(firstLayerIndex,0.0f);
        animator.SetLayerWeight(secondLayerIndex,1.0f);
        aiPath.maxSpeed = SecondStateSpeed;
    }
}
