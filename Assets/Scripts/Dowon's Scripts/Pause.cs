using UnityEngine;

public class Pause : MonoBehaviour
{
    public PlayerController control;
    public Hunter hunter;
    public Mask mask;
    public GameObject text;
    public CountdownTimer timer;

    private void Start()
    {
        control = GetComponent<PlayerController>();
    }

    public void OnUseMask()
    {
        // start game
        if (hunter && control && text && timer)
        {
            hunter.enabled = true;
            control.enabled = true;
            text.SetActive(false);
            timer.enabled = true;
        }
    }
}
