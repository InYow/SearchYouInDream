using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Hit VFX")]
    public List<string> hitVFX_List = new List<string> { "受伤特效x" };
    public List<string> 血VFX_List = new List<string> { "血1" };
    public Transform hitVFX_Pivot;
    [Header("Camera")]
    public Transform camera_Pivot;
    [Header("Component")]
    public CheckBox attackBox;
    public Rigidbody2D _rb;
    [Header("Entity")]
    public float health;
    public float health_Max;
    public float resis;
    public float resis_Max;
    /// <summary>
    /// 耐力回复速度
    /// </summary>
    public float resis_ResponSpeed;
    public float attackValue;
    public int breakAttackTimes = 1;
    public int breakAttackTimes_Max = 1;

    //状态机参数
    public string StartStateName = "STEmpty";// Resources/Prefabs/State中的名称
    public bool transExecution;
    public Entity transExecution_DamageSourceEntity;
    public string transExecution_Type;
    public CheckBox transExecution_AttackBox;
    public CheckBoxBehaviour transExecution_AttackBoxBehaviour;

    public bool transBreakStun;
    public bool beingBreakStun;

    [Header("Buff")]
    //buff集 <buff的名称，buff相关信息>
    public Dictionary<string, Buff> buffs = new();

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
        breakAttackTimes = breakAttackTimes_Max;
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
        foreach (var key in buffs.Keys.ToList())
        {
            if (buffs[key] == null) // 检查 Buff 是否被销毁
            {
                keysToRemove.Add(key); // 记录要删除的键
                continue; // 跳过 Update 调用
            }
            buffs[key].UpdateBuff(this);
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

    [ContextMenu("OnValidate")]
    public virtual void OnValidate()
    {
        if (hitVFX_Pivot == null)
        {
            hitVFX_Pivot = transform.Find("HitVFX Pivot");
        }
        if (hitVFX_Pivot == null)
        {
            Debug.LogWarning($"{gameObject.name}没有HitVFX Pivot");
        }
        if (camera_Pivot == null)
        {
            camera_Pivot = transform.Find("Camera Pivot");
        }
        if (camera_Pivot == null)
        {
            Debug.LogWarning($"{gameObject.name}没有Camera Pivot");
        }

        attackBox = GetComponentInChildren<CheckBox>(true);
        if (attackBox == null)
        {
            Debug.LogWarning($"{gameObject.name}没有找到子物体中的CheckBox组件");
        }
    }

    /// <summary>
    /// 伤害Entity
    /// </summary>
    /// <param name="entity"></param>
    public virtual void Hurt(Entity entity, CheckBox attackBox)
    {
        entity.GetHurt(this, attackBox);
        //CameraShake.Shake(new Vector3(1, 0, 0), 0.3f);
    }
    public virtual void Hurt(Entity entity, CheckBoxBehaviour checkBoxBehaviour)
    {
        entity.GetHurt(this, checkBoxBehaviour);
        //CameraShake.Shake(new Vector3(1, 0, 0), 0.3f);
    }

    /// <summary>
    /// 被Entity伤害
    /// </summary>
    /// <param name="entity"></param>
    public virtual void GetHurt(Entity entity, CheckBox attackBox)
    {
        if (!BuffContain("BFPlayerUnselected"))
        {
            health -= entity.attackValue;
            GetHurtVFX();
            if (beingBreakStun)
                CameraShake.ShakeRecoil(new Vector3(1, 0, 0), 0.3f);

            //死掉了
            if (health <= 0f)
            {
                Execution(entity, attackBox);
            }
        }
    }
    public virtual void GetHurt(Entity entity, CheckBoxBehaviour checkBoxBehaviour)
    {
        if (!BuffContain("BFPlayerUnselected"))
        {
            health -= entity.attackValue;
            GetHurtVFX();
            if (beingBreakStun)
                CameraShake.ShakeRecoil(new Vector3(1, 0, 0), 0.3f);

            //死掉了
            if (health <= 0f)
            {
                Execution(entity, checkBoxBehaviour);
            }
        }
    }

    public virtual void GetHurtVFX()
    {
        if (血VFX_List.Count > 0)
        {
            // 随机选择一个特效
            int randomIndex = Random.Range(0, 血VFX_List.Count);
            string randomEffect = 血VFX_List[randomIndex];

            // 播放随机特效
            VisualEffectManager.PlayEffect(randomEffect, hitVFX_Pivot);
        }
        else
        {
            Debug.LogWarning("血VFX_List 为空，无法播放特效！");
        }

        if (hitVFX_List.Count > 0)
        {
            // 随机选择一个特效
            int randomIndex = Random.Range(0, hitVFX_List.Count);
            string randomEffect = hitVFX_List[randomIndex];

            // 播放随机特效
            VisualEffectManager.PlayEffect(randomEffect, hitVFX_Pivot);
        }
        else
        {
            Debug.LogWarning("hitVFX_List 为空，无法播放特效！");
        }
    }

    /// <summary>
    /// 处决死亡
    /// </summary>
    public virtual void Execution(Entity entity, CheckBox attackBox)
    {
        if (attackBox.attacktype == AttackType.none)
        {
            transExecution = true;
            var enemy = this as Enemy;
            if (enemy)
            {
                enemy.behaviourTree.SetVariableValue("bCanExecute", transExecution);
                enemy.isGetHurt = false;
                enemy.behaviourTree.SetVariableValue("bIsGetHurt", enemy.isGetHurt);
            }
            transExecution_Type = "fly";
            transExecution_DamageSourceEntity = entity;
            transExecution_AttackBox = attackBox;
        }
    }
    public virtual void Execution(Entity entity, CheckBoxBehaviour checkBoxBehaviour)
    {
        if (checkBoxBehaviour.attacktype == AttackType.none)
        {
            transExecution = true;
            var enemy = this as Enemy;
            if (enemy)
            {
                enemy.behaviourTree.SetVariableValue("bCanExecute", transExecution);
                enemy.isGetHurt = false;
                enemy.behaviourTree.SetVariableValue("bIsGetHurt", enemy.isGetHurt);
            }
            transExecution_Type = "fly";
            transExecution_DamageSourceEntity = entity;
            transExecution_AttackBoxBehaviour = checkBoxBehaviour;
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
                if (Mathf.Approximately(resis, 0f) && health > 0f && breakAttackTimes > 0)
                {
                    StartBreakStun(player);
                }
            }
        }
    }
    public virtual void DetectResis(Entity entity, CheckBoxBehaviour checkBoxBehaviour)
    {
        if (entity is Player player)
        {
            if (!beingBreakStun)
            {
                resis = Mathf.Clamp(resis - player.attackResisValue, 0, resis_Max);//降低耐性
                //破防
                if (Mathf.Approximately(resis, 0f) && health > 0f && breakAttackTimes > 0)
                {
                    StartBreakStun(player);

                    //击破攻击
                    if (checkBoxBehaviour.canBreakAttackType == CanBreakAttackType.can && this is Enemy)
                    {
                        MessageManager.CallBreakStun(this);
                    }
                }
            }
            else
            {
                //击破攻击
                if (checkBoxBehaviour.canBreakAttackType == CanBreakAttackType.can && this is Enemy)
                {
                    MessageManager.CallBreakStun(this);
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
        var enemy = this as Enemy;
        if (enemy)
        {
            enemy.behaviourTree.SetVariableValue("bStun", enemy.transBreakStun);
        }
    }

    public virtual void ExitBreakStun()
    {
        breakAttackTimes = breakAttackTimes_Max;
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

    public virtual Vector2 GetFlipX()
    {
        if (gameObject.transform.localScale.x == 1f)
        {
            return Vector2.right;
        }
        else if (gameObject.transform.localScale.x == -1f)
        {
            return Vector2.left;
        }
        return Vector2.right;
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
}
