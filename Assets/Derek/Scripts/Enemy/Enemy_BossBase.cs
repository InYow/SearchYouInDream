using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_BossBase : Enemy
{
    public ExternalBehaviorTree FirstStageBehaviorTree;
    public ExternalBehaviorTree SecondStageBehaviorTree;

    public override void Start()
    {
        base.Start();
        SetBehaviorTree(FirstStageBehaviorTree);
    }
    
    public void EnterSecondStage()
    {
        SetBehaviorTree(SecondStageBehaviorTree);
    }
    
    private void SetBehaviorTree(ExternalBehaviorTree treeAsset)
    {
        if (treeAsset)
        {
            behaviourTree.ExternalBehavior = treeAsset;    
        }
    }
}
