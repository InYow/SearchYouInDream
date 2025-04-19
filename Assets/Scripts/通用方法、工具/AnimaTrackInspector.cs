using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AnimaTrackInspector : MonoBehaviour
{
    private Animator animator;

    private PlayableDirector playableDirector;

    public TimelineAsset timelineAsset;

    [EnumPaging]
    public DirectorWrapMode directorWrapMode;

    [Range(1f, 60f)]
    public int frame;

    private void OnValidate()
    {
        if (playableDirector == null)
        {
            playableDirector = GetComponent<PlayableDirector>();
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (timelineAsset != null)
        {
            PlayableDirectorMethod.BindFirstAnimationTrack(animator, timelineAsset, playableDirector);
            playableDirector.playableAsset = timelineAsset;
        }
        if (playableDirector != null)
        {
            playableDirector.extrapolationMode = directorWrapMode;
        }

    }

    [Button("SetFrame")]
    public void SetFrame()
    {
        float speed = ((float)frame) / 60f;
        PlayableDirectorMethod.SetAnimationTrackFirstClipSpeed(PlayableDirectorMethod.GetFirstAnimationTrack(timelineAsset), speed);
    }

}
#endif