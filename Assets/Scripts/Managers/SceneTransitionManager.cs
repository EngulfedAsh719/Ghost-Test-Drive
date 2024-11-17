using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    //Выход
    public void ExitGame()
    {
        Application.Quit();
    }

    //Начало
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
