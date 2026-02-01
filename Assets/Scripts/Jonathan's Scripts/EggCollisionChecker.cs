using UnityEngine;

public class EggCollisionChecker : MonoBehaviour
{
    PlayFoxAnimation playFoxAnimation;
    Collider foxHeadCollider;
    UpdateEggs updateEggs;

    void Start()
    {
        Fox fox = FindAnyObjectByType<Fox>();
        if (fox != null)
        {
            playFoxAnimation = fox.GetComponent<PlayFoxAnimation>();
            foxHeadCollider = fox.headCollider;
            updateEggs = fox.GetComponent<UpdateEggs>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (updateEggs == null) return;

        if (other == foxHeadCollider)
        {
            if (!updateEggs.CanPickUp()) return;

            playFoxAnimation.StartPicking();
            updateEggs.PickUpEgg();
            Destroy(gameObject);
        }
    }
}
