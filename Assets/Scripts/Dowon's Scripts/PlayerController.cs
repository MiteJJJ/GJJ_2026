using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float acceleration = 20f;
    public float deceleration = 25f;

    [Header("Rotation")]
    public bool rotateTowardsMovement = true;
    public float rotationSpeed = 720f;

    private Rigidbody rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    // Called automatically by PlayerInput
    public void OnMove(InputValue value)
    {
        moveInput = moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        Vector3 inputDirection = new Vector3(-moveInput.y, 0f, moveInput.x);
        Vector3 targetVelocity = inputDirection * moveSpeed;

        Vector3 velocity = rb.linearVelocity;
        Vector3 velocityChange =
            targetVelocity - new Vector3(velocity.x, 0f, velocity.z);

        float accelRate = inputDirection.magnitude > 0.1f
            ? acceleration
            : deceleration;

        velocityChange = Vector3.ClampMagnitude(
            velocityChange,
            accelRate * Time.fixedDeltaTime
        );

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    void Rotate()
    {
        if (!rotateTowardsMovement || moveInput.sqrMagnitude < 0.001f)
            return;

        // Original movement direction
        Vector3 lookDir = new Vector3(moveInput.x, 0f, moveInput.y);

        // Base rotation
        Quaternion targetRotation = Quaternion.LookRotation(lookDir, Vector3.up);

        // Apply -90 degrees Y rotation
        Quaternion yOffset = Quaternion.Euler(0f, -90f, 0f);
        targetRotation = targetRotation * yOffset;

        // Smoothly rotate
        rb.MoveRotation(
            Quaternion.RotateTowards(
                rb.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            )
        );
    }
}
