using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class STEnemy_TieTouShoot : State
{
    public GameObject projectilePrefab;
    public PlayableAsset shootStartAsset;
    public PlayableAsset shootLoopAsset;
    public float followSpeed = 1.0f;
    public float laserDuration;
    
    private GameObject laser;
    private Enemy_ShooterBase enemy;
    private Transform playerTransform;
    private float startTime;
    private bool bFinishShoot = false;
    
    public override void StateStart(Entity entity)
    {
        bFinishShoot = false;
        
        playableDirector.playableAsset = shootStartAsset;
        playableDirector.extrapolationMode = DirectorWrapMode.Hold;
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();
        
        enemy = (Enemy_ShooterBase)entity;
        enemy.StartShootCD();
        playerTransform  = enemy.target;
    }

    public override void UPStateInit(Entity entity)
    { }

    public override void UPStateBehaviour(Entity entity)
    {
        if (laser)
        {
            //Update Rotation
            if (enemy.bFoundPlayer)
            {
                Vector3 dir = (playerTransform.position - enemy.transform.position).normalized;
                
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rotation = laser.transform.rotation;
                Quaternion q = Quaternion.Euler(0, 0, angle);
                Quaternion target = Quaternion.Slerp(rotation,q,followSpeed*0.01f);
                laser.transform.rotation = target;
                
                if (dir.x > 0)
                {
                    entity.transform.localScale = new Vector3(1, 1, 1);
                    laser.transform.parent.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    entity.transform.localScale = new Vector3(-1, 1, 1);
                    laser.transform.parent.localScale = new Vector3(-1, 1, 1);
                }
            }

            if (Time.time - startTime >= laserDuration)
            {
                Destroy(laser.gameObject);
                laser = null;
                bFinishShoot = true;
            }
        }
    }

    public override void StateExit(Entity entity)
    {
        if (laser)
        {
            Destroy(laser.gameObject);
            laser = null;
        }
        Destroy(this.gameObject);
    }

    public void SpawnLaser()
    {
        Vector3 dir = (playerTransform.position - enemy.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.Euler(0, 0, angle);

        var projectileSpawnTransform = enemy.GetProjectileSpawnTransform();
        laser = Instantiate(projectilePrefab, projectileSpawnTransform.position, q);
        laser.transform.SetParent(projectileSpawnTransform);
        startTime = Time.time;
    }

    public void OnShootStartEnd()
    {
        playableDirector.playableAsset = shootLoopAsset;
        playableDirector.extrapolationMode = DirectorWrapMode.Loop;
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();
    }

    public override bool Finished(Entity entity)
    {
        return bFinishShoot;
    }
}
