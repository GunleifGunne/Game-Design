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
    }

    private void onCollisionEnter2D(Collider2D other){
   //   if(other.name == "Player Projectile Ice(Clone)" || other.name == "Player Projectile Fire(Clone)" || other.name == "Player Projectile Water(Clone)"|| other.name == "Player Projectile Earth(Clone)"){
           Debug.Log("Hello");
     //   }
    }
}
