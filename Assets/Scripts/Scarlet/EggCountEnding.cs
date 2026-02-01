using UnityEngine;
using TMPro;

public class EggCountEnding : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI eggText;
    [SerializeField] private int eggCount = 0;

    void Start()
    {
        UpdateEggCount(UpdateEggs.FinalScore);
    }

    public void UpdateEggCount(int count)
    {
        eggCount = count;
        eggText.text = $"COUNGRATS! You ate {eggCount} eggs!";
    }
}