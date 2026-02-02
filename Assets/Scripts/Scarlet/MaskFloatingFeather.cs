using UnityEngine;
using System.Collections;

public class MaskFloatingFeather : MonoBehaviour
{
    [SerializeField] private float floatDistance = 0.5f;
    [SerializeField] private float floatDuration = 2f;

    void Start()
    {
        StartCoroutine(FloatCoroutine());
    }

    IEnumerator FloatCoroutine()
    {
        Vector3 startLocalPos = transform.localPosition;
        float time = 0;

        while (true)
        {
            time += Time.deltaTime;
            float offset = Mathf.Sin(time / floatDuration * Mathf.PI * 2) * floatDistance;
            transform.localPosition = new Vector3(startLocalPos.x, startLocalPos.y + offset, startLocalPos.z);
            yield return null;
        }
    }
}