using UnityEngine;

public class BFPlayerGuideBreakAttack : Buff
{
    public Entity e_target;     //破防的敌人
    public override void StartBuff(Entity entity)
    {
        base.StartBuff(entity);
        //值
        Player player = entity as Player;
        e_target = player.eTarget_击破;

        //TODO 近镜头、UI提示
        //FIN 慢动作
        SlowMotion.StartSlow(3f, 0f);
        CameraShake.ShakeExplosion(new Vector3(1, 0, 0), 0.3f);

        //UI
        击破Canvas.PlayAnimation();
    }
    public override void UpdateBuff(Entity entity)
    {
        //流逝时间
        if (time <= 0f)
        {
            FinishBuff(entity);
        }
        else
        {
            time = Mathf.Clamp(time - Time.unscaledDeltaTime, 0f, time);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            var currentVirtualCamera = Cinemachine.CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;
            //CameraZone.SetLenOrthoSize(currentVirtualCamera as Cinemachine.CinemachineVirtualCamera, 3.5f);

            击破Canvas.ContinueAnimation();
        }
    }

    public override void FinishBuff(Entity entity)
    {
        base.FinishBuff(entity);
        e_target = null;

        //关闭慢动作、近镜头、UI
        SlowMotion.FinishSlow();
    }

    public override void AddAgain(Entity entity)
    {
        //FIXME 如果在击破buff期间，又有敌人被击破了怎么办？
        base.AddAgain(entity);
    }
}
