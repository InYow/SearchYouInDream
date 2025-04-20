using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class VFXInitBehaviour : PlayableBehaviour
{
    public string visualEffectName; //特效预制体名字

    // 当 Clip 开始播放时调用
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
        Debug.Log($"播放特效: {visualEffectName}");
        // 在这里添加逻辑，例如实例化特效
        if (!string.IsNullOrEmpty(visualEffectName))
        {
            GameObject vfxPrefab = Resources.Load<GameObject>($"Prefabs/VFX/{visualEffectName}");
            if (vfxPrefab != null)
            {
                // 获取 PlayableDirector 的 Transform
                PlayableDirector director = playable.GetGraph().GetResolver() as PlayableDirector;
                if (director != null)
                {
                    // 实例化特效并设置位置
                    GameObject vfxInstance = GameObject.Instantiate(vfxPrefab, director.transform.position, Quaternion.identity);
                    Vector3 vector3 = vfxInstance.transform.localScale;
                    vector3.x = director.transform.lossyScale.x;
                    vfxInstance.transform.localScale = vector3;
                    Debug.Log($"特效 {visualEffectName} 已生成，位置设置为 {director.transform.position}");
                }
                else
                {
                    Debug.LogWarning("未找到 PlayableDirector，无法设置特效位置。");
                }
            }
            else
            {
                Debug.LogWarning($"未找到特效预制体: {visualEffectName}");
            }
        }
    }
}
