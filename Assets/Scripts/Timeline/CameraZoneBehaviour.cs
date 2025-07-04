using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class CameraZoneBehaviour : PlayableBehaviour
{
    public CameraZoneData cameraZoneData;

    // 当 Clip 开始播放时调用
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
        if (!Application.isPlaying)
            return;
        if (cameraZoneData == null)
        {
            return;
        }
        CameraZone.CameraZoneUseData(cameraZoneData);
    }
}
