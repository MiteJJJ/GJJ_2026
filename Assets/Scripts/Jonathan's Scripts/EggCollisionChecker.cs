using UnityEngine;

public class EggCollisionChecker : MonoBehaviour
{
    PlayFoxAnimation playFoxAnimation;
    Collider foxHeadCollider;
    Fox fox;

    void Start()
    {
        fox = FindAnyObjectByType<Fox>();
        if (fox != null)
        {
            playFoxAnimation = fox.GetComponent<PlayFoxAnimation>();
            foxHeadCollider = fox.headCollider;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (fox == null) return;

        if (other == foxHeadCollider)
        {
            playFoxAnimation.StartPicking();
            fox.IncrementEgg();
            fox.UpdateEggsUI();
            Destroy(gameObject);
        }
    }
}
