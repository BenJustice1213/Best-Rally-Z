using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public float fuel = 100f;
    public float fuelConsumptionRate = 1f;

    void Update()
    {
        // Forward
        float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, move, 0);

        // Rotate
        float rotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -rotate);

        // Consume fuel
        fuel -= fuelConsumptionRate * Time.deltaTime;
        if (fuel <= 0)
        {
            // Handle out of fuel scenario
            speed = 0;
            rotationSpeed = 0;
        }
    }
}
