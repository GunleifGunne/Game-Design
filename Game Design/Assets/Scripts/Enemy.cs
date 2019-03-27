using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float scaleSizeTime = 5;
    [SerializeField] float timeBetweenAttacks = 2;
    [SerializeField] float projectileSpeed = 1;
    [SerializeField] GameObject projectilePrefab;

    float movementThisFrame;
    float scaleFactor = 1;
    float spawnToTargetDif;

    EnemySpawner enemySpawner;
    GameObject projectile;
    TargetPositionHolder targetPositionHolder;

    Vector3 initialTargetPosition, targetPosition;
    Vector3 originalSize, currentRotation;

    bool grow = true;
    bool callScaleRoutine = true;
    bool flipMe = false;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        targetPositionHolder = FindObjectOfType<TargetPositionHolder>();

        initialTargetPosition = enemySpawner.GetTargetPosition();
        originalSize = transform.localScale;
        spawnToTargetDif = transform.position.x - targetPosition.x;

        FindTargetPosition();

        if (spawnToTargetDif > 0)
        {
            transform.Rotate(0f, 180f, 0f);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FlipAtTarget();
        ScaleSize();
    }

    private void FlipAtTarget()
    {
        if (targetPosition == transform.position && flipMe == false)
        {
            if (spawnToTargetDif > 0 && (enemySpawner.GetHousePosition().x - transform.position.x) > 0)
            {
                transform.Rotate(0f, 180f, 0f);
                flipMe = true;
            }
            else if (spawnToTargetDif < 0 && (enemySpawner.GetHousePosition().x - transform.position.x) < 0)
            {
                transform.Rotate(0f, 180f, 0f);
                flipMe = true;
            }
            else
            {
                flipMe = true;
            }
        }
    }

    private void Move()
    {
        movementThisFrame = movementSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
    }

    private void ScaleSize()
    {
        if(transform.position == targetPosition && callScaleRoutine)
        {
            callScaleRoutine = false;
            StartCoroutine(ScaleSizeRoutine(scaleFactor, scaleSizeTime));
        }
    }

    IEnumerator ScaleSizeRoutine(float scaleFactor, float scaleSizeTime)
    {
        while(grow == true)
        {
            yield return new WaitForSeconds(scaleSizeTime);

            transform.localScale += new Vector3(originalSize.x, originalSize.y);

            if(transform.localScale == new Vector3(originalSize.x * 3, originalSize.y * 3, 1f))
            {
                grow = false;
                StartCoroutine(ShootParticle(timeBetweenAttacks));
            }
        }
    }

    IEnumerator ShootParticle(float timeBetweenAttacks)
    {
        while (true)
        {
            projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0.0f, 0.0f, 90f)) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0f);
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }

    private void FindTargetPosition()
    {
        if(!targetPositionHolder.occupiedTargetPositions.Contains(initialTargetPosition))
        {
            targetPositionHolder.AddToList(initialTargetPosition);
            targetPosition = initialTargetPosition;
        }
        else
        {
            initialTargetPosition = enemySpawner.GetTargetPosition();
            FindTargetPosition();
        }
    }
}
