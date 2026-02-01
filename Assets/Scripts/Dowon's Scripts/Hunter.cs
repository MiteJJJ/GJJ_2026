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
    public float spawnIntervalInit = 5f;
    public float spawnIntervalDecrease = 0.1f;
    public float spawnIntervalMin = 0.5f;
    public float radius = 500f;

    bool wasMasked = false;

    private Coroutine spawnRoutine;
    public AudioManager audioManager;

    void Start()
    {
        spawnInterval = spawnIntervalInit;
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            // 🔒 Pause here while masked
            yield return new WaitUntil(() => !Fox.Masked);

            if (wasMasked)
            {
                spawnInterval = spawnIntervalInit;
                wasMasked = false;
            }

            // Spawn
            SpawnAtRandomCirclePoint();

            spawnInterval -= spawnIntervalDecrease;
            spawnInterval = Mathf.Max(spawnInterval, spawnIntervalMin);

            yield return new WaitForSeconds(spawnInterval);

            // Detect masking after the wait
            if (Fox.Masked)
            {
                wasMasked = true;
            }
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
        atk.audioManager = audioManager;

        LineRenderer line = go.GetComponent<LineRenderer>();
        line.SetPosition(0, spawnPos);
    }
}
