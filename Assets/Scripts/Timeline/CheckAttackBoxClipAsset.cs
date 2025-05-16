using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class CheckAttackBoxClipAsset : PlayableAsset, ITimelineClipAsset
{
    public CheckAttackBoxBehaviour template = new CheckAttackBoxBehaviour();

    public ClipCaps clipCaps => ClipCaps.None; // 该 Clip 不支持剪辑变速等功能

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<CheckAttackBoxBehaviour>.Create(graph, template);
    }
}
