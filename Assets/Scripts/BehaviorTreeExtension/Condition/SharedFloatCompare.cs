using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SharedFloatCompare : Conditional
{
    public SharedFloat globalVariable; // 全局变量
    public SharedFloat comparTO; // 参考值
    public ComparisonType comparisonType; // 比较类型

    public enum ComparisonType
    {
        Greater,    // 大于
        GreaterOrEqual, // 大于等于
        Less,      // 小于
        LessOrEqual,  // 小于等于
        Equal,      // 等于
        NotEqual    // 不等于
    }

    private bool CanExecute()
    {
        // 获取全局变量的值
        float globalValue = globalVariable.Value;

        // 根据比较类型执行不同的比较操作
        switch (comparisonType)
        {
            case ComparisonType.Greater:
                return globalValue > comparTO.Value;
            case ComparisonType.GreaterOrEqual:
                return globalValue >= comparTO.Value;
            case ComparisonType.Less:
                return globalValue < comparTO.Value;
            case ComparisonType.LessOrEqual:
                return globalValue <= comparTO.Value;
            case ComparisonType.Equal:
                return globalValue == comparTO.Value;
            case ComparisonType.NotEqual:
                return globalValue != comparTO.Value;
        }
        return false;
    }
        
    public override TaskStatus OnUpdate()
    {
        // 先判断条件是否成立，如果不成立则直接返回失败
        if (!CanExecute())
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }

    public override void OnReset()
    {
        base.OnReset();
        globalVariable = 0;
        comparTO = 0;
    }
}
