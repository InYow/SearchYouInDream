using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
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

/// <summary>
/// 输入窗口类型
/// </summary>
public enum InputWindType
{
    inputdis,       //输入无效
    inputpre,       //预输入
    inputable,      //输入有效
}

public enum RankABCD
{
    A,
    B,
    C,
    D
}

[RequireComponent(typeof(PlayableDirector))]
public abstract class State : MonoBehaviour
{
    [HideInInspector]
    public PlayableDirector playableDirector;

    public DirectorWrapMode wrapMode;

    [ReadOnly]
    [SerializeField]
    [Tooltip("按什么评级执行")] private RankABCD rankABCD = RankABCD.A;
    public RankABCD RankABCD
    {
        get
        {
            return rankABCD;
        }
        set
        {
            //使用对应Timeline
            switch (value)
            {
                case RankABCD.A:
                    {
                        playableDirector.playableAsset = playableAsset_A;
                        break;
                    }
                case RankABCD.B:
                    {
                        playableDirector.playableAsset = playableAsset_B;
                        break;
                    }
                case RankABCD.C:
                    {
                        playableDirector.playableAsset = playableAsset_C;
                        break;
                    }
                case RankABCD.D:
                    {
                        playableDirector.playableAsset = playableAsset_D;
                        break;
                    }
                default:
                    {
                        goto case RankABCD.A;
                    }
            }

            rankABCD = value;
        }
    }

    public PlayableAsset playableAsset_A;
    public PlayableAsset playableAsset_B;
    public PlayableAsset playableAsset_C;
    public PlayableAsset playableAsset_D;

    public List<TimelineClip> windTypeSectionClipList_A = new();
    public List<TimelineClip> inputWindTypeSectionClipList_A = new();

    public List<TimelineClip> windTypeSectionClipList_B = new();
    public List<TimelineClip> inputWindTypeSectionClipList_B = new();

    public List<TimelineClip> windTypeSectionClipList_C = new();
    public List<TimelineClip> inputWindTypeSectionClipList_C = new();

    public List<TimelineClip> windTypeSectionClipList_D = new();
    public List<TimelineClip> inputWindTypeSectionClipList_D = new();



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

