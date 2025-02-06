using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSFX : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayCollectSound()
    {
        audioSource.Play();
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
