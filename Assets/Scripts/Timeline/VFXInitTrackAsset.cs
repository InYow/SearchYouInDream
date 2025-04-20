using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.8f, 0.1f, 0.1f)] // 轨道颜色
[TrackClipType(typeof(VFXInitClipAsset))] // 轨道只能放 `SkillSectionClip`
public class VFXInitTrackAsset : TrackAsset
{
}