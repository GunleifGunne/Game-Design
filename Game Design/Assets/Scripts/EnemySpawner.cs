using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject bigBoyPrefab;

    [Header("Spawn Settings")]
    [SerializeField] int enemiesBeforeBoss = 10;
    [SerializeField] float currentDifficulty = 0;

//DifficultyMod % 0.25f should always = 0.
    [SerializeField] float  difficultyMod = 0.25f;
    [SerializeField] float actionSpeedMod = 0.1f;
    public float actionSpeed = 0.0f;
    public float maxDifficulty = 3.0f;
    [SerializeField] float spawnTime = 2.0f;

    Vector3 spawnPosition;
    Camera gameCamera;
    public float houseCount;

    float xMin, xMax, yMin, yMax, timer;

    int enemyCounter = 1;

    public List <int> el1, el2, el3, el4, el5;

    private void Start()
    {
        SetUpSpawnBoundaries();
        houseCount = GameObject.Find("Houses").transform.childCount;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(maxDifficulty > determineDifficulty())
        {
            determineSpawn();
        }

        Debug.Log("Max Difficulty: " + maxDifficulty);
    }

    public Vector3 GetSpawnPosition()
    {
        int spawnPointEdge;

        spawnPointEdge = Random.Range(0, 4);

        if (spawnPointEdge == 0)
        {
            spawnPosition = new Vector3((float)Random.Range(xMin, xMax), yMax, 0.0f);
        }
        else if (spawnPointEdge == 1)
        {
            spawnPosition = new Vector3((float)Random.Range(xMin, xMax), yMin, 0.0f);
        }
        else if (spawnPointEdge == 2)
        {
            spawnPosition = new Vector3(xMin, (float)Random.Range(yMin, yMax), 0.0f);
        }
        else if (spawnPointEdge == 3)
        {
            spawnPosition = new Vector3(xMax, (float)Random.Range(yMin, yMax), 0.0f);
        }

        return spawnPosition;
    }

    private void SpawnEnemy()
    {
        if(enemyCounter % (enemiesBeforeBoss + 1) != 0)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            sortElemental(enemyIndex);
            Instantiate(enemyPrefabs[enemyIndex], GetSpawnPosition(), Quaternion.identity);
            enemyCounter++;
        }
        else
        {
            SpawnBigBoy();
            sortElemental(4);
            enemyCounter++;
        }
    }

    private void SpawnBigBoy()
    {
        Instantiate(bigBoyPrefab, GetSpawnPosition(), Quaternion.identity);
    }

    private void SetUpSpawnBoundaries()
    {
        gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private void determineSpawn()
    {
        if (spawnTime - timer <= 0)
        {
            if (houseCount * 2 > currentDifficulty)
            {
                SpawnEnemy();
                timer = 0; 
                spawnTime = Random.Range(0, 6 - maxDifficulty / 2);
            }
        }
    }

    //Determine current difficulty of game
    private float determineDifficulty()
    {
        currentDifficulty = 0;

        currentDifficulty += (el1.Count + el2.Count + el3.Count + el4.Count + el5.Count);

        return currentDifficulty;
    }

    //Adds spawned enemy to a list with other enemies of its type.
    private void sortElemental (int enemyIndex)
    {
        if (enemyIndex == 0)
        {
            el1.Add(enemyIndex);
        }

        else if (enemyIndex == 1)
        {
            el2.Add(enemyIndex);
        }

        else if (enemyIndex == 2)
        {
            el3.Add(enemyIndex);
        }

        else if (enemyIndex == 3)
        {
            el4.Add(enemyIndex);
        }

        else if (enemyIndex == 4)
        {
            el5.Add(enemyIndex);
        }
    }

    //As long as there are spaces, increase the max number of enemies by 1. Otherwise increase their MS and fire rate.
    public void increaseDifficulty()
    {
        if (houseCount * 2 >= maxDifficulty)
        { 
            maxDifficulty += difficultyMod;
        }

        if (houseCount * 2 < maxDifficulty)
        { 
            actionSpeed += actionSpeedMod;
        }
    }
}