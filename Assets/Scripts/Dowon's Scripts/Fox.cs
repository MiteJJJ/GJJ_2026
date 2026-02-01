using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fox : MonoBehaviour
{
    public static bool Masked = false;

    public GameObject restartButton;
    public TMP_Text currentEggText;
    public TMP_Text totalEggText;

    Mask mask;

    public int totalEggs = 0;
    public int currentEggs = 1;
    public int maxEggs = 3;

    PlayFoxAnimation playFoxAnimation;

    public Collider headCollider;

    void Start()
    {
        Masked = false;
        mask = GetComponent<Mask>();
        UpdateEggsUI();
        playFoxAnimation = GetComponent<PlayFoxAnimation>();
    }

    void OnDestroy()
    {
        Masked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //PickUpEggs;
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Trigger Entered: " + other.gameObject.name);
        //if (other.CompareTag("Egg"))
        //{
        //    if (currentEggs >= maxEggs)
        //    {
        //        return;
        //    }

        //    Debug.Log("Picked up egg");
        //    currentEggs += 1;
        //    Destroy(other.gameObject);
        //}

        if (other.CompareTag("Foxhole"))
        {
            Debug.Log("Reached foxhole");

            // recharge feathers
            mask.RefillFeathers();

            // move current eggs to total
            totalEggs += currentEggs;
            currentEggs = 0;

            UpdateEggsUI();
        }

        if (other.CompareTag("Bullet"))
        {
            Die();
            Destroy(other.gameObject);
        }
    }

    public void IncrementEgg()
    {
        if (currentEggs >= maxEggs)
        {
            return;
        }
        Debug.Log("Picked up Egg");
        currentEggs += 1;
    }

    public void UpdateEggsUI()
    {
        if (!currentEggText || !totalEggText)
        {
            Debug.LogWarning("Set up reference to egg UI");
            return;
        }

        if (currentEggText)
        {
            currentEggText.text = "Current: " + currentEggs;
        }
        if (totalEggText)
        {
            totalEggText.text = "Total Eggs: " + totalEggs;
        }
    }

    public void Die()
    {
        Debug.Log("Fox dies");
        playFoxAnimation.Die();

        GetComponent<PlayerController>().enabled = false;

        if (restartButton)
        {
            restartButton.SetActive(true);
        }
    }

    public void RestartScene()
    {
        if (restartButton)
        {
            restartButton.SetActive(false);
        }

        // Reload the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}