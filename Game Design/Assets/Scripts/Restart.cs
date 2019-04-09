using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    SceneLoader sceneLoader = new SceneLoader();

    void Update()
    {
        sceneLoader.Restart();
    }
}
