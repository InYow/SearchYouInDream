using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenu("Custom-PostProcess/RadialBlur")]
public class PostProcessingRadialBlur : VolumeComponent, IPostProcessComponent
{
    public BoolParameter enableEffect = new BoolParameter(false);
    
    public ClampedIntParameter loop = new ClampedIntParameter(1, 1, 15);
    public ClampedFloatParameter blur = new ClampedFloatParameter(3, 0, 8);
    public ClampedFloatParameter radialSmoothness = new ClampedFloatParameter(10, 2, 20);
    public Vector2Parameter radialCenter = new Vector2Parameter(new Vector2(0.5f, 0.5f));

    public bool IsActive() => enableEffect.value;
    public bool IsTileCompatible() => false;

    protected override void OnDisable()
    {
        base.OnDisable();
        Debug.Log("Removing PostProcessing Radial Blur");
    }
}
