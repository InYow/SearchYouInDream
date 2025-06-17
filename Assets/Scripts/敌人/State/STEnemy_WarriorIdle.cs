using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class STEnemy_WarriorIdle : State
{
    [ReadOnly]
    public float time;
    public float time_TransAttack;
    public string[] hitSFXSet = Array.Empty<string>();

    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();
        if (hitSFXSet.Length>0)
        {
            PlayHitSFX();
        }

        //普攻CD
        time = 0f;
    }

    public override void UPStateBehaviour(Entity entity)
    {
        time += Time.deltaTime;//更新攻击CD时间
    }

    public override void UPStateInit(Entity entity)
    {

    }
    
    private void PlayHitSFX()
    {
        int index = Random.Range(0,hitSFXSet.Length);
        SoundManager_New.PlayIfFinish(hitSFXSet[index]);
    }

    //---------------------方法------------------------

    public bool Attack()
    {
        if (time >= time_TransAttack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
