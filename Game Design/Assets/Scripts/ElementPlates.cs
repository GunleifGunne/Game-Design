using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPlates : MonoBehaviour
{
    [SerializeField] GameObject icon;
    private SpriteRenderer current;
    private Sprite waterIcon, fireIcon, earthIcon, iceIcon, noIcon;

    private void Start()
    {
        current = icon.GetComponent<SpriteRenderer>();

        noIcon = Resources.Load<Sprite>("No Element");
        waterIcon = Resources.Load<Sprite>("Water Element");
        fireIcon = Resources.Load<Sprite>("Fire Element");
        earthIcon = Resources.Load<Sprite>("Earth Element");
        iceIcon = Resources.Load<Sprite>("Ice Element");

        current.sprite = noIcon;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown("space"))
        {
            if (other.gameObject.name == "Fire Element")
            {
                current.sprite = fireIcon;
            }
            else if (other.gameObject.name == "Water Element")
            {
                current.sprite = waterIcon;
            }
            else if (other.gameObject.name == "Ice Element")
            {
                current.sprite = iceIcon;
            }
            else if (other.gameObject.name == "Earth Element")
            {
                current.sprite = earthIcon;
            }
        }
    }
}