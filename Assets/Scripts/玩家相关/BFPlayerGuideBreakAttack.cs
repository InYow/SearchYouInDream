using UnityEngine;

public class BFPlayerGuideBreakAttack : Buff
{
    public Entity e_target;     //破防的敌人
    public bool actived;
    public CameraZoneData noactivedEndZoneData;
    public override void StartBuff(Entity entity)
    {
        base.StartBuff(entity);
        //值
        Player player = entity as Player;
        e_target = player.eTarget_击破;
        actived = false;

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

        if ((Input.GetKeyDown(KeyCode.J) || InputManager.ReadPreInput(KeyCode.J)) && 击破Canvas.IfFinish前半段())
        {
            var currentVirtualCamera = Cinemachine.CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;
            //CameraZone.SetLenOrthoSize(currentVirtualCamera as Cinemachine.CinemachineVirtualCamera, 3.5f);
            time = 99f;
            击破Canvas.ContinueAnimation();
        }
    }

    public override void FinishBuff(Entity entity)
    {
        if (actived)
        {
            base.FinishBuff(entity);
            e_target = null;

            //关闭慢动作、近镜头、UI
            SlowMotion.FinishSlow();
        }
        else
        {
            base.FinishBuff(entity);
            e_target = null;


            //关闭慢动作、近镜头、UI
            SlowMotion.FinishSlow();
            CameraFollow.SetFollow(GameManager.Instance.player.transform);
            CameraZone.CameraZoneUseData(noactivedEndZoneData);
            击破Canvas.ReSetAnimation();
            //开启径向模糊
            RadialBlur.RadialBlurUseData(击破Canvas.Instance.radialBlurData);

        }
    }

    public override void AddAgain(Entity entity)
    {
        //FIXME 如果在击破buff期间，又有敌人被击破了怎么办？
        base.AddAgain(entity);
    }
}
