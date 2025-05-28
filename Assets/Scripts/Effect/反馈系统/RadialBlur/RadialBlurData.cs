using UnityEngine;

[CreateAssetMenu(fileName = "RadialBlurData", menuName = "RadialBlurData", order = 0)]
public class RadialBlurData : ScriptableObject
{
    public AnimationCurve animationCurve;

    public AnimatorUpdateMode animatorUpdateMode = AnimatorUpdateMode.Normal;
}
