using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    public GameObject player1;

    Animator player1Animator;

    // Start is called before the first frame update
    void Start()
    {
        player1Animator = player1.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player1Animator.SetBool("walkLeft", true);
            player1Animator.SetBool("walkRight", false);
            player1Animator.SetBool("walkUp", false);
            player1Animator.SetBool("walkDown", false);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            player1Animator.SetBool("walkRight", true);
            player1Animator.SetBool("walkLeft", false);
            player1Animator.SetBool("walkUp", false);
            player1Animator.SetBool("walkDown", false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            player1Animator.SetBool("walkUp", true);
            player1Animator.SetBool("walkLeft", false);
            player1Animator.SetBool("walkRight", false);
            player1Animator.SetBool("walkDown", false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            player1Animator.SetBool("walkDown", true);
            player1Animator.SetBool("walkLeft", false);
            player1Animator.SetBool("walkRight", false);
            player1Animator.SetBool("walkUp", false);     
        }

        //if(directionX == 0 && directionY == 0)
        //{
        //    player1Animator.SetBool("walkDown", false);
        //    player1Animator.SetBool("walkLeft", false);
        //    player1Animator.SetBool("walkRight", false);
        //    player1Animator.SetBool("walkUp", false);
        //    player1Animator.Play("Idle");
        //}

        //print(avatar.transform.position);
    }
}
