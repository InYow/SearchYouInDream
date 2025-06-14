using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class STEnemy_BossDash : State
{
    public LayerMask obstacleLayer;
    //public PlayableAsset dashStartAsset;
    public PlayableAsset dashLoopAsset;
    public float stopDistance = 2.5f;
    
    private Enemy_DashBase dashEnemy;
    private Vector3 targetDirection;
    private Vector3 targetPosition;

    private bool shouldBreak = false;
    private float maxDashTime = 3.5f;
    private float startDashTime;
    private bool isDashing = false;
    private bool isDashEnd = false;
    
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
        if (targetDirection.x < 0)
        {
            dashEnemy.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            dashEnemy.transform.localScale = new Vector3(1, 1, 1);
        }
        isDashing = true;
        startDashTime = Time.time;
        playableDirector.playableAsset = dashLoopAsset;
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();
    }

    public override void UPStateInit(Entity entity) { }

    public override void UPStateBehaviour(Entity entity)
    {
        if (isDashing)
        {
            if (Time.time - startDashTime > maxDashTime)
            {
                dashEnemy._rb.velocity = Vector2.zero;
                isDashing = false;
                isDashEnd = true;
                //playableDirector.playableAsset = dashEndAsset;
                //playableDirector.extrapolationMode = DirectorWrapMode.Hold;
                //BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
                //playableDirector.Play();
            }
            
            float distance = (entity.transform.position - targetPosition).magnitude;
            if (distance <= stopDistance)
            {
                dashEnemy._rb.velocity = Vector2.zero;
                isDashing = false;
                isDashEnd = true;
                //playableDirector.playableAsset = dashEndAsset;
                //playableDirector.extrapolationMode = DirectorWrapMode.Hold;
                //BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
                //playableDirector.Play();
            }
            else
            {
                dashEnemy._rb.velocity = dashEnemy.dashSpeed * targetDirection;
            }
        }
    }

    public override void StateExit(Entity entity)
    {
        if (dashEnemy)
        {
            dashEnemy._rb.velocity = Vector2.zero;
        }

        isDashing = false;
        isDashEnd = false;
        
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
        
        return isDashEnd;
    }

    public void OnDashStartEnd()
    {
        playableDirector.playableAsset = dashLoopAsset;
        playableDirector.extrapolationMode = DirectorWrapMode.Loop;
        BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
        playableDirector.Play();
        isDashing = true;
    }
    
    public void OnDashEndFinish()
    {
        isDashEnd = true;
    }
}
