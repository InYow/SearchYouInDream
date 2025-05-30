using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorTreeExtension
{
    public class BTAction_TransitState : Action
    {
        [Tooltip("角色状态标签")]
        public string stateName;

        public string defaultStateName = "STEmpty";

        private Enemy m_Entity;

        public override void OnAwake()
        {
            base.OnAwake();
            m_Entity = gameObject.GetComponent<Enemy>();
        }

        public override void OnStart()
        {
            if (m_Entity != null)
            {
                m_Entity.StateCurrent = m_Entity.InstantiateState(stateName); ;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (m_Entity.StateCurrent.Finished(m_Entity))
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Running;
            }
        }

        public override void OnEnd()
        {
            if (m_Entity != null)
            {
                m_Entity.StateCurrent = m_Entity.InstantiateState(defaultStateName);
            }
        }
    }
}
