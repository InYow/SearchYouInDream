using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RadialBlur : MonoBehaviour
{
    public Volume volume;//Global Volume
    private PostProcessingRadialBlur radialBlur; //要控制的后处理对象 
    public CinemachineVirtualCamera virtualCamera;
    public static RadialBlur Instance;
    public float t;
    public RadialBlurData radialBlurData;

    [Header("预先设置好的camerazonedata")]
    public RadialBlurData 击破敌人时;

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
    private void Start()
    {
        volume.profile.TryGet(out radialBlur);
    }

    private void Update()
    {
        virtualCamera = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera as Cinemachine.CinemachineVirtualCamera;

        if (radialBlurData == null)
            return;

        //中心点
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(virtualCamera.Follow.position);
        screenPosition.x /= Screen.width;
        screenPosition.y /= Screen.height;
        // Debug.Log("RadialBlur center: " + screenPosition);
        radialBlur.radialCenter.value = screenPosition;

        //执行
        if (radialBlurData.animatorUpdateMode == AnimatorUpdateMode.Normal)
        {
            t += Time.deltaTime;
        }
        else if (radialBlurData.animatorUpdateMode == AnimatorUpdateMode.UnscaledTime)
        {
            t += Time.unscaledDeltaTime;
        }
        radialBlur.blur.value = radialBlurData.animationCurve.Evaluate(t);

        //结束条件
        if (t >= radialBlurData.animationCurve.keys[radialBlurData.animationCurve.keys.Length - 1].time)
        {
            t = 0f;
            radialBlurData = null;
        }
    }

    public static void RadialBlurUseData(RadialBlurData data)
    {
        Instance.radialBlurData = data;
        Instance.t = 0f;
    }

}
