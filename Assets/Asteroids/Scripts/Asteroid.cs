using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] float maxHealth;
    [SerializeField] float startSpeed;
    Vector2 _movementVector;

    [SerializeField, Min(0)] int smallPiecesCount = 3;
    [SerializeField] Asteroid[] smallerPiecesPrototypes;
    
    
    float _currentHealth;
    


    void OnValidate()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Awake()
    {
        float fullCircleInRad = Mathf.PI * 2;
        float randomInRad = Random.Range(0, fullCircleInRad);

        _movementVector = new Vector2(
            Mathf.Cos(randomInRad),
            Mathf.Sin(randomInRad));

        _movementVector *= startSpeed;
        _currentHealth = maxHealth;

    }



    void Update()
    {
        transform.Translate(_movementVector * Time.deltaTime);
    }

    public void Damage(float damage)
    {
        if (_currentHealth <= 0)
            return;

        _currentHealth -= damage;
        UpdateSprite();

        if (_currentHealth <= 0)
                Explode();
            
    }
    void UpdateSprite()
    {
        float _healthRate =  1 - (_currentHealth / maxHealth);

        int index = (int)(sprites.Length * _healthRate);
        index = Mathf.Clamp(index, 0, sprites.Length - 1);

        spriteRenderer.sprite = sprites[index];
    }

    void Explode()
    {
        if (smallerPiecesPrototypes.Length != 0)
        {

            for (int i = 0; i < smallPiecesCount; i++)
            {
                int randomIndex = Random.Range(0, smallerPiecesPrototypes.Length);
                Asteroid proto = smallerPiecesPrototypes[randomIndex];
                Asteroid newAsteroid = Instantiate(proto);
                newAsteroid.transform.position = transform.position;
            }
        }
           
        

        Destroy(gameObject);
            

    }
}
