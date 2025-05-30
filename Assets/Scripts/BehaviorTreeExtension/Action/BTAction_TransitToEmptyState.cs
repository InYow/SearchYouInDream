using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorTreeExtension
{
    public class BTAction_TransitToEmptyState : Action
    {
        [Tooltip("角色默认状态标签")]
        public string emptyStateName = "STEmpty";

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
                m_Entity.StateCurrent = m_Entity.InstantiateState(emptyStateName); ;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (m_Entity != null)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}
