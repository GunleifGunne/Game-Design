using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPlates : MonoBehaviour
{
    [SerializeField] GameObject elementIcon;
    [SerializeField] Sprite[] elementSprites;
    private SpriteRenderer current;

    private void Start()
    {
        current = elementIcon.GetComponent<SpriteRenderer>();
        current.sprite = elementSprites[0];
        //current.color = Color.black;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (Input.GetKeyDown("space"))
        //{
            if (other.gameObject.name == "Fire Element")
            {
                current.sprite = elementSprites[2];
                gameObject.tag = other.gameObject.name;
            }
            else if (other.gameObject.name == "Water Element")
            {
                current.sprite = elementSprites[4];
                gameObject.tag = other.gameObject.name;
            }
            else if (other.gameObject.name == "Ice Element")
            {
                current.sprite = elementSprites[3];
                gameObject.tag = other.gameObject.name;
            }
            else if (other.gameObject.name == "Earth Element")
            {
                current.sprite = elementSprites[1];
                gameObject.tag = other.gameObject.name;
            }
        //}
    }
}