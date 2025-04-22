using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Entity entity_master;
    public Rigidbody2D _rb;
    
    public virtual void Stop()
    {
        _rb.velocity = Vector2.zero;    //速度为0
    }
    
    public virtual void EmmitProjectile(Vector3 direction)
    {
    }
}
