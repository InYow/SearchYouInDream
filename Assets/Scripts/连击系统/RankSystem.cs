using System;
using System.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//你别说, AI比我还能编
public enum Rank
{
    //F = 0,  //  Fear（恐惧）	你的动作畏畏缩缩，像个猎物             
    E,      //	Erosion（侵蚀）	你开始适应暴力，但仍然犹豫                              //正常  
    D,      //	Daze（恍惚）	你在杀戮中摇摆不定，手感迟钝    
    C,      //	Carnage（屠戮）	你变得越来越顺畅，攻击带着野性          
    B,      //	Brutal（暴虐）	你的战斗开始变得暴力，节奏加快                          //高等
    A,      //	Anarchy（无序）	你放弃防守，全力进攻
    S,      //	Savage（狂暴）	你的攻击已经无可阻挡，纯粹的碾碎敌人    
    //SS,     //  SchiSm（精神裂变）	你已经超越了战斗本能，攻击带有扭曲的癫狂
    O,      // Ø	Oblivion（湮灭）	你已经无法控制自己的动作，战斗节奏完全崩坏      //疯狂
    //X,      // ⛧   Xenosis（异变）       你已经不是人类了，敌人无法理解你的攻击方式 
}

public class RankSystem : MonoBehaviour
{
    public static RankSystem rankSystem;

    [SerializeField]
    //配置各评级
    private float[] ranks;

    [SerializeField]
    //评值最大值
    private float RankValueMax
    {
        get
        {
            return ranks.Sum();
        }
    }

    //评值
    [ReadOnly]
    [SerializeField]
    private float rankValue;

    public float time;  //评值降低的时间
    public float t;

