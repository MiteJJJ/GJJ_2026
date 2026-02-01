using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 20f;       // units per second
    public float lifetime = 10f;     // seconds before self-destruction

    private void Start()
    {
        // Automatically destroy the bullet after lifetime seconds
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move forward in the direction the bullet is facing
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
