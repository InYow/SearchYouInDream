using UnityEngine;
using UnityEngine.Timeline;

[TrackColor(0.1f, 0.2f, 0.8f)]  // 设置轨道的颜色
[TrackClipType(typeof(NewPlayableAsset))]  // 设置轨道上可以包含的Clip类型
public class NewPlayableTrack : TrackAsset
{
    // 你可以在这里自定义额外的逻辑
}
