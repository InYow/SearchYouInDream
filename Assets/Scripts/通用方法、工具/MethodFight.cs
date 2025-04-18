using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MethodFight : MonoBehaviour
{
    /// <summary>
    /// 返回扇形区域内所有T类型组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="origin">扇形圆点</param>
    /// <param name="forward">中轴方向</param>
    /// <param name="radius_outer">外径</param>
    /// <param name="radius_inner">内径</param>
    /// <param name="angle">角度</param>
    /// <param name="targetLayer">目标层</param>
    /// <returns></returns>
    public static List<T> DetectComponentsInSector<T>(Vector2 origin, Vector2 forward, float radius_outer, float radius_inner, float angle, LayerMask targetLayer)
    {
        //获取所有在 `radius` 半径内的碰撞体
        Collider2D[] colliders = Physics2D.OverlapCircleAll(origin, radius_outer, targetLayer);

        List<T> tList = new List<T>();

        foreach (var collider in colliders)
        {
            Vector2 dirToTarget = ((Vector2)collider.transform.position - origin).normalized;

            //计算方向夹角
            float angleToTarget = Vector2.Angle(forward, dirToTarget);
            //计算距离
            float distance = ((Vector2)collider.transform.position - origin).magnitude;

            //如果目标在扇形范围内
            if (distance >= radius_inner && angleToTarget <= angle / 2)
            {
                //进一步检查该对象是否包含指定的组件
                var t = collider.GetComponent<T>();
                if (t != null)
                {
                    //OPTIMIZE 按距离排序插入
                    tList.Add(t);
                }
            }
        }
        //输出检测到的目标
        Debug.Log($"检测到 {tList.Count} 个目标");
        return tList;
    }

    /// <summary>
    /// 扇区最近的Enemy
    /// </summary>
    /// <param name="origin">扇形圆点</param>
    /// <param name="forward">中轴方向</param>
    /// <param name="radius_outer">外径</param>
    /// <param name="radius_inner">内径</param>
    /// <param name="angle">角度</param>
    /// <param name="targetLayer">目标层</param>
    /// <returns></returns>
    public static Enemy GetAtkFTarget(Vector2 origin, Vector2 forward, float radius_outer, float radius_inner, float angle, LayerMask targetLayer)
    {
        var enemies = DetectComponentsInSector<Enemy>(origin, forward, radius_outer, radius_inner, angle, targetLayer);

        Enemy closest = null;
        float minDistance = float.MaxValue;
        foreach (var enemy in enemies)
        {
            float distance = Vector2.SqrMagnitude(origin - (Vector2)enemy.transform.position); // 用 SqrMagnitude 提高性能
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy;
            }
        }
        return closest;
    }
}
