using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public LayerMask wallLayer;
    private Vector3 direction;
    private float changeDirectionTime;

    void Start()
    {
        // Set an initial direction
        direction = Vector3.right;
        // Set an initial time to change direction
        changeDirectionTime = Random.Range(2f, 5f);
        // Set initial rotation to face the initial direction
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void Update()
    {
        // Move in the current direction
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Update the timer for changing direction
        changeDirectionTime -= Time.deltaTime;
        if (changeDirectionTime <= 0)
        {
            ChangeDirection();
            changeDirectionTime = Random.Range(2f, 5f); // Reset the timer
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.GameOver();
        }
        else
        {
            // Turn 90 degrees if it collides with anything other than the player
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        for (int i = 0; i < 4; i++)
        {
            // Randomly select a 90-degree turn that is different from the current direction
            Vector3 newDirection = Quaternion.Euler(0, (i + 1) * 90, 0) * direction.normalized;

            // Check if the new direction is clear of walls
            if (!Physics.Raycast(transform.position, newDirection, 1f, wallLayer))
            {
                direction = newDirection;
                transform.rotation = Quaternion.LookRotation(direction); // Face the new direction
                return;
            }
        }
    }
}
