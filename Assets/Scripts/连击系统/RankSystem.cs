using System;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//你别说, AI比我还能编
public enum Rank
{
    F = 0,  //  Fear（恐惧）	你的动作畏畏缩缩，像个猎物              //正常
    E,      //	Erosion（侵蚀）	你开始适应暴力，但仍然犹豫
    D,      //	Daze（恍惚）	你在杀戮中摇摆不定，手感迟钝    
    C,      //	Carnage（屠戮）	你变得越来越顺畅，攻击带着野性          //中等
    B,      //	Brutal（暴虐）	你的战斗开始变得暴力，节奏加快
    A,      //	Anarchy（无序）	你放弃防守，全力进攻
    S,      //	Savage（狂暴）	你的攻击已经无可阻挡，纯粹的碾碎敌人    //高等
    SS,     //  SchiSm（精神裂变）	你已经超越了战斗本能，攻击带有扭曲的癫狂
    O,      // Ø	Oblivion（湮灭）	你已经无法控制自己的动作，战斗节奏完全崩坏  //疯狂
    X,      // ⛧   Xenosis（异变）       你已经不是人类了，敌人无法理解你的攻击方式 
}

public class RankSystem : MonoBehaviour
{
    public static RankSystem rankSystem;

    public TextMeshProUGUI rankGUI;

    public Image image;

    //配置各评级
    private static float[] ranks;

    //评值最大值
    private static float RankValueMax
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
        ranks = new float[10]
        {
            5,  //F
            6,  //E
            7,  //D
            8,  //C
            9,  //B
            10,  //A
            11,  //S
            12,  //SS
            13,  //O
            15,  //X
        };
    }

    private void Start()
    {
        ACRankValueOverflow += LogRankValueOverflow;
    }

    private void Update()
    {
        rankGUI.text = GetRank().ToString();
        image.fillAmount = GetCurrentRankHasValue() / GetRankVolumn(GetRank());
    }

    //---------------方法---------------

    //获取评级
    public static Rank GetRank()
    {
        Rank rk = Rank.F;
        float value = ranks[(int)rk]; // 评级对应的阈值
        while ((int)rk < ranks.Length - 1 && value < rankSystem.rankValue)
        {
            rk = (Rank)((int)rk + 1); // 使用安全的方式递增枚举
            value += ranks[(int)rk];  // 更新阈值
        }
        return rk;
    }

    public static RankABCD GetRankABCD()
    {
        Rank rank = GetRank();
        if (rank == Rank.F || rank == Rank.E || rank == Rank.D)
        {
            return RankABCD.A;
        }
        else if (rank == Rank.C || rank == Rank.B || rank == Rank.A)
        {
            return RankABCD.B;
        }
        else if (rank == Rank.S || rank == Rank.SS)
        {
            return RankABCD.C;
        }
        else if (rank == Rank.O || rank == Rank.X)
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
            float max = RankValueMax;
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
                case Rank.F:
                    {
                        ACOutofF?.Invoke();
                        break;
                    }
                case Rank.E:
                    {
                        ACOutofE?.Invoke();
                        break;
                    }
                case Rank.D:
                    {
                        ACOutofD?.Invoke();
                        break;
                    }
                case Rank.C:
                    {
                        ACOutofC?.Invoke();
                        break;
                    }
                case Rank.B:
                    {
                        ACOutofB?.Invoke();
                        break;
                    }
                case Rank.A:
                    {
                        ACOutofA?.Invoke();
                        break;
                    }
                case Rank.S:
                    {
                        ACOutofS?.Invoke();
                        break;
                    }
                case Rank.SS:
                    {
                        ACOutofSS?.Invoke();
                        break;
                    }
                case Rank.O:
                    {
                        ACOutofO?.Invoke();
                        break;
                    }
                case Rank.X:
                    {
                        ACOutofX?.Invoke();
                        break;
                    }
            }

            //进入新的评级
            switch (rankAfter)
            {
                case Rank.F:
                    {
                        ACIntoF?.Invoke();
                        break;
                    }
                case Rank.E:
                    {
                        ACIntoE?.Invoke();
                        break;
                    }
                case Rank.D:
                    {
                        ACIntoD?.Invoke();
                        break;
                    }
                case Rank.C:
                    {
                        ACIntoC?.Invoke();
                        break;
                    }
                case Rank.B:
                    {
                        ACIntoB?.Invoke();
                        break;
                    }
                case Rank.A:
                    {
                        ACIntoA?.Invoke();
                        break;
                    }
                case Rank.S:
                    {
                        ACIntoS?.Invoke();
                        break;
                    }
                case Rank.SS:
                    {
                        ACIntoSS?.Invoke();
                        break;
                    }
                case Rank.O:
                    {
                        ACIntoO?.Invoke();
                        break;
                    }
                case Rank.X:
                    {
                        ACIntoX?.Invoke();
                        break;
                    }
            }

            //评级提升
            if (rankAfter > rankBefore)
            {
                ACRankUp?.Invoke();
            }
            //评级降低
            else
            {
                ACRankDown?.Invoke();
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
        return ranks[(int)rank];
    }

    //获得特定评级下最多拥有多少评值
    public static float GetMaxValueAtRank(Rank rank)
    {
        float value = 0f;
        for (int i = 0; i <= (int)rank; i++)
        {
            value += ranks[i]; // 修正这里的索引
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

    public static Action ACIntoF;
    public static Action ACOutofF;

    public static Action ACIntoE;
    public static Action ACOutofE;

    public static Action ACIntoD;
    public static Action ACOutofD;

    public static Action ACIntoC;
    public static Action ACOutofC;

    public static Action ACIntoB;
    public static Action ACOutofB;

    public static Action ACIntoA;
    public static Action ACOutofA;

    public static Action ACIntoS;
    public static Action ACOutofS;

    public static Action ACIntoSS;
    public static Action ACOutofSS;

    public static Action ACIntoO;
    public static Action ACOutofO;

    public static Action ACIntoX;
    public static Action ACOutofX;

    public static Action ACRankUp;
    public static Action ACRankDown;

}
