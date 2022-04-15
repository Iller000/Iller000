using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] float force;
    [SerializeField] float radius;
    [SerializeField] float upwardModifier;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MouseDown();        
    }



    void MouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit HitInfo))
         Explode(HitInfo.point);
    }

    
    void Explode(Vector3 position)
    {
        Rigidbody[] allrgb = FindObjectsOfType<Rigidbody>();


        foreach (Rigidbody rgb in allrgb)
        {

            int layer = rgb.gameObject.layer;
            bool explode = (layer & mask) == 0;

            if(explode)
                rgb.AddExplosionForce(force, position, radius, upwardModifier, ForceMode.Impulse);
        }    

    }
}
