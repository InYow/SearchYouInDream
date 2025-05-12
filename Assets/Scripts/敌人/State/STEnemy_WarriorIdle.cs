using Sirenix.OdinInspector;
using UnityEngine;
public class STEnemy_WarriorIdle : State
{
    [ReadOnly]
    public float time;
    public float time_TransAttack;

    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

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
