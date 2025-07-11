using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class STEnemy_WarriorDieLayOn : State
{
    private Enemy enemy;
    public override void StateExit(Entity entity)
    {
        enemy.behaviourTree.SetVariableValue("bCanExecute", entity.transExecution);

        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();

        enemy = (Enemy)entity;
        enemy._rb.simulated = false;
        enemy.isDead = true;

        //踩血特效
        VisualEffectManager.PlayEffectWithoutRotation(enemy.bloodVFXName, enemy.transform);
        //出血音效
        SoundEffectManager.PlaySFX("出血", enemy.transform);
    }

    public override void UPStateBehaviour(Entity entity)
    {

    }

    public override void UPStateInit(Entity entity)
    {
    }

    public override bool Finished(Entity entity)
    {
        return false;
    }
}
