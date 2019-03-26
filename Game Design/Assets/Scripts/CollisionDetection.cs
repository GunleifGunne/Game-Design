using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] GameObject icon;
    private SpriteRenderer current;
    private Sprite waterIcon, fireIcon, groundIcon, iceIcon, noIcon;

    private void Start()
    {
        current = icon.GetComponent<SpriteRenderer>();
        noIcon = Resources.Load<Sprite>("nothing");
        waterIcon = Resources.Load<Sprite>("water");
        fireIcon = Resources.Load<Sprite>("fire");

        current.sprite = noIcon;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        float y = collision.gameObject.transform.position.y;
        float yObj = collision.transform.position.y;

        if (collision.gameObject.name == "Fire Element")
        {
            if (Input.GetKeyDown("space"))
            {
                print("Now I have FIRE element");

                current.sprite = fireIcon;
            }
        }
        else if (collision.gameObject.name == "Water Element")
        {
            if (Input.GetKeyDown("space"))
            {
                print("Now I have Water element");

                current.sprite = waterIcon;
            }
        }
        else if (collision.gameObject.name == "Ice Element")
        {
            if (Input.GetKeyDown("space"))
            {
                print("Now I have Ice element");
            }
        }
        else if (collision.gameObject.name == "Ground Element")
        {
            if (Input.GetKeyDown("space"))
            {
                print("Now I have Ground element");
            }
        }
    }
}