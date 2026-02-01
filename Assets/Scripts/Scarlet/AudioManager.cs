using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    public AudioSource Hens;
    public AudioSource FeatherSpawn;
    public AudioSource EggSpawn;
    public AudioSource FeatherCollect;
    public AudioSource EggCollect;

    private void Start()
    {
        if (BackgroundMusic != null) BackgroundMusic.Play();
        if (Hens != null) Hens.Play();
    }

    public void PlayFeatherSpawn()
    {
        if (FeatherSpawn != null) FeatherSpawn.Play();
    }

    public void PlayEggSpawn()
    {
        if (EggSpawn != null) EggSpawn.Play();
    }
    public void PlayFeatherCollect()
    {
        if (FeatherCollect != null) FeatherCollect.Play();
    }

    public void PlayEggCollect()
    {
        if (EggCollect != null) EggCollect.Play();
    }
}