using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class RankUpVisualEffect : VisualSequence
{
    public static RankUpVisualEffect instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        RankSystem.rankSystem.ACRankUp += Play;
    }

    public VisualEffect thunderVFX;
    public Vector2 offset;
    public LayerMask layerMask1;
    public LayerMask layerMask2;
    public List<Entity> entities;
    public float slowFactor;
    public float slowTime;
    public void ThunderVFX()
    {
        thunderVFX.gameObject.SetActive(true);
        //set pos
        thunderVFX.gameObject.transform.position = (Vector2)GameManager.Instance.player.transform.position + offset;
    }

    public void ThunderSFX()
    {
        //SoundManager_New.Play();
    }

    public void CameraRender1()
    {
        Camera.main.cullingMask = layerMask1;
    }

    public void CameraRender2()
    {
        Camera.main.cullingMask = layerMask2;
    }

    public void Slow()
    {
        SlowMotion.StartSlow(slowTime, slowFactor);
    }

    private void GetVisibleEntities()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        Entity[] allEntities = FindObjectsOfType<Entity>();
        List<Entity> visibleEntities = new List<Entity>();

        foreach (var entity in allEntities)
        {
            Renderer renderer = entity.GetComponent<Renderer>();
            if (renderer == null)
                continue; // 没有Renderer无法判断

            if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
            {
                visibleEntities.Add(entity);
            }
        }

        entities = visibleEntities;
    }

    public void SetMaterial1()
    {
        GameManager.Instance.player.GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_PureColor", 1f);
        // GetVisibleEntities();
        // foreach (var e in entities)
        // {
        //     Debug.Log(e.name);
        //     e.GetComponent<SpriteRenderer>().material.SetFloat("_PureColor", 1);
        // }
    }

    public void SetMaterial2()
    {
        GameManager.Instance.player.GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_PureColor", 0f);
        // foreach (var e in entities)
        // {
        //     e.GetComponent<SpriteRenderer>().material.SetFloat("_PureColor", 0);
        // }
    }
}