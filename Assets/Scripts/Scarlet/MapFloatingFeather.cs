using UnityEngine;
using System.Collections;

public class MapFloatingFeather : MonoBehaviour
{
    [SerializeField] private float initialZRotation = 40f;
    [SerializeField] private float floatDistance = 0.5f;
    [SerializeField] private float floatSpeed = 2f;
    [SerializeField] private float rotateSpeed = 50f;

    void Start()
    {
        StartCoroutine(Float());
    }

    IEnumerator Float()
    {
        Vector3 startPos = transform.position;
        float time = Random.Range(0f, 100f);
        float rotationOffset = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0, rotationOffset, initialZRotation);

        while (true)
        {
            time += Time.deltaTime;
            transform.position = new Vector3(startPos.x, startPos.y + Mathf.Sin(time * floatSpeed) * floatDistance, startPos.z);
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }
}