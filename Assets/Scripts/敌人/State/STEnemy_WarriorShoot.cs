using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class STEnemy_WarriorShoot : State
{
    //public SignalReceiver signalReceiver;
    public LayerMask obstacleLayer; 
    public GameObject bulletPrefab;

    private Enemy_ShooterBase shootEnemy;
    private Vector3 targetDirection;
    private Vector3 targetPosition;
    private Vector3 playerDirection;

    private bool shouldBreak = false;
    public override void StateStart(Entity entity)
    {
        shouldBreak = false;
        shootEnemy = entity as Enemy_ShooterBase;
        if (shootEnemy == null)
        {
            Debug.LogError($"{this.name} can only use for dash enemy!");
            shouldBreak = true;
        }
        
        shootEnemy.StartShootCD();
        if (!shootEnemy.target)
        {
            shouldBreak = true;
        }

        UpdateFaceDirection();
            
        // shootEnemy._rb.velocity = Vector2.zero;
        //
        // targetDirection = Vector3.Normalize(shootEnemy.target.position-shootEnemy.transform.position);
        // Ray2D ray = new Ray2D(transform.position, targetDirection);
        // RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, shootEnemy.dashDistance, obstacleLayer);
        // if (hit.collider)
        // {
        //     shouldBreak = true;
        // }
        // targetPosition = entity.transform.position + targetDirection*shootEnemy.dashDistance;
        
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();
    }

    public override void UPStateInit(Entity entity) { }
    public override void UPStateBehaviour(Entity entity) { }

    public override void StateExit(Entity entity)
    {
        if (shootEnemy)
        {
            shootEnemy._rb.velocity = Vector2.zero;
        }
        
        Destroy(gameObject);
    }
    
    public override bool Finished(Entity entity)
    {
        
        if (!shootEnemy)
        {
            return true;
        }
        
        if (!shootEnemy.target)
        {
            return true;
        }

        if (shouldBreak)
        {
            shouldBreak = false;
            return true;
        }
        
        return base.Finished(entity);
    }

    public void FireBullet()
    {
        //float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        //Quaternion q = Quaternion.Euler(0, 0, angle);  // 绕Z轴旋转angle度
        var bulletSpawnPosition = shootEnemy.GetProjectileSpawnTransform();
        Vector3 dir = playerDirection.x > 0 ? Vector3.one : new Vector3(-1, 1, 1);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition.position, Quaternion.identity);
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
