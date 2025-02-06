using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoy : MonoBehaviour
{

    private AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }

        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.GameOver();
        }
    }
}
