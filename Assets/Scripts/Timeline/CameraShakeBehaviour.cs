using UnityEngine;
using UnityEngine.Playables;

public enum CameraShakeBehaviourType
{
    Recoil = 0,
    Expolsion,
}

[System.Serializable]
public class CameraShakeBehaviour : PlayableBehaviour
{
    public CameraShakeBehaviourType type = CameraShakeBehaviourType.Recoil; // 震动类型
    public float shakeForce = 0.3f; // 震动力度
    public Vector3 shakeVelocity = new Vector3(1, 0, 0); // 震动速度
    public float shakeDuration = 0f; // 震动持续时间

    // 当 Clip 开始播放时调用
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
        if (!Application.isPlaying)
            return;
        if (type == CameraShakeBehaviourType.Expolsion)
        {
            CameraShake.SetExplosionShakeTime(shakeDuration); // 设置震动时间
            CameraShake.ShakeExplosion(shakeVelocity, shakeForce);
        }
        else if (type == CameraShakeBehaviourType.Recoil)
        {
            CameraShake.SetRecoilShakeTime(shakeDuration); // 设置震动时间
            CameraShake.ShakeRecoil(shakeVelocity, shakeForce);
        }
    }
}
