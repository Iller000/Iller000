using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] int damage = 1;

    void OnTriggerEnter(Collider other)
    {
        Damageable d = other.GetComponent<Damageable>();
        if (d != null)
            d.Damage(damage);
    }
}
