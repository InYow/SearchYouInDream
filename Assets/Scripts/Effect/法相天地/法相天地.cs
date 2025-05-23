using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 法相天地 : MonoBehaviour
{
    public SpriteRenderer originRenderer;
    public SpriteRenderer copyRenderer;

    private Transform originTransform;
    private Material shadowMaterial;
    private int positionID;
    private int directionID;
    private void Start()
    {
        originTransform = transform.parent;
        if (!shadowMaterial && copyRenderer)
        {
            shadowMaterial =  copyRenderer.material;
            positionID = Shader.PropertyToID("_PlayerPosition");
            directionID = Shader.PropertyToID("_PlayerFront");
        }
    }
    
    void Update()
    {
        copyRenderer.sprite = originRenderer.sprite;
        shadowMaterial.SetVector(positionID,originTransform.position);
        shadowMaterial.SetFloat(directionID,originTransform.localScale.x);
    }

    private void OnValidate()
    {
        copyRenderer.sprite = originRenderer.sprite;
    }
}
