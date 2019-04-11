using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public AudioSource BMusic, houseDestroyed, heroShoot, elementalShoot, elementalDeath;
    // Start is called before the first frame update
    void Start()
    {
        BMusic.Play();
        BMusic.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
