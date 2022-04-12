using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
    [SerializeField] Bounds range;
    [SerializeField] float rotation;

    public void Teleport()
    {
        float randomX = Random.Range(range.min.x, range.max.x);
        float randomY = Random.Range(range.min.y, range.max.y);
        float randomZ = Random.Range(range.min.z, range.max.z);

        transform.position =
            new Vector3(randomX, randomY, randomZ);
    }

    void Update()
    {
        transform.Rotate(
            Vector3.forward,
            rotation * Time.deltaTime);
    }
}
