using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public AudioSource Music;
    // Start is called before the first frame update
    void Start()
    {
        Music.loop = true;
        Music.playOnAwake = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
