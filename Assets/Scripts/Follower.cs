using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] AnimationCurve speedOverDistance;
    [SerializeField] Transform followThis;

    [SerializeField] float energyUsedOverDistance = 0.2f;
    [SerializeField] float energyRegeneratedOverTime = 0.2f;

    float _energy = 1;
    bool _charging = false;

    void Update()
    {
        if (followThis == null)
            return;

        if (_charging)
        {
            _energy += Time.deltaTime * energyRegeneratedOverTime;
            if (_energy >= 1)
            {
                _energy = 1;
                _charging = false;
            }
        }
        else
        {
            float movemen = Move();
            _energy -= movemen * energyUsedOverDistance;
            if (_energy <= 0)
            {
                _energy = 0;
                _charging = true;
            }
        }
    }

    float Move()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = followThis.position;

        float distance = Vector3.Distance(startPos, targetPos);
        float speed = speedOverDistance.Evaluate(distance);

        float movementDistance = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(startPos, targetPos, movementDistance);

        Vector3 lookDir = targetPos - transform.position;

        if (lookDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDir);
            return movementDistance;
        }

        return 0;
    }
}