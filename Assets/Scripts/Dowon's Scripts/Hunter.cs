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
    public float spawnInterval = 2f;
    public float spawnIntervalDecrease = 0.1f;
    public float spawnIntervalMin = 0.2f;
    public float radius = 500f;

    private Coroutine spawnRoutine;
    //public AudioManager audioManager;

    void Start()
    {
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            // 🔒 Pause here while masked
            yield return new WaitUntil(() => !Fox.Masked);

            // Spawn
            SpawnAtRandomCirclePoint();

            // Wait for interval (unchanged by masking)
            float waitTime = Math.Max(
                spawnInterval - spawnIntervalDecrease,
                spawnIntervalMin
            );

            yield return new WaitForSeconds(waitTime);
        }
    }

    private void SpawnAtRandomCirclePoint()
    {
        if (incomingAttackPF == null)
        {
            Debug.LogWarning("No prefab assigned to spawn!");
            return;
        }

        float angle = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;

        Vector3 spawnPos = new Vector3(
            transform.position.x + radius * Mathf.Cos(angle),
            3.5f,
            transform.position.z + radius * Mathf.Sin(angle)
        );

        GameObject go = Instantiate(incomingAttackPF, spawnPos, Quaternion.identity);

        IncomingAttack atk = go.GetComponent<IncomingAttack>();
        atk.fox = fox;

        LineRenderer line = go.GetComponent<LineRenderer>();
        line.SetPosition(0, spawnPos);
    }
}
