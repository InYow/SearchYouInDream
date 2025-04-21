using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

//弃用
public class PlayableDirectorBinder : MonoBehaviour
{
    public PlayableDirector playableDirector;

    public Animator animator;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        animator = transform.parent.GetComponent<Animator>();
    }

    [ContextMenu("绑定Player_Animator")]
    public void Bind_Parent_Animator()
    {
        // 获取Timeline中的轨道
        TimelineAsset timeline = (TimelineAsset)playableDirector.playableAsset;

        // 绑定AnimationTrack轨道
        var tracks = timeline.GetOutputTracks();
        foreach (var track in tracks)
        {
            if (track.name == "Animation Track") // 根据名字查找你要绑定的轨道
            {
                // 设置绑定
                Debug.Log("绑定");
                playableDirector.SetGenericBinding(track, animator);
                break;
            }
        }
    }
}