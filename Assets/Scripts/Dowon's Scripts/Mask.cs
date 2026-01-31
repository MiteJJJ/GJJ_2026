using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mask : MonoBehaviour
{
    [Header("Mask Settings")]
    public float maskDuration = 5f;

    [SerializeField]
    private int featherCount = 0;
    [SerializeField]
    private int feathersNeeded = 3;
    private Coroutine maskRoutine;

    [Header("UI")]
    public TMP_Text featherCountText = null;
        
    public void OnUseMask()
    {
        if (Fox.Masked)
        {
            ExitMaskedState();
        }
        else if (featherCount >= feathersNeeded)
        {
            EnterMaskedState();
        }
        else
        {
            Debug.Log("Not enough feathers! Currently " + featherCount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Feather"))
        {
            featherCount++;
            featherCount = Math.Min(featherCount, feathersNeeded);
            Destroy(other.gameObject);

            if (featherCountText)
            {
                featherCountText.text = "Feathers: " + featherCount + " / " + feathersNeeded;
            }
            else
            {
                Debug.LogWarning("Set up reference to Feather Count UI");
            }
        }
    }

    private void EnterMaskedState()
    {
        featherCount = 0;
        Fox.Masked = true;

        if (maskRoutine != null)
            StopCoroutine(maskRoutine);

        maskRoutine = StartCoroutine(MaskTimer());

        Debug.Log("Entered masked state");
    }

    private void ExitMaskedState()
    {
        if (maskRoutine != null)
        {
            StopCoroutine(maskRoutine);
            maskRoutine = null;
        }

        Fox.Masked = false;

        Debug.Log("Exited masked state");
    }

    private IEnumerator MaskTimer()
    {
        yield return new WaitForSeconds(maskDuration);
        ExitMaskedState();
    }
}
