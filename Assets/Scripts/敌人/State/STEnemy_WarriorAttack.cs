using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class STEnemy_WarriorAttack : State
{
    public string[] hitSFXSet = Array.Empty<string>();
    
    public override void StateExit(Entity entity)
    {
        Destroy(gameObject);
    }

    public override void StateStart(Entity entity)
    {
        Enemy enemy = entity as Enemy;
        if (enemy != null)
        {
            Vector3 targetDir = enemy.target.transform.position - enemy.transform.position;
            enemy.transform.localScale = targetDir.x > 0 ? Vector3.one : new Vector3(-1, 1, 1);
        }

        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();
        PlayHitSFX();
    }

    public override void UPStateBehaviour(Entity entity) { }

    public override void UPStateInit(Entity entity) { }
    
    private void PlayHitSFX()
    {
        int index = Random.Range(0,hitSFXSet.Length);
        SoundManager_New.PlayOneshot(hitSFXSet[index]);
    }
}
