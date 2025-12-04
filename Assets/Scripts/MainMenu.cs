using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("CarroCutscene");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Creditos");
    }
}
