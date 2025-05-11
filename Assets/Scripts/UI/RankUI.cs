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
        RankSystem.rankSystem.ACIntoE += OpenGO_E;
        RankSystem.rankSystem.ACOutofE += CloseGO_E;
        RankSystem.rankSystem.ACIntoD += OpenGO_D;
        RankSystem.rankSystem.ACOutofD += CloseGO_D;
        RankSystem.rankSystem.ACIntoC += OpenGO_C;
        RankSystem.rankSystem.ACOutofC += CloseGO_C;
        RankSystem.rankSystem.ACIntoB += OpenGO_B;
        RankSystem.rankSystem.ACOutofB += CloseGO_B;
        RankSystem.rankSystem.ACIntoA += OpenGO_A;
        RankSystem.rankSystem.ACOutofA += CloseGO_A;
        RankSystem.rankSystem.ACIntoS += OpenGO_S;
        RankSystem.rankSystem.ACOutofS += CloseGO_S;
        RankSystem.rankSystem.ACIntoO += OpenGO_O;
        RankSystem.rankSystem.ACOutofO += CloseGO_O;

        OpenGO_E();
    }

    private void Update()
    {
        rankGUI.text = RankSystem.GetRank().ToString();
        image.fillAmount = RankSystem.GetCurrentRankHasValue() / RankSystem.GetRankVolumn(RankSystem.GetRank());
    }

    public void OpenGO_E() { gameObjects[0].SetActive(true); image = images[0]; }
    public void CloseGO_E() { gameObjects[0].SetActive(false); }
    public void OpenGO_D() { gameObjects[1].SetActive(true); image = images[1]; }
    public void CloseGO_D() { gameObjects[1].SetActive(false); }
    public void OpenGO_C() { gameObjects[2].SetActive(true); image = images[2]; }
    public void CloseGO_C() { gameObjects[2].SetActive(false); }
    public void OpenGO_B() { gameObjects[3].SetActive(true); image = images[3]; }
    public void CloseGO_B() { gameObjects[3].SetActive(false); }
    public void OpenGO_A() { gameObjects[4].SetActive(true); image = images[4]; }
    public void CloseGO_A() { gameObjects[4].SetActive(false); }
    public void OpenGO_S() { gameObjects[5].SetActive(true); image = images[5]; }
    public void CloseGO_S() { gameObjects[5].SetActive(false); }
    public void OpenGO_O() { gameObjects[6].SetActive(true); image = images[6]; }
    public void CloseGO_O() { gameObjects[6].SetActive(false); }
}
