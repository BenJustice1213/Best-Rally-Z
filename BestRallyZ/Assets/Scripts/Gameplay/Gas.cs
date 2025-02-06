using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    public GasSFX gasSFX;

    public float addedGas;

    void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            gasSFX.PlayCollectSound();
            controller.updateGas(addedGas);
            Destroy(gameObject);
        }
    }
}
