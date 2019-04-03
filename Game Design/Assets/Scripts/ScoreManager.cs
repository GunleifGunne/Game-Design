using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
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
