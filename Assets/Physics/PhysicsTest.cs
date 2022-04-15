using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PhysicsTest : MonoBehaviour
{
    [SerializeField] Vector3 localAddForcePos;
    [SerializeField] Vector3 force;
   
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] bool addForceQuick;
    [SerializeField] bool addForceCountinous;
    bool _lastAddForce;

    void OnValidate()
    {
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody>();
    }


    void Start()
    {
        _lastAddForce = addForceQuick;
    }

    // Update is called once per frame
    void Update()
    {
        if (_lastAddForce != addForceQuick)
        {
            rigidBody.AddForce(force, ForceMode.VelocityChange);
            Vector3 globalPos = transform.TransformPoint(localAddForcePos);
            rigidBody.AddForceAtPosition(force, globalPos, ForceMode.VelocityChange);
            
            
            
            _lastAddForce = addForceQuick;


        }
    }
    void FixedUpdate()
    {
        if (addForceCountinous)
        {
            // rigidBody.AddForce(force, ForceMode.Acceleration);
            //rigidBody.AddRelativeForce(force, ForceMode.Acceleration);

            rigidBody.AddTorque(force, ForceMode.Force);
        }
    }
        
        

}
