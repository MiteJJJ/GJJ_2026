using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float speed = 30f;
    public float lifetime = 5f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRb = collision.rigidbody;

        if (otherRb != null)
        {
            // Direction from bullet to the other object's center of mass
            Vector3 direction = (otherRb.worldCenterOfMass - transform.position).normalized;

            float force = 1f;
            otherRb.AddForceAtPosition(
                direction * force,
                otherRb.worldCenterOfMass,
                ForceMode.Impulse
            );
        }

        Destroy(gameObject);
    }
}
