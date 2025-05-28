using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class AttackRangeCondition : Conditional
{
	public SharedTransform target;
	public float attackRange;
	[Range(0,90)]public float attackAngle;
	
	private bool InAttackRange()
	{
		if (target == null)
		{
			return false;
		}
		
		float cosAngle = Mathf.Cos(attackAngle);
		Vector3 targetDir = (target.Value.position - transform.position);
		float distance = targetDir.magnitude;
		float angle = Vector3.Dot(Vector3.Normalize(targetDir), transform.right*transform.localScale.x); 
		
		return ((angle >= cosAngle) || (angle <= -cosAngle)) && 
		       distance <= attackRange;
	}
	
	public override TaskStatus OnUpdate()
	{
		if (InAttackRange())
		{
			return TaskStatus.Success;	
		}
		return TaskStatus.Failure;
	}
}