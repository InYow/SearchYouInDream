using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BTAction_RunOnceState : Action
{
    public SharedBool hasRun;
    
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
        if (hasRun.Value)
        {
            return;
        }
        
        if (m_Entity != null)
        {
            m_Entity.StateCurrent = m_Entity.InstantiateState(stateName); ;
        }
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        if (hasRun.Value)
        {
            return TaskStatus.Success;
        }
        
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
        if (!hasRun.Value)
        {
            hasRun.Value = true;
        }
        
        if (m_Entity != null)
        {
            m_Entity.StateCurrent = m_Entity.InstantiateState(defaultStateName);
        }
    }
}
