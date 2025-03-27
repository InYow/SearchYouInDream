using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// 前后摇类型
/// </summary>
public enum WindType
{
    windup,     //前摇
    windflush,  //生效
    winddown,   //后摇
}

public enum InputWindType
{
    inputdis,       //输入无效
    inputpre,       //预输入
    inputable,      //输入有效
}

public abstract class State : MonoBehaviour
{
    public PlayableDirector playableDirector;

    private TimelineAsset timeline;

    public List<TimelineClip> windTypeSectionClipList = new();
    public List<TimelineClip> inputWindTypeSectionClipList = new();


    private void Start()
    {
        //InitClipDiction();
    }

    /// <summary>
    /// 状态开始
    /// </summary>
    /// <param name="entity"></param>
    public abstract void StateStart(Entity entity);

    /// <summary>
    /// 帧初始化
    /// </summary>
    /// <param name="entity"></param>
    public abstract void UPStateInit(Entity entity);

    /// <summary>
    /// 帧状态行为
    /// </summary>
    /// <param name="entity"></param>
    public abstract void UPStateBehaviour(Entity entity);

    /// <summary>
    ///  状态结束
    /// </summary>
    /// <param name="entity"></param>
    public abstract void StateExit(Entity entity);

    ////////////////
    //-------------方法------------
    ////////////////

    /// <summary>
    /// 状态是否结束
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual bool Finished(Entity entity)
    {
        if (playableDirector.time >= playableDirector.duration)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [ContextMenu("初始化自定义Clip引用集")]
    /// <summary>
    /// 初始化自定义Clip引用集
    /// </summary>
    public void InitClipDiction()
    {
        timeline = playableDirector.playableAsset as TimelineAsset;
        if (timeline == null) return;

        // 遍历 Timeline 里的所有轨道
        foreach (var trackAsset in timeline.GetOutputTracks())
        {
            if (trackAsset is WindTypeSectionTrackAsset windTrackAsset)                    //处理windtrack
            {
                windTypeSectionClipList = windTrackAsset.GetClips().ToList();
            }
            else if (trackAsset is InputWindTypeSectionTrackAsset inputWindTrackAsset)             //处理inputwindtrack
            {
                inputWindTypeSectionClipList = inputWindTrackAsset.GetClips().ToList();
            }
        }
    }

    /// <summary>
    /// 获取当前状态，当前帧的WindType
    /// </summary>
    /// <returns></returns>
    public WindType GetCurrentStateWindType()
    {
        double currentTime = playableDirector.time; // 获取当前播放时间

        foreach (var clip in windTypeSectionClipList)
        {
            if (currentTime >= clip.start && currentTime <= clip.end)
            {
                Debug.Log("当前处于段落：" + (clip.asset as WindTypeSectionClipAsset).template.sectionWindType);
                return (clip.asset as WindTypeSectionClipAsset).template.sectionWindType;
            }
        }
        return WindType.winddown;
    }

    /// <summary>
    /// 获取当前状态，当前帧的InputWindType
    /// </summary>
    /// <returns></returns>
    public InputWindType GetCurrentStateInputWindType()
    {
        double currentTime = playableDirector.time; // 获取当前播放时间

        foreach (var clip in inputWindTypeSectionClipList)
        {
            if (currentTime >= clip.start && currentTime <= clip.end)
            {
                Debug.Log("当前处于段落：" + (clip.asset as InputWindTypeSectionClipAsset).template.sectionInputWindType);
                return (clip.asset as InputWindTypeSectionClipAsset).template.sectionInputWindType;
            }
        }
        return InputWindType.inputable;
    }
}
