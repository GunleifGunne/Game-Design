using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] Transform firePoint, upFirePoint, downFirePoint;
    [SerializeField] string shootCTRL = "P1Fire";
    [SerializeField] GameObject[] projectilePrefabs;
    GameObject projectile;
    private AudioSource heroShoot;

    void Start(){
        heroShoot = GameObject.Find("Sound").GetComponent<BGMusic>().heroShoot;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetButtonDown(shootCTRL))
        {
            if(gameObject.tag == "Fire Element")
            {
                if(PlayerAnimation.verticalUpwardsFirePoint == true)
                {
                    projectile = Instantiate(projectilePrefabs[0], upFirePoint.position, upFirePoint.rotation);
                }
                else if(PlayerAnimation.verticalDownwardsFirePoint == true)
                {
                    projectile = Instantiate(projectilePrefabs[0], downFirePoint.position, downFirePoint.rotation);
                }
                else
                {
                    projectile = Instantiate(projectilePrefabs[0], firePoint.position, firePoint.rotation);
                }
                heroShoot.Play();
            }
            else if(gameObject.tag == "Water Element")
            {
                if (PlayerAnimation.verticalUpwardsFirePoint == true)
                {
                    projectile = Instantiate(projectilePrefabs[1], upFirePoint.position, upFirePoint.rotation);
                }
                else if (PlayerAnimation.verticalDownwardsFirePoint == true)
                {
                    projectile = Instantiate(projectilePrefabs[1], downFirePoint.position, downFirePoint.rotation);
                }
                else
                {
                    projectile = Instantiate(projectilePrefabs[1], firePoint.position, firePoint.rotation);
                }
                heroShoot.Play();
            }
            else if(gameObject.tag == "Ice Element")
            {
                if (PlayerAnimation.verticalUpwardsFirePoint == true)
                {
                    projectile = Instantiate(projectilePrefabs[2], upFirePoint.position, upFirePoint.rotation);
                }
                else if (PlayerAnimation.verticalDownwardsFirePoint == true)
                {
                    projectile = Instantiate(projectilePrefabs[2], downFirePoint.position, downFirePoint.rotation);
                }
                else
                {
                    projectile = Instantiate(projectilePrefabs[2], firePoint.position, firePoint.rotation);
                }
                heroShoot.Play();
            }
            else if(gameObject.tag == "Earth Element")
            {
                if (PlayerAnimation.verticalUpwardsFirePoint == true)
                {
                    projectile = Instantiate(projectilePrefabs[3], upFirePoint.position, upFirePoint.rotation);
                }
                else if (PlayerAnimation.verticalDownwardsFirePoint == true)
                {
                    projectile = Instantiate(projectilePrefabs[3], downFirePoint.position, downFirePoint.rotation);
                }
                else
                {
                    projectile = Instantiate(projectilePrefabs[3], firePoint.position, firePoint.rotation);
                }
                
                heroShoot.Play();
            }

            Destroy(projectile, 0.2f);
        }
    }
}
