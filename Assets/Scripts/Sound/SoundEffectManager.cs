using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;

    public List<string> sfxList01 = new List<string>();

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

    public static void PlaySFX(string sfxName, Transform transform)
    {
        GameObject sfxPrefab = Resources.Load<GameObject>("Prefabs/SFX/" + sfxName);
        if (sfxPrefab != null)
        {
            // Debug.Log("Loading SFX: " + sfxName);
            GameObject sfxInstance = Instantiate(sfxPrefab, transform.position, Quaternion.identity);
        }
    }

    public static void PlaySFX01(Transform transform)
    {
        string sfxName = instance.sfxList01[Random.Range(0, instance.sfxList01.Count)];
        GameObject sfxPrefab = Resources.Load<GameObject>("Prefabs/SFX/" + sfxName);
        if (sfxPrefab != null)
        {
            //Debug.Log("Loading SFX: " + sfxName);
            GameObject sfxInstance = Instantiate(sfxPrefab, transform.position, Quaternion.identity);
        }
    }
}
