using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(2);
    }

    public void Restart()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
