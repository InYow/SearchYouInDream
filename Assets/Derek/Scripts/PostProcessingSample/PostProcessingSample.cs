using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingSample : MonoBehaviour
{
    public Volume volume;//Global Volume
    [Range(1,15)]public int radialBlurLoop = 1; 
    
    private PostProcessingRadialBlur radialBlur; //要控制的后处理对象
    private Bloom bloom;//要控制的后处理对象
    
    private void Start()
    {
        if (volume.profile.TryGet(out radialBlur)) // 获取GlobalVolume Profile中的后处理效果
        {
            Debug.Log("RadialBlur found!");
        }
        
        if (volume.profile.TryGet(out bloom))
        {
            Debug.Log("Bloom found!");
        }
        else
        {
            Debug.Log("No Radial Blur found!");
        }
    }

    private void Update()
    {
        // 更新后处理效果参数
        if (radialBlur)
        {
            radialBlur.loop.value = radialBlurLoop;
        }
    }
}