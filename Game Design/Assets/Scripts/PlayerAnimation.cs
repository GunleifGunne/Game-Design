using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] GameObject player;
    private static bool upwardsFirePointBool;
    private static bool downwardsFirePointBool;
    private Animator p1Animation, p2Animation;
    bool p1Up, p1Down, p1Left, p1Right;
    bool p2Up, p2Down, p2Left, p2Right;

    public static bool verticalUpwardsFirePoint
    {
        get
        {
            return upwardsFirePointBool;
        }
        set
        {
            upwardsFirePointBool = value;
        }
    }

    public static bool verticalDownwardsFirePoint
    {
        get
        {
            return downwardsFirePointBool;
        }
        set
        {
            downwardsFirePointBool = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        p1Animation = player.GetComponent<Animator>();
        p2Animation = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimation.verticalUpwardsFirePoint = false;
        PlayerAnimation.verticalDownwardsFirePoint = false;

        //Player 1 movement animation
        p1Up = Input.GetKey(KeyCode.W);
        p1Down = Input.GetKey(KeyCode.S);
        p1Left = Input.GetKey(KeyCode.A);
        p1Right = Input.GetKey(KeyCode.D);

        if (p1Right && !p1Up && !p1Left && !p1Down)
        {
            p1Animation.Play("Player1Right");
        }
        if (p1Left && !p1Up && !p1Down && !p1Right)
        {
            p1Animation.Play("Player1Right");

            if (Input.GetKeyDown(KeyCode.A) && player.name == "Player One")
            {
                player.GetComponent<PlayerController>().Flip();
            }
        }
        if (p1Up && !p1Right && !p1Left && !p1Down)
        {
            p1Animation.Play("Player1Up");
            PlayerAnimation.verticalUpwardsFirePoint = true;
        }
        if (p1Down && !p1Right && !p1Left && !p1Up)
        {
            p1Animation.Play("Player1Down");
            PlayerAnimation.verticalDownwardsFirePoint = true;
        }

        //Player 2 movement animation
        p2Up = Input.GetKey(KeyCode.UpArrow);
        p2Down = Input.GetKey(KeyCode.DownArrow);
        p2Left = Input.GetKey(KeyCode.LeftArrow);
        p2Right = Input.GetKey(KeyCode.RightArrow);

        if (p2Right && !p2Up && !p2Down && !p2Left)
        {
            p2Animation.Play("Player2Right");
        }
        if (p2Left && !p2Up && !p2Down && !p2Right)
        {
            p2Animation.Play("Player2Right");
            if (Input.GetKeyDown(KeyCode.LeftArrow) && player.name == "Player Two")
            {
                player.GetComponent<PlayerController>().Flip();
            }
        }
        if (p2Up && !p2Right && !p2Left && !p2Down)
        {
            p2Animation.Play("Player2Up");
            PlayerAnimation.verticalUpwardsFirePoint = true;
        }
        if (p2Down && !p2Right && !p2Left && !p2Up)
        {
            p2Animation.Play("Player2Down");
            PlayerAnimation.verticalDownwardsFirePoint = true;
        }

        //Stop player animation when there is no movement
        if (!p1Up && !p1Down && !p1Left && !p1Right)
        {
            p1Animation.Play("Player1Idle");
        }
        if (!p2Up && !p2Down && !p2Left && !p2Right)
        {
            p2Animation.Play("Player2Idle");
        }
    }
}