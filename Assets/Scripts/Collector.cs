using UnityEngine;

public class Collector : MonoBehaviour
{
    int _score = 0;

    void OnTriggerEnter(Collider other)
    {
        Collectable c = other.GetComponent<Collectable>();
        if (c != null)
        {
            _score += c.value;
            Debug.Log("Score: " + _score);
        }

        Coin coin = other.GetComponent<Coin>();
        if (coin != null)
            coin.Teleport();
    }

}
