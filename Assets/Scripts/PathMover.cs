using UnityEngine;

public class PathMover : MonoBehaviour
{
    public float speed;

    public Transform pos1;
    public Transform pos2;

    float _phase;

    void Update()
    {
        Vector3 p1 = pos1.position;
        Vector3 p2 = pos2.position;
        float distance = Vector3.Distance(p1, p2) * 2;
        float frequency = speed / distance;

        _phase += frequency * Time.deltaTime;

        float x = _phase % 2;
        x -= 1;
        x = Mathf.Abs(x);
        transform.position = Vector3.Lerp(p1, p2, x);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 p1 = pos1.position;
        Vector3 p2 = pos2.position;
        Gizmos.DrawSphere(p1, 0.5f);
        Gizmos.DrawSphere(p2, 0.5f);
        Gizmos.DrawLine(p1, p2);
    }
}
