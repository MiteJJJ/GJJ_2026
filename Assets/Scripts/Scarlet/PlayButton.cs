using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadFinalGame);
        bool isMouseDisabled = Cursor.lockState == CursorLockMode.Locked || !Cursor.visible;
        Debug.Log(isMouseDisabled);
    }

    void LoadFinalGame()
    {
        SceneManager.LoadScene("FinalGame");
        Debug.Log("Started");
    }
}