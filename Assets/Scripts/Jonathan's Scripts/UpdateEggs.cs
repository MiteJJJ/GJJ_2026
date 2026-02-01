using TMPro;
using UnityEngine;

public class UpdateEggs : MonoBehaviour
{
    [Header("Egg Settings")]
    public int maxEggs = 3;

    [Header("UI")]
    public TMP_Text currentEggText;
    public TMP_Text totalEggText;

    int currentEggs = 0;
    int totalEggs = 0;

    public static int FinalScore { get; private set; }

    void Start()
    {
        UpdateUI();
    }

    public bool CanPickUp()
    {
        return currentEggs < maxEggs;
    }

    public void PickUpEgg()
    {
        if (!CanPickUp()) return;

        currentEggs++;
        Debug.Log("Picked up Egg. Carrying: " + currentEggs);
        UpdateUI();
    }

    public void DepositEggs()
    {
        if (currentEggs <= 0) return;

        totalEggs += currentEggs;
        FinalScore = totalEggs;
        Debug.Log("Deposited " + currentEggs + " eggs. Total score: " + totalEggs);
        currentEggs = 0;
        UpdateUI();
    }

    public int GetCurrentEggs() { return currentEggs; }
    public int GetTotalEggs() { return totalEggs; }

    void UpdateUI()
    {
        if (currentEggText)
            currentEggText.text = "Current: " + currentEggs;

        if (totalEggText)
            totalEggText.text = "Total Eggs: " + totalEggs;
    }
}
