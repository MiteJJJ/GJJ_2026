using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mask : MonoBehaviour
{
    [Header("Mask Settings")]
    public float maskDuration = 5f;

    [SerializeField]
    private int featherCount = 0;
    private Coroutine maskRoutine;

    public void OnUseMask()
    {
        if (Fox.Masked)
        {
            ExitMaskedState();
        }
        else if (featherCount > 0)
        {
            EnterMaskedState();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Feather"))
        {
            featherCount++;
            Destroy(other.gameObject);
        }
    }

    private void EnterMaskedState()
    {
        featherCount--;
        Fox.Masked = true;

        if (maskRoutine != null)
            StopCoroutine(maskRoutine);

        maskRoutine = StartCoroutine(MaskTimer());

        Debug.Log("++Entered masked state++");
    }

    private void ExitMaskedState()
    {
        if (maskRoutine != null)
        {
            StopCoroutine(maskRoutine);
            maskRoutine = null;
        }

        Fox.Masked = false;

        Debug.Log("--Exited masked state--");
    }

    private IEnumerator MaskTimer()
    {
        yield return new WaitForSeconds(maskDuration);
        Fox.Masked = false;
        maskRoutine = null;
    }
}
