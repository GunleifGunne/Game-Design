using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject[] bigBoyPrefabs;

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

    float xMin, xMax, yMin, yMax, timer;

    int enemyCounter = 1;

    public List <int> el1, el2, el3, el4, el5;

    private void Start()
    {
        SetUpSpawnBoundaries();

    }

    private void Update(){
      timer += Time.deltaTime;
      if(maxDifficulty > determineDifficulty()){
      determineSpawn();
      }
     Debug.Log(maxDifficulty);
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
        int bigBoyIndex = Random.Range(0, bigBoyPrefabs.Length);
        Instantiate(bigBoyPrefabs[bigBoyIndex], GetSpawnPosition(), Quaternion.identity);
    }

    private void SetUpSpawnBoundaries()
    {
        gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private void determineSpawn(){
        if(spawnTime - timer <= 0){
        if(GameObject.Find("Houses").transform.childCount*4 >= maxDifficulty){
        SpawnEnemy();
        timer = 0; 
        spawnTime = Random.Range(0,6 - maxDifficulty/2);
        }
        }
    }
    //Determine current difficulty of game
    private float determineDifficulty(){
        currentDifficulty = 0;

        for(int i = 0; i < el1.Count; i++){
            currentDifficulty++;
        }
        for(int i = 0; i < el2.Count; i++){
            currentDifficulty++;
        }
        for(int i = 0; i < el3.Count; i++){
            currentDifficulty++;
        }
        for(int i = 0; i < el4.Count; i++){
            currentDifficulty++;
        }
        for(int i = 0; i< el5.Count; i++){
            currentDifficulty++;
        }
        return currentDifficulty;
    }

    //Adds spawned enemy to a list with other enemies of its type.
    private void sortElemental (int enemyIndex){
        if(enemyIndex == 0){
            el1.Add(enemyIndex);
        }
        if(enemyIndex == 1){
            el2.Add(enemyIndex);
        }
        if(enemyIndex == 2){
            el3.Add(enemyIndex);
        }
        if(enemyIndex == 3){
            el4.Add(enemyIndex);
        }
        if(enemyIndex == 4){
            el5.Add(enemyIndex);
        }
    }

//As long as there are spaces, increase the max number of enemies by 1. Otherwise increase their MS and fire rate.
    public void increaseDifficulty(){
        if(GameObject.Find("Houses").transform.childCount*4 >= maxDifficulty){ 
        maxDifficulty += difficultyMod;
        }
        if(GameObject.Find("Houses").transform.childCount*4 < maxDifficulty){ 
        actionSpeed += actionSpeedMod;
        }
    }
}
