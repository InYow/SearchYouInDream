using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCanvas : MonoBehaviour
{
    public void LoadPlayScene()
    {
        SceneLoader.instance.LoadScene("功能开发场景-美术勿动");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
