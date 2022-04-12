using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTeleporter : MonoBehaviour
{   
    enum BoundType
    {
        RendererBased,
        ColliderBased,
    }

    [SerializeField] new Camera camera;
    [SerializeField] new Renderer renderer;
    [SerializeField] new Collider2D collider;
    [SerializeField] BoundType boundType;


    void OnValidate()
    {
        if (camera == null)
            camera = Camera.main;
        if (renderer == null)
            renderer = GetComponent<Renderer>();
        if (collider == null)
            collider = GetComponent<Collider2D>();

        
        
        
    }

  

   
    void Update()
    {
        float screenExtentY = camera.orthographicSize;
        float screenExtentX = screenExtentY * camera.aspect;
        Vector3 screenCenter = camera.transform.position;
        Vector3 screenSize = new Vector3(screenExtentX * 2, screenExtentY * 2);

        Bounds screenBound = new Bounds(screenCenter, screenSize);
        Bounds selfBound = GetBound();

        Vector3 center = selfBound.center;
        float selfExtentX = selfBound.extents.x;
        float selfExtentY = selfBound.extents.y;

        if (selfBound.max.x < screenBound.min.x) // kilóg balra
            Teleport(screenBound.max.x + selfExtentX, center.y);
        if (selfBound.min.x > screenBound.max.x) // kilóg jobbra
            Teleport(screenBound.min.x - selfExtentX, center.y);

        if (selfBound.max.y < screenBound.min.y) // kilóg fentre
            Teleport(center.x, screenBound.max.y + selfExtentY);
        if (selfBound.min.y > screenBound.max.y) // kilóg lentre
            Teleport(center.x, screenBound.min.y - selfExtentY);

    }

    private void Teleport(float centerX, float centerY)
    {
        Bounds b = GetBound();
        Vector3 offset = transform.position - b.center;
        transform.position = new Vector3(centerX, centerY) + offset;
    }

    Bounds GetBound()
    {
        if (boundType == BoundType.RendererBased)
            return renderer.bounds;
        else
            return collider.bounds;
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Bounds bound = GetBound();
        Gizmos.DrawWireCube(bound.center, bound.size);

        
    }
}
