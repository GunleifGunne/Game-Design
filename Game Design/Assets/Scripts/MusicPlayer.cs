using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    AudioSource backgroundMusic;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();
    }

    void Update()
    {
        ReducePitch();
    }

    public void ReducePitch()
    {
        if(!LifeManager.isGameOver && backgroundMusic.pitch < 0)
        {
            backgroundMusic.pitch = 1;
        }

        if (LifeManager.isGameOver && backgroundMusic.pitch > 0)
        {
            float reduce = .5f;
            backgroundMusic.pitch -= Time.deltaTime * reduce;
        }
    }
}
