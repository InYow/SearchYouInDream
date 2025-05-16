using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAfterBuild : MonoBehaviour
{
    public static DebugAfterBuild debugAfterBuild;

    public GameObject g1;
    public GameObject g2;
    public GameObject g3;

    private void Awake()
    {
        if (debugAfterBuild == null)
        {
            debugAfterBuild = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            g1.SetActive(false);
            g2.SetActive(false);
            g3.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            DebugAfterBuild.C();
        }
    }

    public static void A()
    {
        debugAfterBuild.g1.SetActive(true);
    }
    public static void B()
    {
        debugAfterBuild.g2.SetActive(true);
    }
    public static void C()
    {
        debugAfterBuild.g3.SetActive(true);
    }


}
