using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Pathfinding;
using UnityEngine;

public class Enemy : Entity
{
    public Rigidbody2D _rb;
    public BehaviorTree behaviourTree;
    public AIPath aiPath;
}
