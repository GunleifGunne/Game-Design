using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float movementSpeed = 0.3f;
    [SerializeField] GameObject icon;
    [SerializeField] int points = 200;
    [SerializeField] GameObject deathVFXPrefab;
    [SerializeField] float durationOfVFX;

    [Header("Scale Settings")]
    [SerializeField] float scaleSizeTime = 5;
    [SerializeField] float scaleFactor = 1;

    [Header("Projectile Settings")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float timeBetweenAttacks = 2;
    [SerializeField] float projectileHorizontalSpeed = 1;
    [SerializeField] float projectileVerticalSpeed = 1;

    float movementThisFrame;
    float spawnToTargetDif;
    float maxSizeX, maxSizeY;

    int target;

    string isKilledBy;

    GameObject projectile, targetPositionObject, targetHouse;
    ScoreUpdate scoreUpdate;
    AvailableTargets availableTargets;

    Vector3 targetPosition, targetHousePos;
    Vector3 originalSize;

    bool grow = false;
    bool callScaleRoutine = true;
    bool flipMe = false;

    AudioSource elementalShoot, elementalDeath;

    // Start is called before the first frame update
    void Start()
    {

        movementSpeed = movementSpeed + GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>().actionSpeed;
        availableTargets = FindObjectOfType<AvailableTargets>();
        scoreUpdate = FindObjectOfType<ScoreUpdate>();

        isKilledBy = icon.GetComponent<SpriteRenderer>().sprite.name;

        AssignTarget();

        originalSize = transform.localScale;
        maxSizeX = originalSize.x + (originalSize.x * scaleFactor * 2);
        maxSizeY = originalSize.y + (originalSize.y * scaleFactor * 2);

        elementalShoot = GameObject.Find("Sound").GetComponent<BGMusic>().elementalShoot;
        elementalDeath = GameObject.Find("Sound").GetComponent<BGMusic>().elementalDeath;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FlipAtTarget();
        ScaleSize();
        Remove();
    }

    private void AssignTarget()
    {
        target = Random.Range(0, availableTargets.availableTargets.Count);
        targetPositionObject = availableTargets.GetTargetPosition(target);
        availableTargets.RemoveFromList(targetPositionObject);
        targetHouse = targetPositionObject.transform.parent.gameObject;

        targetPosition = targetPositionObject.transform.position;
        targetHousePos = targetHouse.transform.position;
        spawnToTargetDif = transform.position.x - targetPosition.x;

         if (spawnToTargetDif > 0)
        {
            transform.Rotate(0f, 180f, 0f);
        }

        if (targetHousePos.x - targetPosition.x < 0)
        {
            projectileVerticalSpeed = 0;
            projectileHorizontalSpeed *= -1;
            projectilePrefab.transform.Rotate(0f, 180f, 0f);
        }

        else if(targetHousePos.x - targetPosition.x > 0){
            projectileVerticalSpeed = 0;
        }

        else if(targetHousePos.y - targetPosition.y < 0)
        {
            projectileHorizontalSpeed = 0;
            projectileVerticalSpeed *= -1;
        }

        else if(targetHousePos.y - targetPosition.y > 0){
            projectileHorizontalSpeed = 0;
        }
    }

    private void FlipAtTarget()
    {
        if (targetPosition == transform.position && flipMe == false)
        {
            if (spawnToTargetDif > 0 && (targetHousePos.x - transform.position.x) > 0)
            {
                transform.Rotate(0f, 180f, 0f);
                flipMe = true;
            }
            else if (spawnToTargetDif < 0 && (targetHousePos.x - transform.position.x) < 0)
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
        if (transform.position == targetPosition && callScaleRoutine)
        {
            callScaleRoutine = false;
            StartCoroutine(ShootParticle(timeBetweenAttacks));
        }
    }

    IEnumerator ShootParticle(float timeBetweenAttacks)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenAttacks);
            projectile = Instantiate(projectilePrefab, transform.position, transform.rotation) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileHorizontalSpeed, projectileVerticalSpeed);
            elementalShoot.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == isKilledBy)
        {
            Die();
        }
    }

    private void Die()
    {
        availableTargets.AddToList(targetPositionObject);
        ScoreManager.AddToScore(points);
        elementalDeath.Play();
        sortElemental();
        GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>().increaseDifficulty();
        Destroy(gameObject);
        GameObject deathVFX = Instantiate(deathVFXPrefab, transform.position, deathVFXPrefab.transform.rotation);
        Destroy(deathVFX, durationOfVFX);
    }

    private void Remove()
    {
        if (targetHouse.tag == "Destroyed")
        {
            sortElemental();
            Destroy(gameObject);
        }
    }

       //When an enemy dies it removes itself from its type's list.
    private void sortElemental(){
        if(this.name == "Earth Enemy(Clone)"){
          GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>().el1.RemoveAt(0);
        }
        if(this.name == "Fire Enemy(Clone)"){
        GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>().el2.RemoveAt(0);
        }
        if(this.name == "Ice Enemy(Clone)"){
        GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>().el3.RemoveAt(0);
        }
        if(this.name == "Water Enemy(Clone)"){
          GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>().el4.RemoveAt(0);
        }
    }
}
