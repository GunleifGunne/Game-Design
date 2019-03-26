﻿using System.Collections;
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

    EnemySpawner enemySpawner;
    GameObject projectile;

    Vector3 targetPosition;
    
    bool grow = true;
    bool callScaleRoutine = true;

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

            transform.localScale += new Vector3(scaleFactor, scaleFactor);

            if(transform.localScale == new Vector3(scaleFactor * 3, scaleFactor * 3, 1f))
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
}
