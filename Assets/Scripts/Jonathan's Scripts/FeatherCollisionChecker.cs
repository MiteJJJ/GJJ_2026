using UnityEngine;

public class FeatherCollisionChecker : MonoBehaviour
{
    PlayFoxAnimation playFoxAnimation;
    Collider foxHeadCollider;
    Mask mask;

    void Start()
    {
        Fox fox = FindAnyObjectByType<Fox>();
        if (fox != null)
        {
            playFoxAnimation = fox.GetComponent<PlayFoxAnimation>();
            foxHeadCollider = fox.headCollider;
            mask = fox.GetComponent<Mask>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (mask == null) return;

        if (other == foxHeadCollider)
        {
            playFoxAnimation.StartPicking();
            mask.incrementFeather();
            Destroy(gameObject);
        }
    }
}
