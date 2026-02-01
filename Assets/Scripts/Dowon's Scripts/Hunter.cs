using UnityEngine;

public class Hunter : MonoBehaviour
{
    [SerializeField]
    GameObject fox;

    [SerializeField]
    GameObject incomingAttackPF;

    [Header("Spawn Settings")]
    public float spawnRadius = 5f;    // Radius of the circle
    public float spawnInterval = 2f;  // Time between spawns in seconds
    public int count = 1;
    public float radius = 1000f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
