using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableTargets : MonoBehaviour
{
    public List<GameObject> availableTargets;

    // Start is called before the first frame update
    void Awake()
    {
        availableTargets.AddRange(GameObject.FindGameObjectsWithTag("TargetPosition"));
    }

    public void RemoveFromList(GameObject target)
    {
        availableTargets.Remove(target);
    }

    public GameObject GetTargetPosition(int index)
    {
        return availableTargets[index];
    }

    public void AddToList(GameObject target)
    {
        availableTargets.Add(target);
    }
}
