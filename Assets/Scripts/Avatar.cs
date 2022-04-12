using UnityEngine;

public class Avatar : MonoBehaviour
{
    [SerializeField] Damageable damagable;

    public float speed;
    public float rotationSpeed = 90;

    void Update()
    {
        if (!damagable.IsAlive())
            return;

        Vector3 inputVector = InputVector();
        Move(inputVector);
        Turn(inputVector);
    }

    Vector3 InputVector()
    {
        bool up = Input.GetKey(KeyCode.UpArrow);
        bool down = Input.GetKey(KeyCode.DownArrow);
        bool right = Input.GetKey(KeyCode.RightArrow);
        bool left = Input.GetKey(KeyCode.LeftArrow);

        float deltax = (right ? 1 : 0) + (left ? -1 : 0);
        float deltaz = (up ? 1 : 0) + (down ? -1 : 0);
        Vector3 movement = new Vector3(deltax, 0, deltaz);
        movement.Normalize();
        return movement;
    }

    void Move(Vector3 movementVector)
    {
        if (movementVector == Vector3.zero)
            return;

        movementVector *= speed * Time.deltaTime;
        transform.Translate(movementVector, Space.World);
    }

    void Turn(Vector3 movement)
    {

        if (movement == Vector3.zero)
            return;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(movement);
        transform.rotation = Quaternion.RotateTowards(
            startRotation,
            targetRotation,
            rotationSpeed * Time.deltaTime);
    }
}
