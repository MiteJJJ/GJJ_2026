using System;
using System.Collections;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    [SerializeField]
    GameObject fox;

    [SerializeField]
    GameObject incomingAttackPF;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;  // Time between spawns in seconds
    public float spawnIntervalDecrease = 0.1f;
    public float spawnIntervalMin = 0.2f;
    public float radius = 500f;

    void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnAtRandomCirclePoint();
            yield return new WaitForSeconds(Math.Max(spawnInterval - spawnIntervalDecrease, spawnIntervalMin));
        }
    }

    private void SpawnAtRandomCirclePoint()
    {
        if (incomingAttackPF == null)
        {
            Debug.LogWarning("No prefab assigned to spawn!");
            return;
        }

        // Choose a random angle
        float angle = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;

        // Convert polar coordinates to Cartesian coordinates
        Vector3 spawnPos = new Vector3(
            transform.position.x + radius * Mathf.Cos(angle),
            3.5f,
            transform.position.z + radius * Mathf.Sin(angle)
        );

        // Spawn the prefab at the random position
        GameObject go = Instantiate(incomingAttackPF, spawnPos, Quaternion.identity);
        IncomingAttack atk = go.GetComponent<IncomingAttack>();
        atk.fox = fox;
        LineRenderer line = go.GetComponent<LineRenderer>();
        line.SetPosition(0, spawnPos);
    }

    void Update()
    {
        
    }
}
