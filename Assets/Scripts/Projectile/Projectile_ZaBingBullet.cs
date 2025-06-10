using System;
using System.Collections;
using System.Collections.Generic;
using UI.UISystem.UIFramework;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile_ZaBingBullet : ProjectileBase
{
    public Animator animator;
    public GameObject shadow;
    public GameObject projectileVisual;
    public float landScale = 1.35f;
    public float initialSpeed = 30.0f;
    public float lastDuration = 10.0f;
    public float emmitYawAngle = 30.0f;
    public float gravityScale = 9.8f;
    public GameObject flashMaskPrefab;
    public bool bEnableCheckBoxOnLand = false;
    
    private float heightFromGround;
    private Transform shadowTransform;
    private Transform projectileTransform;
    private Vector3 velocity;
    private Vector3 shadowMoveDirection;
    private float shadwOriginalY;
    private bool isHitGround = false;
    private float hitLandTime;
    private CheckBox checkBox; 
    private void OnEnable()
    {
        checkBox = GetComponentInChildren<CheckBox>();
        if (checkBox != null)
        {
            checkBox.OnHitEntity.AddListener(OnPlayerHit);
        }
    }

    private void OnDisable()
    {
        if (checkBox != null)
        {
            checkBox.OnHitEntity.RemoveListener(OnPlayerHit);
        }
    }

    private void OnPlayerHit()
    {
        if (flashMaskPrefab)
        {
            Instantiate(flashMaskPrefab,UIManager.instance.transform);
        }
    }

    public override void EmmitProjectile(Vector3 direction)
    {
        shadowTransform = shadow.transform;
        projectileTransform = projectileVisual.transform;
        _rb = GetComponent<Rigidbody2D>();
        
        shadwOriginalY = shadowTransform.position.y;
        Vector3 axis = direction.x > 0 ? Vector3.forward : Vector3.back;
        Quaternion rotation = Quaternion.AngleAxis(emmitYawAngle, axis);
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
            if (Time.time-hitLandTime>lastDuration)
            {
                Destroy(this.gameObject);
            }
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

    public override void Stop()
    {
        _rb.velocity = Vector2.zero;    //速度为0
        Destroy(this.gameObject);
    }
    
    private void CalculateHeightAndVelocity()
    {
        heightFromGround = projectileTransform.position.y - shadowTransform.position.y;
        if (heightFromGround <= 0.01f)
        {
            OnHitGround();
        }
    }

    private void SetAnimationParam()
    {
        if (animator != null)
        {
            animator.SetFloat("VelocityY", velocity.y);
            animator.SetFloat("HeightFromGround", heightFromGround);
            if (isHitGround)
            {
                projectileVisual.transform.localScale = Vector3.one * landScale;
            }
        }
    }

    private void OnHitGround()
    {
        isHitGround = true;
        hitLandTime = Time.time;
        
        velocity = Vector3.zero;
        _rb.velocity = Vector2.zero;

        checkBox.enabled = bEnableCheckBoxOnLand;
        Destroy(shadow);
    }
}
