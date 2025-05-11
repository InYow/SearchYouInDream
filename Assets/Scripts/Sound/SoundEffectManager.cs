using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;

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
            Debug.Log("Loading SFX: " + sfxName);
            GameObject sfxInstance = Instantiate(sfxPrefab, transform.position, Quaternion.identity);
        }
    }
}
