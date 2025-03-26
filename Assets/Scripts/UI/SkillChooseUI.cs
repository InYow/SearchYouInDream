using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillChooseUI : MonoBehaviour
{
    public static SkillChooseUI instance;
    public GameObject renderGO;
    public SpriteRenderer spriteRendererA;
    public SpriteRenderer spriteRendererW;
    public SpriteRenderer spriteRendererS;
    public SpriteRenderer spriteRendererD;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Close();
    }

    public static void Open()
    {
        instance.renderGO.SetActive(true);
    }

    public static void Close()
    {
        instance.renderGO.SetActive(false);
    }

    public static void ChooseSkill(string keycode)
    {
        if (keycode == "A")
        {
            instance.spriteRendererA.color = Color.green;
            instance.spriteRendererW.color = Color.white;
            instance.spriteRendererS.color = Color.white;
            instance.spriteRendererD.color = Color.white;
        }
        else if (keycode == "W")
        {
            instance.spriteRendererA.color = Color.white;
            instance.spriteRendererW.color = Color.green;
            instance.spriteRendererS.color = Color.white;
            instance.spriteRendererD.color = Color.white;
        }
        else if (keycode == "S")
        {
            instance.spriteRendererA.color = Color.white;
            instance.spriteRendererW.color = Color.white;
            instance.spriteRendererS.color = Color.green;
            instance.spriteRendererD.color = Color.white;
        }
        else if (keycode == "D")
        {
            instance.spriteRendererA.color = Color.white;
            instance.spriteRendererW.color = Color.white;
            instance.spriteRendererS.color = Color.white;
            instance.spriteRendererD.color = Color.green;
        }
    }
}