    public virtual void OnValidate()
    {
        if (playableDirector == null)
        {
            playableDirector = GetComponent<PlayableDirector>();
        }
        if (playableDirector != null)
        {
            if (playableDirector.playableAsset == null)
                playableDirector.playableAsset = playableAsset_A;
            playableDirector.extrapolationMode = wrapMode;
        }
    }

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
        // _A
        TimelineAsset timeline_A = playableAsset_A as TimelineAsset;
        if (timeline_A == null)
        { }
        // 遍历 Timeline 里的所有轨道
        else
        {
            foreach (var trackAsset in timeline_A.GetOutputTracks())
            {
                if (trackAsset is WindTypeSectionTrackAsset windTrackAsset)                    //处理windtrack
                {
                    windTypeSectionClipList_A = windTrackAsset.GetClips().ToList();
                }
                else if (trackAsset is InputWindTypeSectionTrackAsset inputWindTrackAsset)             //处理inputwindtrack
                {
                    inputWindTypeSectionClipList_A = inputWindTrackAsset.GetClips().ToList();
                }
            }
        }
        // _B
        TimelineAsset timeline_B = playableAsset_B as TimelineAsset;
        if (timeline_B == null)
        { }
        // 遍历 Timeline 里的所有轨道
        else
        {
            foreach (var trackAsset in timeline_B.GetOutputTracks())
            {
                if (trackAsset is WindTypeSectionTrackAsset windTrackAsset)                    //处理windtrack
                {
                    windTypeSectionClipList_B = windTrackAsset.GetClips().ToList();
                }
                else if (trackAsset is InputWindTypeSectionTrackAsset inputWindTrackAsset)             //处理inputwindtrack
                {
                    inputWindTypeSectionClipList_B = inputWindTrackAsset.GetClips().ToList();
                }
            }
        }
        // _C
        TimelineAsset timeline_C = playableAsset_C as TimelineAsset;
        if (timeline_C == null)
        { }
        // 遍历 Timeline 里的所有轨道
        else
        {
            foreach (var trackAsset in timeline_C.GetOutputTracks())
            {
                if (trackAsset is WindTypeSectionTrackAsset windTrackAsset)                    //处理windtrack
                {
                    windTypeSectionClipList_C = windTrackAsset.GetClips().ToList();
                }
                else if (trackAsset is InputWindTypeSectionTrackAsset inputWindTrackAsset)             //处理inputwindtrack
                {
                    inputWindTypeSectionClipList_C = inputWindTrackAsset.GetClips().ToList();
                }
            }
        }
        // _D
        TimelineAsset timeline_D = playableAsset_D as TimelineAsset;
        if (timeline_D == null)
        { }
        // 遍历 Timeline 里的所有轨道
        else
        {
            foreach (var trackAsset in timeline_D.GetOutputTracks())
            {
                if (trackAsset is WindTypeSectionTrackAsset windTrackAsset)                    //处理windtrack
                {
                    windTypeSectionClipList_D = windTrackAsset.GetClips().ToList();
                }
                else if (trackAsset is InputWindTypeSectionTrackAsset inputWindTrackAsset)             //处理inputwindtrack
                {
                    inputWindTypeSectionClipList_D = inputWindTrackAsset.GetClips().ToList();
                }
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

        switch (rankABCD)
        {
            case RankABCD.A:
                {
                    foreach (var clip in windTypeSectionClipList_A)
                    {
                        if (currentTime >= clip.start && currentTime <= clip.end)
                        {
                            //当前处于段落Type
                            return (clip.asset as WindTypeSectionClipAsset).template.sectionWindType;
                        }
                    }
                    return WindType.winddown;
                }
            case RankABCD.B:
                {
                    foreach (var clip in windTypeSectionClipList_B)
                    {
                        if (currentTime >= clip.start && currentTime <= clip.end)
                        {
                            //当前处于段落Type
                            return (clip.asset as WindTypeSectionClipAsset).template.sectionWindType;
                        }
                    }
                    return WindType.winddown;
                }
            case RankABCD.C:
                {
                    foreach (var clip in windTypeSectionClipList_C)
                    {
                        if (currentTime >= clip.start && currentTime <= clip.end)
                        {
                            //当前处于段落Type
                            return (clip.asset as WindTypeSectionClipAsset).template.sectionWindType;
                        }
                    }
                    return WindType.winddown;
                }
            case RankABCD.D:
                {
                    foreach (var clip in windTypeSectionClipList_D)
                    {
                        if (currentTime >= clip.start && currentTime <= clip.end)
                        {
                            //当前处于段落Type
                            return (clip.asset as WindTypeSectionClipAsset).template.sectionWindType;
                        }
                    }
                    return WindType.winddown;
                }
            default:
                {
                    return WindType.winddown;
                }
        }

    }

    /// <summary>
    /// 获取当前状态，当前帧的InputWindType
    /// </summary>
    /// <returns></returns>
    public InputWindType GetCurrentStateInputWindType()
    {
        double currentTime = playableDirector.time; // 获取当前播放时间

        switch (rankABCD)
        {
            case RankABCD.A:
                {
                    foreach (var clip in inputWindTypeSectionClipList_A)
                    {
                        if (currentTime >= clip.start && currentTime <= clip.end)
                        {
                            //当前处于段落InputWindType
                            return (clip.asset as InputWindTypeSectionClipAsset).template.sectionInputWindType;
                        }
                    }
                    return InputWindType.inputable;
                }
            case RankABCD.B:
                {
                    foreach (var clip in inputWindTypeSectionClipList_B)
                    {
                        if (currentTime >= clip.start && currentTime <= clip.end)
                        {
                            //当前处于段落InputWindType
                            return (clip.asset as InputWindTypeSectionClipAsset).template.sectionInputWindType;
                        }
                    }
                    return InputWindType.inputable;
                }
            case RankABCD.C:
                {
                    foreach (var clip in inputWindTypeSectionClipList_C)
                    {
                        if (currentTime >= clip.start && currentTime <= clip.end)
                        {
                            //当前处于段落InputWindType
                            return (clip.asset as InputWindTypeSectionClipAsset).template.sectionInputWindType;
                        }
                    }
                    return InputWindType.inputable;
                }
            case RankABCD.D:
                {
                    foreach (var clip in inputWindTypeSectionClipList_D)
                    {
                        if (currentTime >= clip.start && currentTime <= clip.end)
                        {
                            //当前处于段落InputWindType
                            return (clip.asset as InputWindTypeSectionClipAsset).template.sectionInputWindType;
                        }
                    }
                    return InputWindType.inputable;
                }
            default:
                {
                    return InputWindType.inputable;
                }
        }
    }
}
