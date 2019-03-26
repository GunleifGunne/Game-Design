using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    Transform bar;
    GameObject building;

	// Use this for initialization
	void Start () {
        building = transform.parent.gameObject;
        SetBorderSize();
	}

    private void Update()
    {
        SetSize(building.GetComponent<Building>().GetHealthPercentage());
    }

    public void SetSize(float sizeNormalized)
    {
        transform.Find("Bar").localScale = new Vector3(sizeNormalized, 1f);
    }

    private void SetBorderSize()
    {
        transform.Find("Border").localScale += new Vector3(0.05f, 0.05f);
    }
}
