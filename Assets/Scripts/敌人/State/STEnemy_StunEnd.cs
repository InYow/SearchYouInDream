using UnityEngine;

namespace 敌人
{
    public class STEnemy_StunEnd : STEnemy_PlayTimelineAsset
    {
        private Enemy enemy;

        public override void StateStart(Entity entity)
        {
            if (!enemy)
            {
                enemy = entity as Enemy;
            }
            entity.transBreakStun = false;
            enemy.behaviourTree.SetVariableValue("bStun", entity.transBreakStun);
            enemy.ExitBreakStun();

            base.StateStart(entity);
        }

        public override void StateExit(Entity entity)
        {
            Debug.LogWarning("ExitBreakStun");
            Destroy(this.gameObject);
        }

    }
}