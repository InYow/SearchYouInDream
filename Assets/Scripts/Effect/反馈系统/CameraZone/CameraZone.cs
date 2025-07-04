using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public static CameraZone Instance;
    private CinemachineVirtualCamera virtualCamera;
    private float t;
    private CameraZoneData cameraZoneData;

    [Header("预先设置好的camerazonedata")]
    public CameraZoneData 击破敌人时;
    public CameraZoneData 击杀敌人时;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        virtualCamera = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera as Cinemachine.CinemachineVirtualCamera;

        if (cameraZoneData == null)
            return;

        //执行
        if (cameraZoneData.animatorUpdateMode == AnimatorUpdateMode.Normal)
        {
            t += Time.deltaTime;
        }
        else if (cameraZoneData.animatorUpdateMode == AnimatorUpdateMode.UnscaledTime)
        {
            t += Time.unscaledDeltaTime;
        }
        virtualCamera.m_Lens.OrthographicSize = cameraZoneData.animationCurve.Evaluate(t);
        //virtualCamera.GetComponent<CinemachineConfiner2D>().enabled = false;

        //结束条件
        if (t >= cameraZoneData.animationCurve.keys[cameraZoneData.animationCurve.keys.Length - 1].time)
        {
            t = 0f;
            cameraZoneData = null;
            //virtualCamera.GetComponent<CinemachineConfiner2D>().enabled = true;
        }
    }

    public static void CameraZoneUseData(CameraZoneData data)
    {
        Instance.cameraZoneData = data;
        Instance.t = 0f;
    }
}
