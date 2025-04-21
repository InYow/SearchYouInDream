using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CheckBox))]
public class CheckBoxEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 获取目标对象
        CheckBox checkBox = (CheckBox)target;

        // 开始检查属性修改
        serializedObject.Update();

        DrawProperty("checkBoxType");

        // 根据 checkBoxType 的值动态显示相关字段
        switch (checkBox.checkBoxType)
        {
            case CheckBoxType.attack:
                DrawProperty("boxSize");
                DrawProperty("attackLayer");
                DrawProperty("descrition");
                DrawProperty("attacktype");
                break;

            case CheckBoxType.pick:
                DrawProperty("pickLayer");
                DrawProperty("picked");
                break;

            case CheckBoxType.attack_throwitem:
                DrawProperty("boxSize");
                DrawProperty("attackLayer");
                DrawProperty("pickableItem_master");
                break;
        }

        // 应用修改
        serializedObject.ApplyModifiedProperties();
    }

    // 辅助方法：绘制属性
    private void DrawProperty(string propertyName)
    {
        SerializedProperty property = serializedObject.FindProperty(propertyName);
        if (property != null)
        {
            EditorGUILayout.PropertyField(property);
        }
    }
}