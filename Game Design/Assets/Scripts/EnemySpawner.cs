using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] List<Transform> houses;
    [SerializeField] float timeBetweenSpawns = 10;
    [SerializeField] float timeBeforeFirstEnemy = 2;

    Vector3 spawnPosition, targetPosition;
    Vector3 enemySpriteOffset, houseSpriteOffset;

    Camera gameCamera;

    Sprite enemySprite, houseSprite;

    float xMin, xMax, yMin, yMax;
    float enemySpriteWidthBounds, houseSpriteWidthBounds;

    private void Start()
    {
        SetUpSpawnBoundaries();

        enemySprite = enemyPrefabs[0].GetComponentInChildren<SpriteRenderer>(true).sprite;
        houseSprite = houses[0].GetComponentInChildren<SpriteRenderer>().sprite;

        enemySpriteWidthBounds = enemySprite.bounds.size.x / 2;
        houseSpriteWidthBounds = houseSprite.bounds.size.x / 2;

        enemySpriteOffset = new Vector3(enemySpriteWidthBounds, 0.0f, 0.0f);
        houseSpriteOffset = new Vector3(houseSpriteWidthBounds, 0.0f, 0.0f);

        InvokeRepeating("SpawnEnemy", timeBeforeFirstEnemy, timeBetweenSpawns);
    }

    public Vector3 GetTargetPosition()
    {
        int houseIndex = Random.Range(0, houses.Count);

        if(spawnPosition.x >= xMin && spawnPosition.x < (xMax / 2))
        {
            targetPosition = houses[houseIndex].transform.position - (enemySpriteOffset + houseSpriteOffset);
        }
        else if (spawnPosition.x >= (xMax / 2) && spawnPosition.x <= xMax)
        {
            targetPosition = houses[houseIndex].transform.position + (enemySpriteOffset + houseSpriteOffset);
        }
        
        return targetPosition;
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
