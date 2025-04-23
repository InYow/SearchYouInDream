using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class CameraShakeBehaviour : PlayableBehaviour
{
    public float shakeForce = 0.3f; // 震动力度
    public Vector3 shakeVelocity = new Vector3(1, 0, 0); // 震动速度

    // 当 Clip 开始播放时调用
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
        if (!Application.isPlaying)
            return;
        CameraShake.Shake(shakeVelocity, shakeForce);
    }
}
