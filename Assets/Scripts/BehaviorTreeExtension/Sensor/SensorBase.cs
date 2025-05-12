using System;
using UnityEngine;

namespace BehaviorTreeExtension.Sensor
{
    public class SensorBase : MonoBehaviour,IBehaviorSensor
    {
        public Action OnTargetDetect;
        public Action OnTargetLose;
        protected bool isFirstDetect = true;
        
        public virtual void SensorFixedUpdate()
        {
            
        }
    }
}