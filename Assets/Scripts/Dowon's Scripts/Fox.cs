using UnityEngine;
using UnityEngine.SceneManagement;

public class Fox : MonoBehaviour
{
    public static bool Masked = false;

    public GameObject restartButton;

    public Mask mask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mask = GetComponent<Mask>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Egg"))
        {
            Debug.Log("Picked up egg");
        }

        if (other.CompareTag("Foxhole"))
        {
            Debug.Log("Reached foxhole");

            // recharge feathers
            mask.RefillFeathers();
        }

        if (other.CompareTag("Bullet"))
        {
            Die();
            Destroy(other.gameObject);
        }
    }

    public void Die()
    {
        Debug.Log("Fox dies");

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