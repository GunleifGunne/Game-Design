using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager
{
    private static int score;

    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    public static void Reset()
    {
        ScoreManager.Score = 0;
    }

    public static void AddToScore(int points)
    {
        ScoreManager.Score += points;
    }
}
