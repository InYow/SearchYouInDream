using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class STEnemy_ZaBingShoot : State
{
    public GameObject projectilePrefab;

    private Enemy_ShooterBase shootEnemy;
    private Transform playerTransform;
    private float startTime;
    
    private Vector3 playerDirection;
    
    public override void StateStart(Entity entity)
    {
        shootEnemy = (Enemy_ShooterBase)entity;
        shootEnemy.StartShootCD();
        playerTransform  = shootEnemy.target;
        
        UpdateFaceDirection();
        
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();
    }

    public override void UPStateInit(Entity entity)
    { }

    public override void UPStateBehaviour(Entity entity)
    { }

    public override void StateExit(Entity entity)
    {
        Destroy(this.gameObject);
    }
    
    public void FireBullet()
    {
        var bulletSpawnPosition = shootEnemy.GetProjectileSpawnTransform();
        Vector3 dir = playerDirection.x > 0 ? Vector3.one : new Vector3(-1, 1, 1);
        
        GameObject bullet = Instantiate(projectilePrefab, bulletSpawnPosition.position, Quaternion.identity);
        bullet.transform.localScale = dir; 
        
        var projectile = bullet.GetComponent<ProjectileBase>();
        projectile.entity_master = shootEnemy;
        projectile.EmmitProjectile(playerDirection.x > 0 ? Vector3.right : Vector3.left);
        
    }
    
    private void UpdateFaceDirection()
    {
        playerDirection = Vector3.Normalize(shootEnemy.target.position - shootEnemy.transform.position);
        if (playerDirection.x < 0)
        {
            shootEnemy.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(playerDirection.x > 0)
        {
            shootEnemy.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
