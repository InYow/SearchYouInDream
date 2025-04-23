using System;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

public class SightSensor : MonoBehaviour,IBehaviorSensor
{
    public float sightRange = 6;
    public float runRange = 7;
    public Transform player; //Fixme Later 
    public BehaviorTree behaviourTree;

    private void Awake()
    {
        if (!player)
        {
            player = FindAnyObjectByType<Player>().transform;    
        }
    }

    public void FixedUpdate()
    {
        SensorFixedUpdate();
    }
    
    public void SensorFixedUpdate()
    {
        if (player)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            behaviourTree.SetVariableValue("DistanceFromPlayer", distance);
            if (distance < sightRange)
            {
                behaviourTree.SetVariableValue("PlayerTransform", player);
                behaviourTree.SetVariableValue("bFoundPlayer", true);
            }
            if(distance > runRange)
            {
                behaviourTree.SetVariableValue("PlayerTransform", null);
                behaviourTree.SetVariableValue("bFoundPlayer", false);
            }
        }
    }
}
