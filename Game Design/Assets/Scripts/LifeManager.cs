using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LifeManager
{
    private static int life;
    private static bool gameOverBool;

    public static int Counter
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
        }
    }

    public static bool isGameOver
    {
        get
        {
            return gameOverBool;
        }
        set
        {
            gameOverBool = value;
        }
    }

    public static void ResetLife()
    {
        LifeManager.Counter = 2;
        LifeManager.isGameOver = false;
    }

    public static void ReduceLife()
    {
        LifeManager.Counter--;
    }

    public static void GameOver()
    {
        LifeManager.isGameOver = true;
    }
}
