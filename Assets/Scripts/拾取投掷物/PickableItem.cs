using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public Rigidbody2D _rb;
    public void Picked(Entity entity)
    {
        Debug.Log(entity.name);
        transform.SetParent(entity.transform, true);
        transform.localPosition = Vector3.zero;
        _rb.simulated = false;
    }

    public void Throw(Entity entity)
    {

    }
}
