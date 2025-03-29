using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenu("Custom-PostProcess/RadialBlur")]
public class PostProcessingRadialBlur : VolumeComponent, IPostProcessComponent
{
    public ClampedIntParameter loop = new ClampedIntParameter(5, 1, 15);
    public ClampedFloatParameter blur = new ClampedFloatParameter(3, 0, 8);
    public ClampedFloatParameter radialSmoothness = new ClampedFloatParameter(10, 2, 20);
    public ClampedIntParameter downsample = new ClampedIntParameter(2, 1, 5);
    public Vector2Parameter radialCenter = new Vector2Parameter(new Vector2(0.5f, 0.5f));

    public BoolParameter enableEffect = new BoolParameter(false);

    public bool IsActive() => enableEffect.value;
    public bool IsTileCompatible() => false;
}
