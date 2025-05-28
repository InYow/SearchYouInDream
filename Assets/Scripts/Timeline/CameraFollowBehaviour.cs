using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class CameraFollowBehaviour : PlayableBehaviour
{
    private Transform cameraPivot;
    // 当 Clip 开始播放时调用
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
        if (!Application.isPlaying)
            return;
        CameraFollow.SetFollow(cameraPivot);
        //CameraFollow.EnableConfiner();
    }
    public override void OnPlayableCreate(Playable playable)
    {
        base.OnPlayableCreate(playable);
        GameObject ownerGameObject = null;
        if (playable.GetGraph().GetResolver() is PlayableDirector director)
        {
            ownerGameObject = director.gameObject;
        }
        cameraPivot = ownerGameObject.transform.GetComponentInParent<Entity>().camera_Pivot;
        if (cameraPivot == null)
        {
            Debug.LogError("entity_master is null, please check the entity master in the inspector.", ownerGameObject);
        }
    }
}
