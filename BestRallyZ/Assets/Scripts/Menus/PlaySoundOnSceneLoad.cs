using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnSceneLoad : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}

