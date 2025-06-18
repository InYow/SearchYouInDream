using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class AudioSourcePlayInspector : MonoBehaviour
{
    public List<AudioSource> audioSources = new();
    public int index = 0;

    [Button("GetComponent")]
    public void GetComponents()
    {
        audioSources = GetComponents<AudioSource>().ToList();
    }

    [Button("Play")]
    public void Play()
    {
        audioSources[index].Play();
    }
}
