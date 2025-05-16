using UnityEngine;

[CreateAssetMenu(fileName = "CameraZoneData", menuName = "CameraZoneData", order = 0)]
public class CameraZoneData : ScriptableObject
{
    public AnimationCurve animationCurve;

    public AnimatorUpdateMode animatorUpdateMode = AnimatorUpdateMode.Normal;
}
