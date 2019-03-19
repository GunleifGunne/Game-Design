using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float scaleSizeTime = 5;

    float movementThisFrame;
    float timer;

    EnemySpawner enemySpawner;

    Vector3 targetPosition;

    int scaleFactor = 1;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        targetPosition = enemySpawner.GetTargetPosition();
        //targetPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ScaleSize();
    }

    private void Move()
    {
        movementThisFrame = movementSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
    }

    private void ScaleSize()
    {
        if(transform.position == targetPosition)
        {
            if(transform.localScale != new Vector3(3, 3, 1))
            {
                timer += Time.deltaTime;
                if(timer >= scaleSizeTime)
                {
                    transform.localScale += new Vector3(scaleFactor, scaleFactor, 0);
                    timer = 0f;
                }
            }
        }
    }
}
