#if UNITY_EDITOR
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public static class PlayableDirectorMethod
{
    // 绑定AnimationTrack轨道
    public static void BindFirstAnimationTrack(Animator animator, TimelineAsset timeline, PlayableDirector playableDirector)
    {
        var tracks = timeline.GetOutputTracks();
        foreach (var track in tracks)
        {
            if (track is AnimationTrack) // 根据名字查找你要绑定的轨道
            {
                // 设置绑定
                playableDirector.SetGenericBinding(track, animator);
                break;
            }
        }
    }

    // 获取第一个AnimationTrack轨道
    public static AnimationTrack GetFirstAnimationTrack(TimelineAsset timeline)
    {
        var tracks = timeline.GetOutputTracks();
        foreach (var track in tracks)
        {
            if (track is AnimationTrack animationTrack) // 根据名字查找你要绑定的轨道
            {
                return animationTrack;
            }
        }
        return null;
    }

    // 设置AnimationTrackFirstClip的速度
    public static void SetAnimationTrackFirstClipSpeed(AnimationTrack animationTrack, float speed)
    {
        if (animationTrack != null)
        {
            var firstClip = animationTrack.GetClips().FirstOrDefault();
            if (firstClip != null)
            {
                firstClip.duration = firstClip.duration / speed * firstClip.timeScale; // 设置持续时间
                firstClip.timeScale = speed; // 设置速度
                // 也可以设置速度，注意这里的speed是一个倍数，所以需要乘以原来的持续时间
            }
        }
    }
}
#endif