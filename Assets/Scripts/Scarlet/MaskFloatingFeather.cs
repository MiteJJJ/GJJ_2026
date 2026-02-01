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
        Vector3 startPos = transform.position;
        float time = 0;

        while (true)
        {
            time += Time.deltaTime;
            float offset = Mathf.Sin(time / floatDuration * Mathf.PI * 2) * floatDistance;
            transform.position = new Vector3(startPos.x, startPos.y + offset, startPos.z);
            yield return null;
        }
    }
}