using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public Rigidbody2D _rb;
    public void Picked(Entity entity)
    {
        transform.parent.SetParent(entity.transform);
        transform.localPosition = Vector3.zero;
    }
}
