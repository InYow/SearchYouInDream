using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个脚本未来会成为使用新InputSystem的脚本
public class InputManager : MonoBehaviour
{
    public static InputManager inputManager;

    public KeyCode keyCodePre;  //预输入的技能按键

    void Awake()
    {
        if (inputManager == null)
        {
            inputManager = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public static void LogPreInput(KeyCode keyCode)
    {
        Debug.Log("预输入J");
        inputManager.keyCodePre = keyCode;
    }

    public static bool ReadPreInput(KeyCode keyCode)
    {
        if (keyCode == inputManager.keyCodePre)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ClearPreInput()
    {
        inputManager.keyCodePre = KeyCode.None;
    }

}
