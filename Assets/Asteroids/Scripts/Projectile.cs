using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 10;
    [SerializeField] Vector2 forwardDirection;
    [SerializeField] float duration;
    [SerializeField] float damage;
    Vector2 _movementVector;

    float _borntime;
    void Start()
    {
        _borntime = Time.time;
    }

    void Update()
    {
        transform.Translate(_movementVector * Time.deltaTime, Space.World);

        float age = Time.time - _borntime;
        if (age >= duration)
            Destroy(gameObject);
               
    }

    public void Setup(Vector3 forwardDirection,Vector3 position)
    {
        float angle = Vector2.SignedAngle(Vector2.up, forwardDirection);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        _movementVector = forwardDirection.normalized * speed;
        transform.position = position;
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Asteroid asteroid = other.GetComponent<Asteroid>();
        if (asteroid != null)
        {
            asteroid.Damage(damage);
            Destroy(gameObject);
        }
    }


}
