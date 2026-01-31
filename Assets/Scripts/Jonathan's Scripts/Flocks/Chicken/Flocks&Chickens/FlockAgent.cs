using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }

    Rigidbody chickenRigidbody;

    [Range(1f, 100f)]
    public float minEscapeForce = 1f;
    [Range(1f, 100f)]
    public float maxEscapeForce = 4f;

    float normalForce;
    float escapeForce;
    public float escapeMultiplyer = 120f;

    [Header("Rotation Physics")]
    [Tooltip("Torque multiplier used to rotate the agent toward movement direction")]
    public float rotationTorque = 10f;
    [Tooltip("Damping applied to angular velocity (higher = faster settling)")]
    public float rotationDamping = 2f;

    Animator animator;
    int scalarHash;
    float animatorScalar;

    float baseSpeed = 1f;
    float currentSpeed;

    Vector3 desiredDirection;

    void Awake()
    {
        agentCollider = GetComponent<Collider>();
        chickenRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        scalarHash = Animator.StringToHash("Scalar");

        normalForce = Random.Range(minEscapeForce, maxEscapeForce);
        escapeForce = escapeMultiplyer * normalForce;
    }


    private void Update()
    {
        // read speed from the physics velocity and update animator
        currentSpeed = chickenRigidbody.linearVelocity.magnitude;
        animatorScalar = currentSpeed / baseSpeed;
        animator.SetFloat(scalarHash, animatorScalar);
    }

    private void FixedUpdate()
    {
        // use physics (torque) to rotate toward the behavior direction on XZ plane
        if (desiredDirection.sqrMagnitude > 0.001f)
        {
            Vector3 currentForward = transform.forward;
            currentForward.y = 0f;
            if (currentForward.sqrMagnitude < 0.0001f) currentForward = Vector3.forward;

            float angle = Vector3.SignedAngle(currentForward, desiredDirection.normalized, Vector3.up);

            // proportional controller: convert angle (deg) to a torque value
            float torque = angle * Mathf.Deg2Rad * rotationTorque;

            // apply torque around Y and damp existing angular velocity
            Vector3 targetTorque = Vector3.up * torque;
            Vector3 damping = -chickenRigidbody.angularVelocity * rotationDamping;
            chickenRigidbody.AddTorque(targetTorque + damping, ForceMode.Acceleration);
        }
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }
    public void Move(Vector3 direction)
    {
        direction.y = 0f;
        desiredDirection = direction;
        chickenRigidbody.AddForce(transform.forward * normalForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 flee = (transform.position - agentFlock.player.position);
            flee.y = 0f;
            chickenRigidbody.AddForce(flee.normalized * escapeForce);
        }
    }
}
