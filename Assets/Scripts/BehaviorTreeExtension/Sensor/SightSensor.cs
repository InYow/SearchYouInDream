using System;
using BehaviorDesigner.Runtime;
using BehaviorTreeExtension.Sensor;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using UnityEngine;
using UnityEngine.Serialization;

public class SightSensor : SensorBase
{
    public float sightRange = 6;
    public float runRange = 7;
    public Transform player; //Fixme Later 
    public BehaviorTree behaviourTree;
    
    public bool hasDetectedPlayer = false;
    
    private void Awake()
    {
        if (!player)
        {
            player = FindAnyObjectByType<Player>().transform;    
        }
    }

    public void Update()
    {
        SensorFixedUpdate();
    }
    
    public override void SensorFixedUpdate()
    {
        if (player)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            behaviourTree.SetVariableValue("DistanceFromPlayer", distance);

            if (!hasDetectedPlayer && distance < sightRange)
            {
                hasDetectedPlayer = true;
                OnTargetDetect?.Invoke();

                behaviourTree.SetVariableValue("PlayerTransform", player);
                behaviourTree.SetVariableValue("bFoundPlayer", true);
            }
            else if (hasDetectedPlayer && distance > runRange)
            {
                hasDetectedPlayer = false;
                OnTargetLose?.Invoke();

                behaviourTree.SetVariableValue("PlayerTransform", null);
                behaviourTree.SetVariableValue("bFoundPlayer", false);
            }
        }
    }
}
