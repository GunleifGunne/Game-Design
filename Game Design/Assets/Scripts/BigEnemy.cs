using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] int points = 200;

    [Header("Projectile Settings")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float timeBetweenAttacks = 2;
    [SerializeField] float projectileSpeed = 1;

    [SerializeField] GameObject icon1;
    [SerializeField] GameObject icon2;
    [SerializeField] Sprite[] elementSprites1;
    [SerializeField] List<Sprite> elementSprites2;

    float movementThisFrame;
    float spawnToTargetDif;

    int target;

    string isKilledBy1, isKilledBy2;

    bool flipMe = false;
    bool isShooting = false;
    bool hasCollidedWithObj1 = false;
    bool hasCollidedWithObj2 = false;

    GameObject projectile, targetPositionObject, targetHouse;
    ScoreManager scoreManager;
    AvailableTargets availableTargets;

    Vector3 targetPosition, targetHousePos;

    // Start is called before the first frame update
    void Start()
    {
        availableTargets = FindObjectOfType<AvailableTargets>();
        scoreManager = FindObjectOfType<ScoreManager>();

        AssignIcons();
        AssignTarget();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FlipAtTarget();

        if(targetPosition == transform.position && isShooting == false)
        {
            isShooting = true;
            StartCoroutine(ShootParticle(timeBetweenAttacks));
        }

        Remove();
    }

    private void AssignIcons()
    {
        int icon1SpriteIndex = Random.Range(0, 4);
        int icon2SpriteIndex = Random.Range(0, 3);
        icon1.GetComponent<SpriteRenderer>().sprite = elementSprites1[icon1SpriteIndex];
        isKilledBy1 = elementSprites1[icon1SpriteIndex].name;
        elementSprites2.RemoveAt(icon1SpriteIndex);
        icon2.GetComponent<SpriteRenderer>().sprite = elementSprites2[icon2SpriteIndex];
        isKilledBy2 = elementSprites2[icon2SpriteIndex].name;
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
            projectileSpeed *= -1;
            projectilePrefab.transform.Rotate(0f, 180f, 0f);
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

    IEnumerator ShootParticle(float timeBetweenAttacks)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenAttacks);
            projectile = Instantiate(projectilePrefab, transform.position, transform.rotation) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == isKilledBy1)
        {
            hasCollidedWithObj1 = true;
            StartCoroutine(CheckCollision1());
        }
        else if (other.tag == isKilledBy2)
        {
            hasCollidedWithObj2 = true;
            StartCoroutine(CheckCollision2());
        }
            
        if (hasCollidedWithObj1 && hasCollidedWithObj2)
        {
            Destroy(gameObject);
        }

        //if (other.tag == isKilledBy1)
        //{
        //    hasCollidedWithObj1 = true;
        //}
        //else if (other.tag == isKilledBy2)
        //{
        //    hasCollidedWithObj2 = true;
        //}

        //if (hasCollidedWithObj1 && hasCollidedWithObj2)
        //{
        //    Destroy(gameObject);
        //}
    }

    IEnumerator CheckCollision1()
    {
        yield return new WaitForSeconds(1);
        hasCollidedWithObj1 = false;
    }

    IEnumerator CheckCollision2()
    {
        yield return new WaitForSeconds(1);
        hasCollidedWithObj2 = false;
    }

    private void Die()
    {
        availableTargets.AddToList(targetPositionObject);
        scoreManager.AddToScore(points);
        Destroy(gameObject);
    }

    private void Remove()
    {
        if (targetHouse.tag == "Destroyed")
        {
            Destroy(gameObject);
        }
    }
}
