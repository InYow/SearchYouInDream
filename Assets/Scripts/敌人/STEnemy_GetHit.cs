using UnityEngine;
using UnityEngine.Playables;

namespace 敌人
{
    public class STEnemy_GetHit : State
    {
        private static int lastAnimIndex =0 ;
        private Enemy enemy;
        
        public PlayableAsset[] asset;
        
        public override void StateStart(Entity entity)
        {
            enemy = entity as Enemy;
            lastAnimIndex = Random.Range(0, asset.Length);
            
            playableDirector.playableAsset = asset[lastAnimIndex];
            BindMethod.BindAnimator(playableDirector, transform.parent.gameObject);
            playableDirector.Play();
        }

        public override void UPStateInit(Entity entity) { }

        public override void UPStateBehaviour(Entity entity) { }

        public override void StateExit(Entity entity)
        {
            enemy.isGetHurt = false;
            enemy.behaviourTree.SetVariableValue("bIsGetHurt",enemy.isGetHurt);
            
            Destroy(this.gameObject);
        }
        
    }
}