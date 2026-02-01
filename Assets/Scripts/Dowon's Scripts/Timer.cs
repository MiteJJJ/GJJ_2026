using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float gameTime = 0;
    public float maxGameTime = 10;

    public GameObject restartButton;

    public GameObject fox;
    public GameObject hunter;

    [Header("UI")]
    public TMP_Text timeDisplay = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameTime = maxGameTime;

        restartButton.SetActive(false);
        fox.SetActive(true);
        hunter.SetActive(true);

        if (!fox || !hunter)
        {
            Debug.LogWarning("Timer is missing fox or hunter reference");
        }

        if (!timeDisplay || !restartButton)
        {
            Debug.LogWarning("Timer is missing restartButton or timeDisplay reference");
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
        if (gameTime < 0)
        {
            gameTime = 0;
            restartButton.SetActive(true);
            hunter.SetActive(false);

            if (!fox || !hunter)
            {
                Debug.LogWarning("Timer is missing fox or hunter reference");
            }

            if (!timeDisplay || !restartButton)
            {
                Debug.LogWarning("Timer is missing restartButton or timeDisplay reference");
            }
        }

        if (timeDisplay != null)
        {
            int minutes = Mathf.FloorToInt(gameTime / 60f);  // 125 / 60 = 2
            int seconds = Mathf.FloorToInt(gameTime % 60f);  // 125 % 60 = 5

            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            timeDisplay.text = formattedTime;
        }
    }
}
