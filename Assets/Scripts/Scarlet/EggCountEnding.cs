using UnityEngine;
using TMPro;

public class EggCountEnding : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI eggText;
    [SerializeField] private int eggCount = 0;

    void Start()
    {
        //in final version it should get eggcount from "finalgame" scene
        UpdateEggCount(eggCount);
    }

    public void UpdateEggCount(int count)
    {
        eggCount = count;
        eggText.text = $"COUNGRATS! You ate {eggCount} eggs!";
    }
}