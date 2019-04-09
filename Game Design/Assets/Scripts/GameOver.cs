using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{

    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }
}
