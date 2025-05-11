using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile_ZaBingBullet : ProjectileBase
{
    public Animator animator;
    public GameObject shadow;
    public GameObject projectileVisual;
    public float initialSpeed = 30.0f;
    public float emmitYawAngle = 30.0f;
    public float gravityScale = 9.8f;
    
    private float heightFromGround;
    private Transform shadowTransform;
    private Transform projectileTransform;
    private Vector3 velocity;
    private Vector3 shadowMoveDirection;
    private float shadwOriginalY;
    private bool isHitGround = false;

    public override void EmmitProjectile(Vector3 direction)
    {
        shadowTransform = shadow.transform;
        projectileTransform = projectileVisual.transform;
        _rb = GetComponent<Rigidbody2D>();
        
        shadwOriginalY = shadowTransform.position.y;
        Quaternion rotation = Quaternion.AngleAxis(emmitYawAngle, Vector3.forward);
        Vector3 velocityDirection = (rotation * direction).normalized;
        velocity = velocityDirection * initialSpeed; // 初始速度
        
        // 设置刚体的初速度
        _rb.simulated = true;
        _rb.velocity = velocity; // 设置刚体速度

        // 更新子弹动画
        UpdateAnimation();
    }
    
    void Update()
    {
        if (isHitGround)
        {
            //Destroy(this.gameObject);
        }
        else
        {
            if (_rb != null)
            {
                velocity.y -= gravityScale * Time.deltaTime;
                _rb.velocity = velocity;
            }

            // 更新阴影的位置
            var vector3 = shadowTransform.position;
            vector3.y = shadwOriginalY;
            shadowTransform.position = vector3;
        
            // 更新动画
            UpdateAnimation();
        }
    }
    
    private void UpdateAnimation()
    {
        CalculateHeightAndVelocity();
        SetAnimationParam();
    }

    private void CalculateHeightAndVelocity()
    {
        heightFromGround = projectileTransform.position.y - shadowTransform.position.y;
        if (heightFromGround <= 0.1f)
        {
            isHitGround = true;
            velocity = Vector3.zero;
            _rb.velocity = Vector2.zero;
        }
    }

    private void SetAnimationParam()
    {
        if (animator != null)
        {
            animator.SetFloat("VelocityY", velocity.y);
            animator.SetFloat("HeightFromGround", heightFromGround);
        }
    }
}
