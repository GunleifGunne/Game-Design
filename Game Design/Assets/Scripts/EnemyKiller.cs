using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    public List<GameObject> enemyArray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       enemyArray = this.GetComponent<EnemySpawner>().enemies;

       for(int i = 0; i < enemyArray.Count; i++){
               Destroy(enemyArray[i]);
           }
    }
}
