using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class InputWindTypeSectionClipAsset : PlayableAsset, ITimelineClipAsset
{
    public InputWindTypeSectionBehaviour template = new InputWindTypeSectionBehaviour();

    public ClipCaps clipCaps => ClipCaps.SpeedMultiplier; // 该 Clip 不支持剪辑变速等功能

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<InputWindTypeSectionBehaviour>.Create(graph, template);
    }
}
