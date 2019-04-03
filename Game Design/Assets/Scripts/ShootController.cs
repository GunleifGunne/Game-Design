using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] string shootCTRL = "P1Fire";
    [SerializeField] GameObject[] projectilePrefabs;
    [SerializeField] float projectileSpeed = 5;

    GameObject projectile;

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetButtonDown(shootCTRL))
        {
            if(gameObject.tag == "Fire")
            {
                projectile = Instantiate(projectilePrefabs[0], firePoint.position, firePoint.rotation);
            }
            else if(gameObject.tag == "Water")
            {
                projectile = Instantiate(projectilePrefabs[1], firePoint.position, firePoint.rotation);
            }
            else if(gameObject.tag == "Ice")
            {
                projectile = Instantiate(projectilePrefabs[2], firePoint.position, firePoint.rotation);
            }
            else if(gameObject.tag == "Earth")
            {
                projectile = Instantiate(projectilePrefabs[3], firePoint.position, firePoint.rotation);
            }

            Destroy(projectile, 0.2f);
        }
    }
}
