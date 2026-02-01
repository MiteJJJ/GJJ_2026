using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mask : MonoBehaviour
{
    [Header("Mask Settings")]
    public float timePerFeather = 8f;

    [SerializeField]
    private int featherCount = 3;
    [SerializeField]
    private int featherMax = 3;

    private Coroutine maskRoutine;

    [Header("UI")]
    public TMP_Text featherCountText = null;

    float maskTime = 0f;

    public void Start()
    {
        UpdateFeatherUI();
    }

    public void OnUseMask()
    {
        if (Fox.Masked)
        {
            ExitMaskedState();
        }
        else if (featherCount >= 0)
        {
            EnterMaskedState();
        }
        else
        {
            Debug.Log("Not enough feathers! Currently " + featherCount);
        }
    }

    void Update()
    {
        if (Fox.Masked)
        {
            maskTime -= Time.deltaTime;
            maskTime = Mathf.Max(maskTime, 0f);
            UpdateFeatherUI();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Feather"))
        {
            // ignore if full
            if (featherCount == featherMax)
            {
                return;
            }

            featherCount++;
            featherCount = Math.Min(featherCount, featherMax);
            Destroy(other.gameObject);
            UpdateFeatherUI();
        }
    }

    private void EnterMaskedState()
    {
        Fox.Masked = true;

        if (maskRoutine != null)
            StopCoroutine(maskRoutine);

        maskRoutine = StartCoroutine(MaskTimer());
        maskTime = featherCount * timePerFeather;

        featherCount = 0;

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
        maskTime = 0f;  
        featherCount = 0;
        UpdateFeatherUI();

        Debug.Log("Exited masked state");
    }

    private void UpdateFeatherUI()
    {
        if (!featherCountText)
        {
            Debug.LogWarning("Set up reference to Feather Count UI");
            return;
        }

        if (Fox.Masked)
        {
            featherCountText.text =
                $"Masked: {maskTime:0.0}s";
        }
        else
        {
            featherCountText.text =
                $"Feathers: {featherCount} / {featherMax}";
        }
    }

    private IEnumerator MaskTimer()
    {
        yield return new WaitForSeconds(timePerFeather * featherCount);
        ExitMaskedState();
    }
}
