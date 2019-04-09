using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;


    void Start()
    {
        ScoreManager.Reset();
        scoreText.text = ScoreManager.Score.ToString();
    }

    void Update()
    {
        scoreText.text = ScoreManager.Score.ToString();
    }

    //private void SetUpSingleton()
    //{
    //    int numberOfScoreManagers = FindObjectsOfType<ScoreManager>
    //}
}
