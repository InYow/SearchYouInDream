using UnityEngine;

public class STEnemy_WarriorDash : State
{
    public LayerMask obstacleLayer; 
    private Enemy_DashBase dashEnemy;
    private Vector3 targetDirection;
    private Vector3 targetPosition;

    private bool shouldBreak = false;
    public override void StateStart(Entity entity)
    {
        shouldBreak = false;
        dashEnemy = entity as Enemy_DashBase;
        if (!dashEnemy)
        {
            Debug.LogError($"{this.name} can only use for dash enemy!");
            shouldBreak = true;
        }
        
        dashEnemy.StartDashCD();
        if (!dashEnemy.target)
        {
            shouldBreak = true;
        }
        dashEnemy._rb.velocity = Vector2.zero;
        
        targetDirection = Vector3.Normalize(dashEnemy.target.position-dashEnemy.transform.position);
        Ray2D ray = new Ray2D(transform.position, targetDirection);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, dashEnemy.dashDistance, obstacleLayer);
        if (hit.collider)
        {
            shouldBreak = true;
        }
        targetPosition = entity.transform.position + targetDirection*dashEnemy.dashDistance;
        
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();
    }

    public override void UPStateInit(Entity entity) { }

    public override void UPStateBehaviour(Entity entity)
    {
        dashEnemy._rb.velocity = dashEnemy.dashSpeed * targetDirection;
    }

    public override void StateExit(Entity entity)
    {
        if (dashEnemy)
        {
            dashEnemy._rb.velocity = Vector2.zero;
        }
        
        Destroy(gameObject);
    }
    
    public override bool Finished(Entity entity)
    {
        if (!dashEnemy)
        {
            return true;
        }
        
        if (!dashEnemy.target)
        {
            return true;
        }

        if (shouldBreak)
        {
            shouldBreak = false;
            return true;
        }
        
        float distance = (entity.transform.position - targetPosition).magnitude;
        if (distance <= 0.01f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
