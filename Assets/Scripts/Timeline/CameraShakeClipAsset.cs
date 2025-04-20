using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class CameraShakeClipAsset : PlayableAsset, ITimelineClipAsset
{
    public CameraShakeBehaviour template = new CameraShakeBehaviour();

    public ClipCaps clipCaps => ClipCaps.None; // 该 Clip 不支持剪辑变速等功能

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<CameraShakeBehaviour>.Create(graph, template);
    }
}
