using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustDebug : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (var go in gameObjects)
            {
                if (go.activeSelf == true)
                { 
                    GameManager.Instance.player.transform.position = go.transform.Find("Point").position;
                }
            }
        }
    }
}
