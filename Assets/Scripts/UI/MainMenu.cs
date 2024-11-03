using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SelectLevel(int level)
    {
        SceneManager.LoadScene(level);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
