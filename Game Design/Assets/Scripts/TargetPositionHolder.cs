using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPositionHolder : MonoBehaviour
{
    public List<Vector3> occupiedTargetPositions;

    public void AddToList(Vector3 position)
    {
        occupiedTargetPositions.Add(position);
    }

    public void RemoveFromList(Vector3 position)
    {
        occupiedTargetPositions.Remove(position);
    }
}
