using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public abstract class State : MonoBehaviour
{
    public PlayableDirector playableDirector;
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
}
