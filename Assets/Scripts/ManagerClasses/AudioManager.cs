﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource[] soundEffects;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void playSFX(int soundToPlay)
    {
        soundEffects[soundToPlay].Play();
    }
}