    private void Awake()
    {
        if (rankSystem == null)
        {
            rankSystem = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //配置各评级
        ranks = new float[7]
        {
            //-1,  //F
            8,  //E
            8,  //D
            8,  //C
            12,  //B
            12,  //A
            12,  //S
            //12,  //SS
            20,  //O
            //15,  //X
        };
    }

    private void Start()
    {
        ACRankValueOverflow += LogRankValueOverflow;
        SetRankValue(0f);
        t = time;
    }
    private void Update()
    {
        //评值降低
        if (rankValue > 0f)
        {
            if (t >= 0f)
                t -= Time.deltaTime;
            else
            {
                DetectRankValue(1f * Time.deltaTime); //减少评值
            }
        }
    }
    //---------------评值行为方法--------、

    //攻击行为
    public static void Attack()
    {
        AddRankValue(1f); //增加评值
        rankSystem.t = rankSystem.time; //重置评值降低时间
    }

    //---------------方法---------------

    //获取评级
    public static Rank GetRank()
    {
        Rank rk = Rank.E;
        float value = rankSystem.ranks[(int)rk]; // 评级对应的阈值
        while ((int)rk < rankSystem.ranks.Length - 1 && value < rankSystem.rankValue)
        {
            rk = (Rank)((int)rk + 1); // 使用安全的方式递增枚举
            value += rankSystem.ranks[(int)rk];  // 更新阈值
        }
        return rk;
    }

    public static RankABCD GetRankABCD()
    {
        Rank rank = GetRank();
        if (/*rank == Rank.F || */rank == Rank.E || rank == Rank.D)
        {
            return RankABCD.A;
        }
        else if (rank == Rank.C || rank == Rank.B || rank == Rank.A)
        {
            return RankABCD.B;
        }
        else if (rank == Rank.S /*|| rank == Rank.SS*/)
        {
            return RankABCD.C;
        }
        else if (rank == Rank.O /*|| rank == Rank.X*/)
        {
            return RankABCD.D;
        }
        return RankABCD.A;
    }

    //获取评值
    public static float GetRankValue()
    {
        return rankSystem.rankValue;
    }

    //改变评值
    public static void SetRankValue(float value)
    {

        Rank rankBefore = GetRank();//评值改变前的评级
        Rank rankAfter;//改变后的评级

        //评值减少
        if (value < rankSystem.rankValue)
        {
            rankSystem.rankValue = Mathf.Clamp(value, 0, rankSystem.rankValue);//不能小于0
        }
        //评值不变
        else if (value == rankSystem.rankValue)
        {
            rankSystem.rankValue = value;
        }
        //评值增加
        else
        {
            //溢出值
            float overValue;
            float max = rankSystem.RankValueMax;
            //没有溢出
            if (value <= max)
            {
                overValue = 0f;
            }
            //计算溢出评值
            else
            {
                overValue = value - max;
            }
            rankSystem.rankValue = Mathf.Clamp(value, rankSystem.rankValue, max);//不能超过最大值
            //处理溢出的评值
            if (overValue > 0f)
                ACRankValueOverflow?.Invoke(overValue);
        }
        rankAfter = GetRank();

        //处理评级改变
        int dic = Math.Sign((int)rankAfter - (int)rankBefore);
        for (; rankBefore != rankAfter; rankBefore += dic)
        {
            //退出旧的评级
            switch (rankBefore)
            {
                // case Rank.F:
                //     {
                //         rankSystem.ACOutofF?.Invoke();
                //         break;
                //     }
                case Rank.E:
                    {
                        rankSystem.ACOutofE?.Invoke();
                        break;
                    }
                case Rank.D:
                    {
                        rankSystem.ACOutofD?.Invoke();
                        break;
                    }
                case Rank.C:
                    {
                        rankSystem.ACOutofC?.Invoke();
                        break;
                    }
                case Rank.B:
                    {
                        rankSystem.ACOutofB?.Invoke();
                        break;
                    }
                case Rank.A:
                    {
                        rankSystem.ACOutofA?.Invoke();
                        break;
                    }
                case Rank.S:
                    {
                        rankSystem.ACOutofS?.Invoke();
                        break;
                    }
                // case Rank.SS:
                //     {
                //         rankSystem.ACOutofSS?.Invoke();
                //         break;
                //     }
                case Rank.O:
                    {
                        rankSystem.ACOutofO?.Invoke();
                        break;
                    }
                    // case Rank.X:
                    //     {
                    //         rankSystem.ACOutofX?.Invoke();
                    //         break;
                    //     }
            }

            //进入新的评级
            switch (rankAfter)
            {
                // case Rank.F:
                //     {
                //         rankSystem.ACIntoF?.Invoke();
                //         break;
                //     }
                case Rank.E:
                    {
                        rankSystem.ACIntoE?.Invoke();
                        break;
                    }
                case Rank.D:
                    {
                        rankSystem.ACIntoD?.Invoke();
                        break;
                    }
                case Rank.C:
                    {
                        rankSystem.ACIntoC?.Invoke();
                        break;
                    }
                case Rank.B:
                    {
                        rankSystem.ACIntoB?.Invoke();
                        break;
                    }
                case Rank.A:
                    {
                        rankSystem.ACIntoA?.Invoke();
                        break;
                    }
                case Rank.S:
                    {
                        rankSystem.ACIntoS?.Invoke();
                        break;
                    }
                // case Rank.SS:
                //     {
                //         rankSystem.ACIntoSS?.Invoke();
                //         break;
                //     }
                case Rank.O:
                    {
                        rankSystem.ACIntoO?.Invoke();
                        break;
                    }
                    // case Rank.X:
                    //     {
                    //         rankSystem.ACIntoX?.Invoke();
                    //         break;
                    //     }
            }

            //评级提升
            if (rankAfter > rankBefore)
            {
                rankSystem.ACRankUp?.Invoke();
            }
            //评级降低
            else
            {
                rankSystem.ACRankDown?.Invoke();
            }
        }

    }
    //增加评值
    public static void AddRankValue(float addvalue)
    {
        SetRankValue(GetRankValue() + addvalue);
    }
    //减少评值
    public static void DetectRankValue(float detectvalue)
    {
        SetRankValue(GetRankValue() - detectvalue);
    }

    //获得特定评级容纳多少评值
    public static float GetRankVolumn(Rank rank)
    {
        return rankSystem.ranks[(int)rank];
    }

    //获得特定评级下最多拥有多少评值
    public static float GetMaxValueAtRank(Rank rank)
    {
        float value = 0f;
        for (int i = 0; i <= (int)rank; i++)
        {
            value += rankSystem.ranks[i]; // 修正这里的索引
        }
        return value;
    }

    //获得当前评级已经容纳多少评值 
    public static float GetCurrentRankHasValue()
    {
        return GetRankVolumn(GetRank()) - GetCurrentRankAvailVolumn();
    }

    //获得当前评级还能容纳多少评值
    public static float GetCurrentRankAvailVolumn()
    {
        return GetMaxValueAtRank(GetRank()) - rankSystem.rankValue;
    }

    //填满当前评级
    public static void FillOutCurrentRank()
    {
        AddRankValue(GetCurrentRankAvailVolumn());
    }

    //提升一个评级
    public static void UpLevelRank()
    {
        AddRankValue(GetMaxValueAtRank(GetRank()));
    }

    //评值溢出Action
    public static Action<float> ACRankValueOverflow;
    private void LogRankValueOverflow(float overValue)
    {
        Debug.Log($"评值溢出了{overValue}");
    }

    public Action ACIntoF;
    public Action ACOutofF;

    public Action ACIntoE;
    public Action ACOutofE;

    public Action ACIntoD;
    public Action ACOutofD;

    public Action ACIntoC;
    public Action ACOutofC;

    public Action ACIntoB;
    public Action ACOutofB;

    public Action ACIntoA;
    public Action ACOutofA;

    public Action ACIntoS;
    public Action ACOutofS;

    public Action ACIntoSS;
    public Action ACOutofSS;

    public Action ACIntoO;
    public Action ACOutofO;

    public Action ACIntoX;
    public Action ACOutofX;

    public Action ACRankUp;
    public Action ACRankDown;

}
