using System.Collections;
using UnityEngine;

public class IncomingAttack : MonoBehaviour
{
    public GameObject fox = null;
    LineRenderer lineRenderer;

    [SerializeField]
    public GameObject bulletPrefab;

    public float aimTime = 3.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SpawnAfterDelay();
    }

    // Update is called once per frame
    void Update()
    {
        // follow fox posiiton
        lineRenderer.SetPosition(1, fox.transform.position + Vector3.up * 5.0f);

        if (Fox.Masked)
        {
            Destroy(gameObject);
        }
    }

    public void SpawnAfterDelay()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(aimTime); // wait 3 seconds

        Vector3 spawnPoint = lineRenderer.GetPosition(0);

        if (bulletPrefab != null)
        {
            Vector3 direction = lineRenderer.GetPosition(1) - spawnPoint;
            Quaternion spawnRot = Quaternion.LookRotation(direction.normalized);

            Instantiate(bulletPrefab, spawnPoint, spawnRot);
        }
        else
        {
            Debug.LogWarning("No prefab assigned to spawn!");
        }

        Destroy(gameObject);
    }
}
