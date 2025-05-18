using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

[System.Serializable, VolumeComponentMenu("Custom/GlassBreakEffect")]
public class PostProcessingBrokenGlass : VolumeComponent, IPostProcessComponent
{
    public BoolParameter enableEffect = new BoolParameter(false);
    public ClampedFloatParameter glassTint = new ClampedFloatParameter(0f, 0f, 1f);
    public TextureParameter glassMask = new TextureParameter(null);
    public ColorParameter glassColor = new ColorParameter(Color.white);
    
    public bool IsActive() => enableEffect.value && glassTint.value > 0f && glassMask.value != null;
    public bool IsTileCompatible() => false;
}
