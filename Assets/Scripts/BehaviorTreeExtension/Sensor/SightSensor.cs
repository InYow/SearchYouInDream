using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

public class SightSensor : MonoBehaviour,IBehaviorSensor
{
    public float sightRange;
    public Transform player; //Fixme Later 
    public BehaviorTree behaviourTree;
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
            else
            {
                behaviourTree.SetVariableValue("PlayerTransform", null);
                behaviourTree.SetVariableValue("bFoundPlayer", false);
            }
        }
    }
}
