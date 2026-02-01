using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class IncomingAttack : MonoBehaviour
{
    public GameObject fox = null;
    LineRenderer lineRenderer;

    [SerializeField]
    public GameObject bulletPrefab;

    public float aimTime = 3.0f;
    public AudioManager audioManager;

    bool flag = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SpawnAfterDelay();
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            // follow fox posiiton
            lineRenderer.SetPosition(1, fox.transform.position + Vector3.up * 5.0f);
        }

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

            if (audioManager != null)
            {
                audioManager.PlayGun();
            }

            CameraShake shake = Camera.main.GetComponent<CameraShake>();
            shake.Shake(1, 10f);
        }
        else
        {
            Debug.LogWarning("No prefab assigned to spawn!");
        }

        flag = false;
        Destroy(gameObject, 1f);
    }
}
