using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    public TextMeshProUGUI rankGUI;

    public List<GameObject> gameObjects = new List<GameObject>();
    public List<Image> images = new List<Image>();
    public Image image;

    private void Start()
    {
        RankSystem.ACIntoF += OpenGO_F;
        RankSystem.ACOutofF += CloseGO_F;
        RankSystem.ACIntoE += OpenGO_E;
        RankSystem.ACOutofE += CloseGO_E;
        RankSystem.ACIntoD += OpenGO_D;
        RankSystem.ACOutofD += CloseGO_D;
        RankSystem.ACIntoC += OpenGO_C;
        RankSystem.ACOutofC += CloseGO_C;
        RankSystem.ACIntoB += OpenGO_B;
        RankSystem.ACOutofB += CloseGO_B;
        RankSystem.ACIntoA += OpenGO_A;
        RankSystem.ACOutofA += CloseGO_A;
        RankSystem.ACIntoS += OpenGO_S;
        RankSystem.ACOutofS += CloseGO_S;
        RankSystem.ACIntoSS += OpenGO_SS;
        RankSystem.ACOutofSS += CloseGO_SS;
        RankSystem.ACIntoO += OpenGO_O;
        RankSystem.ACOutofO += CloseGO_O;
        RankSystem.ACIntoX += OpenGO_X;
        RankSystem.ACOutofX += CloseGO_X;
    }

    private void Update()
    {
        rankGUI.text = RankSystem.GetRank().ToString();
        image.fillAmount = RankSystem.GetCurrentRankHasValue() / RankSystem.GetRankVolumn(RankSystem.GetRank());
    }

    public void OpenGO_F() { gameObjects[0].SetActive(true); image = images[0]; }
    public void CloseGO_F() { gameObjects[0].SetActive(false); }
    public void OpenGO_E() { gameObjects[1].SetActive(true); image = images[1]; }
    public void CloseGO_E() { gameObjects[1].SetActive(false); }
    public void OpenGO_D() { gameObjects[2].SetActive(true); image = images[2]; }
    public void CloseGO_D() { gameObjects[2].SetActive(false); }
    public void OpenGO_C() { gameObjects[3].SetActive(true); image = images[3]; }
    public void CloseGO_C() { gameObjects[3].SetActive(false); }
    public void OpenGO_B() { gameObjects[4].SetActive(true); image = images[4]; }
    public void CloseGO_B() { gameObjects[4].SetActive(false); }
    public void OpenGO_A() { gameObjects[5].SetActive(true); image = images[5]; }
    public void CloseGO_A() { gameObjects[5].SetActive(false); }
    public void OpenGO_S() { gameObjects[6].SetActive(true); image = images[6]; }
    public void CloseGO_S() { gameObjects[6].SetActive(false); }
    public void OpenGO_SS() { gameObjects[7].SetActive(true); image = images[7]; }
    public void CloseGO_SS() { gameObjects[7].SetActive(false); }
    public void OpenGO_O() { gameObjects[8].SetActive(true); image = images[8]; }
    public void CloseGO_O() { gameObjects[8].SetActive(false); }
    public void OpenGO_X() { gameObjects[9].SetActive(true); image = images[9]; }
    public void CloseGO_X() { gameObjects[9].SetActive(false); }
}
