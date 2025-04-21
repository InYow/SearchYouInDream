using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HitFly))]
public class HitFlyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 获取目标对象
        HitFly hitFly = (HitFly)target;

        // 绘制默认属性
        DrawDefaultInspector();

        // 根据 hitFlyType 的值动态隐藏 flySpeed
        if (hitFly.hitFlyType == HitFlyType.Float)
        {
            // 显示 flySpeed 属性
            hitFly.flySpeed = EditorGUILayout.FloatField("Fly Speed", hitFly.flySpeed);
        }
        else if (hitFly.hitFlyType == HitFlyType.Curve)
        {
            // 显示 flySpeed 属性
            hitFly.flyCurve = EditorGUILayout.CurveField("Fly Curve", hitFly.flyCurve);
        }
    }
}