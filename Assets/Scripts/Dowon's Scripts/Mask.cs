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

    public GameObject chickenMask;
    public bool maskOn;
    public AudioManager audioManager;

    public void Start()
    {
        UpdateFeatherUI();
        chickenMask.SetActive(false);
        maskOn = false;
    }

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
        else
        {
            Debug.Log("Not enough feathers! Currently " + featherCount);
        }
    }

    void Update()
    {
        if (this == null) return;

        if (Fox.Masked)
        {
            maskTime -= Time.deltaTime;
            maskTime = Mathf.Max(maskTime, 0f);
            UpdateFeatherUI();
        }
    }

    //pickup feather
    public void incrementFeather()
    {
        if (Fox.Masked)
        {
            // extend mask duration
            maskTime += timePerFeather;
            Debug.Log("Extended mask time by " + timePerFeather + "s");
            UpdateFeatherUI();
            if (audioManager != null)
            {
                audioManager.PlayFeatherCollect();
            }
            return;
        }

        // ignore if full
        if (featherCount == featherMax)
        {
            return;
        }

        Debug.Log("Picked up feather");
        featherCount++;
        featherCount = Math.Min(featherCount, featherMax);
        UpdateFeatherUI();
        if (audioManager != null)
        {
            audioManager.PlayFeatherCollect();
        }
    }

    public void RefillFeathers()
    {
        featherCount = featherMax;
        UpdateFeatherUI();
    }

    private void EnterMaskedState()
    {
        Fox.Masked = true;

        if (maskRoutine != null)
            StopCoroutine(maskRoutine);

        maskTime = featherCount * timePerFeather;
        featherCount = 0;
        maskRoutine = StartCoroutine(MaskTimer());

        Debug.Log("Entered masked state");
        chickenMask.SetActive(true);
        maskOn = true;
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
        chickenMask.SetActive(false);
        maskOn = false;
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
        while (maskTime > 0f)
        {
            yield return null;
        }
        ExitMaskedState();
    }
}
