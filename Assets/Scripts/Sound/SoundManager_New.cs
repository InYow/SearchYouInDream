using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager_New : MonoBehaviour
{
    public static SoundManager_New instance;

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

    public static List<AudioSource> Get(string name)
    {
        GameObject go = instance.transform.Find(name).gameObject;
        //get audio sources
        var audioSources = go.GetComponents<AudioSource>();
        //random select 
        return audioSources.ToList();
    }

    public static void Play(string name)
    {
        GameObject go = instance.transform.Find(name).gameObject;
        //get audio sources
        var audioSources = go.GetComponents<AudioSource>();
        //random select 
        var audioSource = audioSources[Random.Range(0, audioSources.Length)];

        audioSource.Play();
    }

    public static void Stop(string name)
    {
        GameObject go = instance.transform.Find(name).gameObject;
        //get audio sources
        var audioSources = go.GetComponents<AudioSource>();
        //random select 
        var audioSource = audioSources[Random.Range(0, audioSources.Length)];

        audioSource.Stop();
    }

    public static void Pause(string name)
    {
        GameObject go = instance.transform.Find(name).gameObject;
        //get audio sources
        var audioSources = go.GetComponents<AudioSource>();
        //random select 
        var audioSource = audioSources[Random.Range(0, audioSources.Length)];

        audioSource.Pause();
    }

    public static void UnPause(string name)
    {
        GameObject go = instance.transform.Find(name).gameObject;
        //get audio sources
        var audioSources = go.GetComponents<AudioSource>();
        //random select 
        var audioSource = audioSources[Random.Range(0, audioSources.Length)];

        audioSource.UnPause();
    }
}
