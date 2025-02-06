using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    private Transform Player;
    private Vector3 direction;
    private float changeDirectionTime;
    private float randomChangeTime;
    public LayerMask wallLayer;

    void Start()
    {
        Player = GameObject.Find("Player").transform;
        // Set an initial direction
        direction = Vector3.right;
        // Set an initial time to change direction
        changeDirectionTime = Random.Range(2f, 5f);
        // Set an initial time for random direction change
        randomChangeTime = Random.Range(2f, 5f);
        // Set initial rotation to face the initial direction
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void Update()
    {
        // Move in the current direction
        transform.Translate(direction * speed * Time.deltaTime);

        // Update the timer for changing direction
        changeDirectionTime -= Time.deltaTime;
        if (changeDirectionTime <= 0)
        {
            ChangeDirection();
            changeDirectionTime = Random.Range(2f, 5f); // Reset the timer
        }

        // Update the timer for random direction change
        randomChangeTime -= Time.deltaTime;
        if (randomChangeTime <= 0)
        {
            RandomlyChangeDirection();
            randomChangeTime = Random.Range(2f, 5f); // Reset the timer
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
            // Randomly select a 90 degree turn that is different from the current direction
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

    private void RandomlyChangeDirection()
    {
        for (int i = 0; i < 4; i++)
        {
            // Randomly select a 90-degree turn
            int randomTurn = Random.Range(0, 4);
            Vector3 newDirection = Quaternion.Euler(0, randomTurn * 90, 0) * Vector3.forward;

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