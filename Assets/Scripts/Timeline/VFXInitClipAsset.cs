using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class VFXInitClipAsset : PlayableAsset, ITimelineClipAsset
{
    public VFXInitBehaviour template = new VFXInitBehaviour();

    public ClipCaps clipCaps => ClipCaps.None; // 该 Clip 不支持剪辑变速等功能

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<VFXInitBehaviour>.Create(graph, template);
    }
}
