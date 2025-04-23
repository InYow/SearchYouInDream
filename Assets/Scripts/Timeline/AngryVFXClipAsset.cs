using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class AngryVFXClipAsset : PlayableAsset, ITimelineClipAsset
{
    public AngryVFXBehaviour template = new AngryVFXBehaviour();

    public ClipCaps clipCaps => ClipCaps.SpeedMultiplier; // 该 Clip 不支持剪辑变速等功能

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<AngryVFXBehaviour>.Create(graph, template);
    }
}
