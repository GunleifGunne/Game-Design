﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;

    [Header("Spawn Settings")]
    [SerializeField] float timeBetweenSpawns = 10;
    [SerializeField] float timeBeforeFirstEnemy = 2;

    Vector3 spawnPosition;
    Camera gameCamera;

    float xMin, xMax, yMin, yMax;

    private void Start()
    {
        SetUpSpawnBoundaries();

        InvokeRepeating("SpawnEnemy", timeBeforeFirstEnemy, timeBetweenSpawns);
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
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyIndex], GetSpawnPosition(), Quaternion.identity);
    }

    private void SetUpSpawnBoundaries()
    {
        gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

}
