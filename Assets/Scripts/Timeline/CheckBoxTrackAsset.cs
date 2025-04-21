using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.8f, 0.1f, 0.1f)] // 轨道颜色
[TrackBindingType(typeof(CheckBox))] // 绑定类型为 CheckBox
[TrackClipType(typeof(CheckBoxClipAsset))] // 轨道只能放 `SkillSectionClip`
public class CheckBoxTrackAsset : TrackAsset
{
}