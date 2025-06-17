using System;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

namespace 敌人
{
    public class STEnemy_GetHit : State
    {
        private static int lastAnimIndex =0 ;
        private Enemy enemy;
        
        public PlayableAsset[] asset;
        public string[] hitSFXSet = Array.Empty<string>();
        
        public override void StateStart(Entity entity)
        {
            enemy = entity as Enemy;
            lastAnimIndex = Random.Range(0, asset.Length);

            if (hitSFXSet.Length>0)
            {
                PlayHitSFX();
            }
            
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

        private void PlayHitSFX()
        {
            int index = Random.Range(0,hitSFXSet.Length);
            SoundManager_New.PlayIfFinish(hitSFXSet[index]);
        }
    }
}