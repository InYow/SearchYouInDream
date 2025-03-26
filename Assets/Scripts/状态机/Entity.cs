using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float health;
    public float health_Max;
    public float resis;
    public float resis_Max;

    //状态机参数
    public bool transExecution;
    public Entity transExecution_DamageSourceEntity;
    public string transExecution_Type;
    public AttackBox transExecution_AttackBox;

    //buff集 <buff的名称，buff相关信息>
    public Dictionary<string, Buff> buffs = new();

    /// <summary>
    /// 耐力回复速度
    /// </summary>
    public float resis_ResponSpeed;

    public float attackValue;

    public bool transBreakStun;

    public bool beingBreakStun;

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
        //buff集帧更新
        UPBuffs();
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
    /// buff集帧更新
    /// </summary>
    public virtual void UPBuffs()
    {
        //字典遍历安全删除对象
        List<string> keysToRemove = new List<string>(); // 记录待删除的键
        foreach (var buff in buffs)
        {
            if (buff.Value == null) // 检查 Buff 是否被销毁
            {
                keysToRemove.Add(buff.Key); // 记录要删除的键
                continue; // 跳过 Update 调用
            }
            buff.Value.UpdateBuff(this);
        }
        // 删除所有标记的 Buff
        foreach (string key in keysToRemove)
        {
            buffs.Remove(key);
        }
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
    public virtual void Hurt(Entity entity, AttackBox attackBox)
    {
        entity.GetHurt(this, attackBox);
    }

    /// <summary>
    /// 被Entity伤害
    /// </summary>
    /// <param name="entity"></param>
    public virtual void GetHurt(Entity entity, AttackBox attackBox)
    {
        if (!BuffContain("BFPlayerUnselected"))
        {
            health -= entity.attackValue;

            //死掉了
            if (health <= 0f)
            {
                Execution(entity, attackBox);
            }
        }
    }

    /// <summary>
    /// 处决死亡
    /// </summary>
    public virtual void Execution(Entity entity, AttackBox attackBox)
    {
        if (attackBox.descrition == "")
        {
            transExecution = true;
            transExecution_Type = "fly";
            transExecution_DamageSourceEntity = entity;
            transExecution_AttackBox = attackBox;
        }
    }

    /// <summary>
    /// 降低耐性
    /// </summary>
    /// <param name="entity"></param>
    public virtual void DetectResis(Entity entity)
    {
        if (entity is Player player)
        {
            if (!beingBreakStun)
            {
                resis = Mathf.Clamp(resis - player.attackResisValue, 0, resis_Max);//降低耐性
                                                                                   //破防
                if (Mathf.Approximately(resis, 0f))
                {
                    StartBreakStun(player);
                }
            }
        }
    }

    /// <summary>
    /// 被破防
    /// </summary>
    /// <param name="entity"></param>
    public virtual void StartBreakStun(Entity entity)
    {
        transBreakStun = true;

        //将信息传递出去（player，UI提示）
        MessageManager.BreakStun(this);

    }

    //----------------方法-buff集-----------------

    /// <summary>
    /// buff集是否包含buff
    /// </summary>
    /// <param name="buffName"></param>
    /// <returns></returns>
    public bool BuffContain(string buffName)
    {
        return buffs.ContainsKey(buffName);
    }

    /// <summary>
    /// 获得已有buff
    /// </summary>
    /// <param name="buffName"></param>
    /// <returns></returns>
    public Buff BuffGet(string buffName)
    {
        buffs.TryGetValue(buffName, out Buff buff);
        return buff;
    }

    /// <summary>
    /// 添加buff
    /// </summary>
    /// <param name="buffName"></param>
    public Buff BuffAdd(string buffName)
    {
        buffs.TryGetValue(buffName, out Buff buff);
        //buff已存在
        if (buff != null)
        {
            buff.AddAgain(this);
            return buff;
        }
        //buff未存在
        else
        {
            GameObject buffPrbGO = Resources.Load<GameObject>("Prefabs/Buff/" + buffName);
            buff = Instantiate(buffPrbGO, transform).GetComponent<Buff>();//实例化一个buff
            //添加到buff集
            buffs.Add(buff.BuffName, buff);
            //生命周期-StartBuff()
            buff.StartBuff(this);
            return buff;
        }
    }

    /// <summary>
    /// 移除buff
    /// </summary>
    /// <param name="buffName"></param>
    public void BuffRemove(string buffName)
    {
        buffs.TryGetValue(buffName, out Buff buff);
        if (buff != null)
        {
            buffs.Remove(buffName);
            buff.FinishBuff(this);
        }
    }

    /// <summary>
    /// 翻转
    /// </summary>
    /// <param name="b"></param>
    public virtual void FlipX(bool b)
    {
        if (!b)
        {
            Vector3 v = gameObject.transform.localScale;
            v.x = 1f;
            gameObject.transform.localScale = v;
            //unscale
            v.x = 1f;
            transform.GetChild(0).localScale = v;
        }
        else if (b)
        {
            Vector3 v = gameObject.transform.localScale;
            v.x = -1f;
            gameObject.transform.localScale = v;
            //unscale
            v.x = -1f;
            transform.GetChild(0).localScale = v;
        }
    }
}
