using UnityEngine;
using System.Collections;

public class EggAnim : MonoBehaviour
{
    [SerializeField] private float initialZRotation = 20f;
    [SerializeField] private float gentleShakeAngle = 5f;
    [SerializeField] private float gentleShakeSpeed = 3f;
    [SerializeField] private float bounceHeight = 1.2f;
    [SerializeField] private float bounceSpeed = 4f;
    [SerializeField] private float cycleDelay = 0f;

    private Vector3 startPos;
    private Quaternion startRotation;

    void Start()
    {
        startPos = transform.position;
        startRotation = Quaternion.Euler(0, 0, initialZRotation);
        transform.rotation = startRotation;
        StartCoroutine(ShakeSequence());
    }

    IEnumerator ShakeSequence()
    {
        yield return new WaitForSeconds(Random.Range(0f, 3f));

        while (true)
        {
            yield return StartCoroutine(GentleShake(3));
            yield return new WaitForSeconds(0.3f);
            yield return StartCoroutine(ViolentBounce(1f));
            yield return new WaitForSeconds(cycleDelay);
        }
    }

    IEnumerator GentleShake(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float elapsed = 0;
            while (elapsed < 1f / gentleShakeSpeed)
            {
                elapsed += Time.deltaTime;
                float angle = Mathf.Sin(elapsed * gentleShakeSpeed * Mathf.PI * 2) * gentleShakeAngle;
                transform.rotation = startRotation * Quaternion.Euler(angle, angle, angle);
                yield return null;
            }
        }
        transform.rotation = startRotation;
    }

    IEnumerator ViolentBounce(float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float offset = Mathf.Abs(Mathf.Sin(elapsed * bounceSpeed * Mathf.PI)) * bounceHeight;
            transform.position = new Vector3(startPos.x, startPos.y + offset, startPos.z);
            yield return null;
        }
        transform.position = startPos;
    }
}