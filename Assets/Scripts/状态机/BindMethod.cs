using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class BindMethod
{
    /// <summary>
    /// 绑定go的Animator到playableDirector的第一个"Animation Track"上
    /// </summary>
    /// <param name="playableDirector"></param>
    /// <param name="go"></param>
    public static void BindAnimator(PlayableDirector playableDirector, GameObject go)
    {
        //获取Timeline中的轨道
        TimelineAsset timeline = (TimelineAsset)playableDirector.playableAsset;
        // 绑定AnimationTrack轨道
        var tracks = timeline.GetOutputTracks();
        foreach (var track in tracks)
        {
            if (track.name == "Animation Track") // 根据名字查找你要绑定的轨道
            {
                // 设置绑定
                playableDirector.SetGenericBinding(track, go.GetComponent<Animator>());
                break;
            }
        }
    }
}