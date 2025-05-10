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
            
            base.StateStart(entity);
        }

        public override void StateExit(Entity entity)
        {
            base.StateStart(entity);
        }
        
    }
}