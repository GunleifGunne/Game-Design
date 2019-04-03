using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private int score = 0;

    void Start()
    {
        scoreText.text = score.ToString();
    }

    void Update()
    {
        scoreText.text = score.ToString();
    }

    //private void SetUpSingleton()
    //{
    //    int numberOfScoreManagers = FindObjectsOfType<ScoreManager>
    //}

    public void AddToScore(int points)
    {
        score += points;
    }
}
