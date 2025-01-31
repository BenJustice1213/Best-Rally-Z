using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the character moves forward
    public float turnSpeed = 1000f; // Speed at which the character turns (degrees per second)
    public int score = 0;
    public Text scoreText;

    private Quaternion targetRotation; // Target rotation for smooth turning
    private Vector3 moveDirection = Vector3.forward; // Current direction the character is moving
    private bool isTurning = false; // Flag to check if the character is currently turning

    void Start()
    {
        targetRotation = transform.rotation; // Initialize target rotation
    }

    void Update()
    {
        // Move the character forward continuously in the current move direction
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Check for player input to handle turning
        if (!isTurning)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // Turn left (face global left)
                SetTargetRotation(Vector3.left, Quaternion.Euler(0, 270, 0));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Turn right (face global right)
                SetTargetRotation(Vector3.right, Quaternion.Euler(0, 90, 0));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Turn up (face global forward)
                SetTargetRotation(Vector3.forward, Quaternion.Euler(0, 0, 0));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // Turn down (face global backward)
                SetTargetRotation(Vector3.back, Quaternion.Euler(0, 180, 0));
            }
        }

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void SetTargetRotation(Vector3 newDirection, Quaternion turnAmount)
    {
        moveDirection = newDirection;

        // Set the target rotation for visual effect
        targetRotation = turnAmount;

        // Set turning flag to disable new inputs during the turn animation
        StartCoroutine(TurnCharacter());
    }

    IEnumerator TurnCharacter()
    {
        isTurning = true;

        // Wait for a short duration to show the turning animation
        yield return new WaitForSeconds(0.1f); // Adjust this duration to your prefers

        // Enable new inputs after the turn animation
        isTurning = false;
    }

    public void updateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}