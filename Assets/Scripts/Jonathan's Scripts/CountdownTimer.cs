using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float countdownTime = 120f; // 2 minutes in seconds
    [SerializeField] private string endingSceneName = "Ending";

    private float timeRemaining;
    private bool timerRunning = true;

    void Start()
    {
        timeRemaining = countdownTime;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                // Time's up!
                timeRemaining = 0;
                timerRunning = false;
                UpdateTimerDisplay();
                LoadEndingScene();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        // Format as MM:SS
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void LoadEndingScene()
    {
        Debug.Log("Time's up! Loading ending scene...");
        SceneManager.LoadScene(endingSceneName);
    }

    // Optional: Public method to add time (for power-ups, etc.)
    public void AddTime(float seconds)
    {
        timeRemaining += seconds;
    }

    // Optional: Public method to pause/resume timer
    public void PauseTimer()
    {
        timerRunning = false;
    }

    public void ResumeTimer()
    {
        timerRunning = true;
    }
}