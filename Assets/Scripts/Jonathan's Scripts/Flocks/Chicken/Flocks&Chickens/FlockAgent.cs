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

    Rigidbody fishRigidbody;

    [Range(1f, 10f)]
    public float minSwimForce = 1f;
    [Range(1f, 10f)]
    public float maxSwimForce = 4f;

    float swimForce;
    float escapeForce;
    public float escapeMultiplyer = 120f;

    Animator animator;
    int scalarHash;
    float animatorScalar;

    float baseSpeed = 1f;
    float currentSpeed;
    ParticleSystem ps;

    Collider circleCollider;

    void Awake()
    {
        agentCollider = GetComponent<Collider>();
        fishRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        scalarHash = Animator.StringToHash("Scalar");

        swimForce = Random.Range(minSwimForce, maxSwimForce);
        escapeForce = escapeMultiplyer * swimForce;
        ps = transform.GetChild(0).GetComponent<ParticleSystem>();
    }


    private void Update()
    {
        currentSpeed = fishRigidbody.linearVelocity.magnitude;
        //set the animation clip in the animator to the playSpeed here
        animatorScalar = currentSpeed / baseSpeed;
        animator.SetFloat(scalarHash, animatorScalar);
        //face the movement direction on the XZ plane
        Vector3 vel = fishRigidbody.linearVelocity;
        vel.y = 0f;
        if (vel.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(vel);
        }
     }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
        circleCollider = agentFlock.player.GetComponent<Collider>();
        ps.trigger.SetCollider(0, circleCollider);
    }
    public void Move(Vector3 direction)
    {
        Vector3 dir = direction;
        dir.y = 0f;
        fishRigidbody.AddForce(dir.normalized * swimForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "paddle")
        {
            // Start the particle system on collision
            ps.gameObject.SetActive(true);
            ps.Play();
            Vector3 flee = (transform.position - agentFlock.player.position);
            flee.y = 0f;
            fishRigidbody.AddForce(flee.normalized * escapeForce);
        }
    }
}
