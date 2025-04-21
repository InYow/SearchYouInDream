using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Pathfinding;
using UnityEngine;

public class Enemy : Entity
{
    public BehaviorTree behaviourTree;
    public AIPath aiPath;
    public Transform target;
}
