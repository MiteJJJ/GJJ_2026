using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fox : MonoBehaviour
{
    public static bool Masked = false;

    public GameObject restartButton;

    Mask mask;
    UpdateEggs updateEggs;

    PlayFoxAnimation playFoxAnimation;

    public Collider headCollider;
    public ParticleSystem depositParticle;
    public AudioManager audioManager;

    void Start()
    {
        Masked = false;
        mask = GetComponent<Mask>();
        updateEggs = GetComponent<UpdateEggs>();
        playFoxAnimation = GetComponent<PlayFoxAnimation>();
        GetComponent<Rigidbody>().isKinematic = false;
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

        if (other.CompareTag("House"))
        {
            Debug.Log("Reached house");

            // only play particle and deposit if carrying eggs
            if (updateEggs.GetCurrentEggs() > 0 && depositParticle != null)
            {
                Vector3 contactPoint = other.ClosestPoint(transform.position);
                depositParticle.transform.position = contactPoint;
                depositParticle.Play();
                audioManager.PlayHome();
            }

            // deposit carried eggs as score
            updateEggs.DepositEggs();
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
        playFoxAnimation.Die();

        GetComponent<PlayerController>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

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