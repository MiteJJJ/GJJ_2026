using UnityEngine;

public class PlayFoxAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;  // or CharacterController, depending on your movement system

    [Header("Movement Detection")]
    [SerializeField] private float movementThreshold = 0.1f;

    [Header("Idle Variation")]
    [SerializeField] private float idleCheckInterval = 2f;  // Check every 2 seconds
    [SerializeField] private float idle1Probability = 0.8f;  // 80% chance for Idle_1

    private float nextIdleCheckTime;
    private bool wasMoving = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();  // Change to CharacterController if needed

        // Set initial idle state
        SetRandomIdle();
        nextIdleCheckTime = Time.time + idleCheckInterval;
    }

    void Update()
    {
        // Check if fox is moving
        bool isMoving = IsMoving();

        // Update IsWalking parameter
        animator.SetBool("IsWalking", isMoving);

        // When transitioning from walking to idle, set random idle
        if (wasMoving && !isMoving)
        {
            SetRandomIdle();
            nextIdleCheckTime = Time.time + idleCheckInterval;
        }

        // Periodically change idle variation while idle
        if (!isMoving && Time.time >= nextIdleCheckTime)
        {
            SetRandomIdle();
            nextIdleCheckTime = Time.time + idleCheckInterval;
        }

        wasMoving = isMoving;
    }

    bool IsMoving()
    {
        // Check velocity to determine if moving
        if (rb != null)
        {
            return rb.linearVelocity.magnitude > movementThreshold;
        }
        return false;
    }

    void SetRandomIdle()
    {
        // 80% chance for Idle_1, 20% chance for Idle_2
        bool useIdle1 = Random.value <= idle1Probability;
        animator.SetBool("IsIdle_1", useIdle1);

        Debug.Log($"Set idle to: {(useIdle1 ? "Idle_1" : "Idle_2")}");
    }

    // Public method to trigger death (call from other scripts)
    public void Die()
    {
        animator.SetBool("IsDead", true);
    }

    // Public method to trigger eating (call from other scripts)
    public void StartEating()
    {
        animator.SetTrigger("PickFeather");  // Assuming this triggers eating
    }
}