using UnityEngine;
using UnityEngine.Playables;

public class NewPlayableBehaviour : PlayableBehaviour
{
    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
        Debug.Log("Graph Started");
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {
        Debug.Log("Graph Stopped");
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        Debug.Log("Playable Started");
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        Debug.Log("Playable Paused");
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        // 在每一帧更新播放的内容，例如动画速度、位置等
    }
}
