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

    bool isAnimation = false;

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

        //Player 2 movement animation
        p2Up = Input.GetKey(KeyCode.Keypad8);
        p2Down = Input.GetKey(KeyCode.Keypad5);
        p2Left = Input.GetKey(KeyCode.Keypad4);
        p2Right = Input.GetKey(KeyCode.Keypad6);

        if (p1Up || p1Down || p1Left || p1Right)
        {
            player1Animation();
            isAnimation = false;
        }
        if (p2Up || p2Down || p2Left || p2Right)
        {
            player2Animation();
            isAnimation = false;
        }
        if (isAnimation == false)
        {
            stopAnimation();
        }
    }

    private bool player1Animation()
    {
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

        return isAnimation = true;
    }

    private bool player2Animation() {
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

        return isAnimation = true;
    }

    private void stopAnimation()
    {
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