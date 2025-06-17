using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using DG.Tweening;
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

    public static void PlayOneshot(string name)
    {

        GameObject go = instance.transform.Find(name).gameObject;
        //get audio sources
        var audioSources = go.GetComponents<AudioSource>();
        //random select 
        var audioSource = audioSources[Random.Range(0, audioSources.Length)];

        audioSource.PlayOneShot(audioSource.clip);
    }

    public static void Play(string name, float delay)
    {
        DOVirtual.DelayedCall(delay, () =>
       {
           Play(name); // 延时后调用 Play 方法
       }).SetUpdate(true); // 使用 unscaled time，确保延时不受 Time.timeScale 影响
    }

    public static void PlayIfFinish(string name)
    {
        var audios = Get(name);
        foreach (var audio in audios)
        {
            if (audio.isPlaying)
                return;
        }
        Play(name);
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
