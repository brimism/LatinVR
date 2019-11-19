using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void EndGame()
    {
        SceneManager.LoadScene("Finish");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
