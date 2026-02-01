using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadFinalGame);
    }

    void LoadFinalGame()
    {
        SceneManager.LoadScene("FinalGame");
    }
}