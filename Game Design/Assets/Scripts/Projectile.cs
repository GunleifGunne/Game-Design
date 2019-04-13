using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 5;
    [SerializeField] Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * projectileSpeed;

        if(PlayerAnimation.verticalUpwardsFirePoint == true)
        {
            rb.velocity = transform.up * projectileSpeed;
        }
        if(PlayerAnimation.verticalDownwardsFirePoint == true)
        {
            rb.velocity = (transform.up * -1) * projectileSpeed;
        }
    }
}
