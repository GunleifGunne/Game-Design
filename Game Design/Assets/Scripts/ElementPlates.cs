using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPlates : MonoBehaviour
{
    [SerializeField] GameObject elementIcon;
    [SerializeField] Sprite[] elementSprites;
    private SpriteRenderer current;

    AudioSource elementChange;
    BGMusic sounds;

    private void Start()
    {
        current = elementIcon.GetComponent<SpriteRenderer>();
        current.sprite = elementSprites[0];
        current.color = Color.black;

        sounds = GameObject.Find("Sound").GetComponent<BGMusic>();
        elementChange = sounds.elementPlate;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        current.color = Color.white;

        if (other.gameObject.name == "Fire Element")
        {
            current.sprite = elementSprites[2];
            gameObject.tag = other.gameObject.name;
            elementChange.Play();
        }
        else if (other.gameObject.name == "Water Element")
        {
            current.sprite = elementSprites[4];
            gameObject.tag = other.gameObject.name;
            elementChange.Play();
        }
        else if (other.gameObject.name == "Ice Element")
        {
            current.sprite = elementSprites[3];
            gameObject.tag = other.gameObject.name;
            elementChange.Play();
        }
        else if (other.gameObject.name == "Earth Element")
        {
            current.sprite = elementSprites[1];
            gameObject.tag = other.gameObject.name;
            elementChange.Play();
        }
    }
}