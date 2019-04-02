using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] string typeOfEnemy;
    [SerializeField] int points = 200;

    [Header("Scale Settings")]
    [SerializeField] float scaleSizeTime = 5;
    [SerializeField] float scaleFactor = 1;

    [Header("Projectile Settings")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float timeBetweenAttacks = 2;
    [SerializeField] float projectileSpeed = 1;

    float movementThisFrame;
    float spawnToTargetDif;

    EnemySpawner enemySpawner;
    GameObject projectile, targetHouse;
    TargetPositionHolder targetPositionHolder;
    ScoreManager scoreManager;

    Vector3 initialTargetPosition, targetPosition, housePosition;
    Vector3 originalSize, currentRotation;

    bool grow = true;
    bool callScaleRoutine = true;
    bool flipMe = false;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        targetPositionHolder = FindObjectOfType<TargetPositionHolder>();
        scoreManager = FindObjectOfType<ScoreManager>();

        initialTargetPosition = enemySpawner.GetTargetPosition();
        housePosition = enemySpawner.GetHousePosition();
        targetHouse = enemySpawner.GetTargetHouse();
        originalSize = transform.localScale;
        spawnToTargetDif = transform.position.x - targetPosition.x;

        Debug.Log(targetHouse.name);

        FindTargetPosition();

        if (spawnToTargetDif > 0)
        {
            transform.Rotate(0f, 180f, 0f);
        }

        if(housePosition.x - targetPosition.x < 0)
        {
            projectileSpeed *= -1;
            projectilePrefab.transform.Rotate(0f, 180f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FlipAtTarget();
        ScaleSize();
        Remove();
    }

    private void FlipAtTarget()
    {
        if (targetPosition == transform.position && flipMe == false)
        {
            if (spawnToTargetDif > 0 && (housePosition.x - transform.position.x) > 0)
            {
                transform.Rotate(0f, 180f, 0f);
                flipMe = true;
            }
            else if (spawnToTargetDif < 0 && (housePosition.x - transform.position.x) < 0)
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

            transform.localScale += new Vector3(originalSize.x * scaleFactor, originalSize.y * scaleFactor);
            points -= 50;

            if(transform.localScale == new Vector3(originalSize.x * scaleFactor * 3, originalSize.y * scaleFactor * 3, 1f))
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
            yield return new WaitForSeconds(timeBetweenAttacks);
            projectile = Instantiate(projectilePrefab, transform.position, transform.rotation) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0f);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(typeOfEnemy == "Earth Enemy" && other.tag == "Ice")
        {
            Die();
        }
        else if (typeOfEnemy == "Fire Enemy" && other.tag == "Water")
        {
            Die();
        }
        else if (typeOfEnemy == "Water Enemy" && other.tag == "Earth")
        {
            Die();
        }
        else if (typeOfEnemy == "Ice Enemy" && other.tag == "Fire")
        {
            Die();
        }
    }

    private void Die()
    {
        targetPositionHolder.RemoveFromList(targetPosition);
        scoreManager.AddToScore(points);
        Destroy(gameObject);
    }

    private void Remove()
    {
        if (targetHouse.tag == "Destroyed")
        {
            targetPositionHolder.RemoveFromList(targetPosition);
            Destroy(gameObject);
        }
    }
}
