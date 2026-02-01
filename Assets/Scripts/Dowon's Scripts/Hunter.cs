using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    [SerializeField]
    GameObject fox;

    [SerializeField]
    GameObject incomingAttackPF;

    [Header("Spawn Settings")]
    public AnimationCurve spawnIntervalCurve;
    // When max difficulty will be reached
    public float curveDuration = 60f;
    // spawn interval at max difficulty
    public float curveMax = 0.1f;
    // spawn interval at min difficulty
    public float curveMin = 5.0f;
    // where on the cureve we are at right now
    [SerializeField]
    float progress = 0.0f;

    bool wasMasked = false;

    public float radius;

    private Coroutine spawnRoutine;
    public AudioManager audioManager;

    void Start()
    {
        progress = 0f;
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private void Update()
    {
        // If masked → reset difficulty and pause
        if (Fox.Masked)
        {
            if (!wasMasked)
            {
                progress = 0f;
                wasMasked = true;
            }
            return;
        }
        else
        {
            progress += Time.deltaTime / curveDuration;
            progress = Mathf.Clamp01(progress);
        }

        wasMasked = false;
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            // 🔒 Pause here while masked
            yield return new WaitUntil(() => !Fox.Masked);

            // Spawn
            SpawnAtRandomCirclePoint();

            float curveValue = spawnIntervalCurve.Evaluate(progress);
            float spawnInterval = Mathf.Lerp(
                curveMin,   // slow (early)
                curveMax,   // fast (late)
                curveValue
            );

            yield return new WaitForSeconds(spawnInterval);
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
