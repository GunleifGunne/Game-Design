using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour
{
    public Text text;

    void OnCollisionStay2D(Collision2D collision)
    {
        //float y = collision.gameObject.transform.position.y;
        //float yObj = collision.transform.position.y;

        if(collision.gameObject.name == "Fire Element") {
            if (Input.GetKeyDown("space"))
            {
                print("Now I have FIRE element");
                text.color = new Color(255, 0, 0);
            }
        }
        else if (collision.gameObject.name == "Water Element")
        {
            if (Input.GetKeyDown("space"))
            {
                print("Now I have Water element");
                text.color = new Color(0, 0, 255);
            }
        }
        else if (collision.gameObject.name == "Ice Element")
        {
            if (Input.GetKeyDown("space"))
            {
                print("Now I have Ice element");
                text.color = new Color(255, 255, 0);
            }
        }
        else if (collision.gameObject.name == "Earth Element")
        {
            if (Input.GetKeyDown("space"))
            {
                print("Now I have Ground element");
                text.color = new Color(0, 255, 0);
            }
        }
    }    
}
