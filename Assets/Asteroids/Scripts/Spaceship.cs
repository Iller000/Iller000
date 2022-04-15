using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Spaceship : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    [SerializeField] TouchControl TouchControl;
    [SerializeField, Min(0)] float acceleration = 3;
    [SerializeField, Min(0)] float maxSpeed = 5;
    [SerializeField, Min(0)] float angularSpeed = 180;
    [SerializeField, Range(0, 1)] float drag;
    [SerializeField] Projectile projectilePrototype;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D shipCollider2D;
    [SerializeField] float invincibilityTime = 2;
    [SerializeField] int invincibilityFlickCount = 50;
    [SerializeField] Animator animator;



    InputAction _moveForward;
    InputAction _turn;
    Vector2 _movementVector;
    

    void OnValidate()
    {
        if (input == null)
            input = GetComponent<PlayerInput>();
        input.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;


        if (TouchControl == null)
            TouchControl = GetComponent<TouchControl>();

        if (animator == null)
            animator = GetComponent<Animator>();


    }

    void Awake()
    {
       

        _moveForward = input.currentActionMap.FindAction("Movement");
        _turn = input.currentActionMap.FindAction("Turning");
       
    }

    void OnEnable()
    {
        input.currentActionMap.FindAction("Shoot").started += Shoot;
    }

    private void OnDisable()
    {
        input.currentActionMap.FindAction("Shoot").started -= Shoot;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Asteroid>())
            Die();    
    }

    void Die()
    {
        Debug.Log("Au");

        //gameObject.SetActive(false);

        animator.SetBool("IsAlive", false);

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        _movementVector = Vector3.zero;
        //MakeInvincible();

        AsteriodsGameManager.SpaceshipDied(this);
        
    }

    private void MakeInvincible()
    {
        StartCoroutine(InvincibilityCoroutine());
    }

    IEnumerator InvincibilityCoroutine()
    {
        shipCollider2D.enabled = false;
        Color transparent1 = new Color(1, 1, 1, 0.2f);
        Color transparent2 = new Color(1, 1, 1, 0.5f);

        float flickTime = invincibilityTime / invincibilityFlickCount;

        for (int i = 0; i < invincibilityFlickCount; i++)
        {
            spriteRenderer.color = i % 2 == 0  ? transparent1 : transparent2;
            yield return new WaitForSeconds(flickTime);
        }
        
        spriteRenderer.color = Color.white;
        shipCollider2D.enabled = true;
    }

    void Shoot(InputAction.CallbackContext obj)
    {
        Shoot();
    }

    void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrototype.gameObject);

        newProjectile.GetComponent<Projectile>()
            .Setup(transform.up, transform.position);
        
    }
    

    void Update()
    {
        transform.position += (Vector3)_movementVector * Time.deltaTime;
    }

    void FixedUpdate()
    {
        float forward = TouchControl.Forward != 0
            ? TouchControl.Forward
            : _moveForward.ReadValue<float>();

        float turn = TouchControl.Turn != 0
            ? TouchControl.Turn
            : _turn.ReadValue<float>();

        SimulatePhysics(forward, turn);
    }


   
   
    




    void SimulatePhysics(float forward, float turn)
    {
        if (forward != 0)
        {
            _movementVector += (Vector2)transform.up * acceleration * Time.fixedDeltaTime;
            if (_movementVector.magnitude > maxSpeed)
             _movementVector = _movementVector.normalized * maxSpeed;
        }

        _movementVector *= Mathf.Pow(1 - drag, Time.fixedDeltaTime);

        if (turn != 0)
            transform.Rotate(new Vector3(0, 0, angularSpeed * Time.fixedDeltaTime * -turn));
                    
    }

}