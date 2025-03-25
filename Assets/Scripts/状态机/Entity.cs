using Sirenix.OdinInspector;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float health;
    public float health_Max;
    public float resis;
    public float resis_Max;
    public float attackValue;

    public string StartStateName = "STEmpty";// Resources/Prefabs/State中的名称

    /// <summary>
    /// 当前状态
    /// </summary>
    public virtual State StateCurrent
    {
        get
        {
            return stateCurrent;
        }
        set
        {
            if (stateCurrent != null)
                stateCurrent.StateExit(this);

            stateCurrent = value;
            stateCurrentName = value.GetType().Name;
            stateCurrent.StateStart(this);
        }
    }

    private State stateCurrent;

    [ReadOnly]
    public string stateCurrentName;

    public virtual void Start()
    {
        StateCurrent = InstantiateState(StartStateName);
    }

    /// <summary>
    /// 实例化状态(至transform.parent)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public virtual State InstantiateState(string str)
    {
        GameObject StatePrb = Resources.Load<GameObject>("Prefabs/State/" + str);
        GameObject go = Instantiate(StatePrb, transform);
        return go.GetComponent<State>();
    }

    public virtual void Update()
    {
        //-------------- 状态机 -----------------
        //帧初始化
        UPInit();
        //帧输入
        UPInput();
        //检测状态转换
        UPCheckStateTrans();
        //状态行为
        UPStateBehaviour();
    }

    /// <summary>
    /// 帧初始化
    /// </summary>
    public virtual void UPInit()
    {
        StateCurrent.UPStateInit(this);
    }

    /// <summary>
    /// 帧输入
    /// </summary>
    public virtual void UPInput()
    {

    }

    /// <summary>
    /// 帧检测状态转换
    /// </summary>
    public virtual void UPCheckStateTrans()
    {

    }

    /// <summary>
    /// 帧状态行为
    /// </summary>
    public virtual void UPStateBehaviour()
    {
        StateCurrent.UPStateBehaviour(this);
    }

    //---------------方法--------------

    /// <summary>
    /// 伤害Entity
    /// </summary>
    /// <param name="entity"></param>
    public virtual void Hurt(Entity entity)
    {
        entity.GetHurt(this);
    }

    /// <summary>
    /// 被Entity伤害
    /// </summary>
    /// <param name="entity"></param>
    public virtual void GetHurt(Entity entity)
    {
        health -= entity.attackValue;
    }
}
