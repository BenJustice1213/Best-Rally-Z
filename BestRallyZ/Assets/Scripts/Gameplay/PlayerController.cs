using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the character moves forward
    public float turnSpeed = 1000f; // Speed at which the character turns (degrees per second)
    public int score = 0;
    public int level = 1;
    public float startingGas = 30f;
    public float currentGas = 0f;
    //public float fuelUpgrade = 5f;
    public Text scoreText;
    public Text gasText;
    public Slider slider;
    [SerializeField] ParticleSystem explode;

    private AudioSource audioSource;
    private int timer;

    private Quaternion targetRotation; // Target rotation for smooth turning
    private Vector3 moveDirection = Vector3.forward; // Current direction the character is moving
    private bool isTurning = false; // Bool to check if the character is currently turning
    private Rigidbody rb;
    private bool canMoveForward = true; // Bool to check if the player can move forward

    public GameObject topDownCam;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        transform.position = new Vector3(-4.6f, 1.55f, 15.12f);
        explode.Stop();
        targetRotation = transform.rotation; // Initialize target rotation
        currentGas = startingGas;
        SetMaxGas(startingGas);

        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        if (score == 6 && level == 1)
        {
            level = 2;
            currentGas = 60f;
            score = 0;
            transform.position = new Vector3(-228, 1.55f, -1.89f);
            topDownCam.transform.position = new Vector3(-238, 515, -5);
            audioSource.Play();
        }
        if (score == 6 && level == 2)
        {
            Debug.Log("Win");
            SceneManager.LoadScene("WinScreen");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        UpdateGasSlider();

        if (currentGas >= 0f)
        {
            currentGas -= 1 * Time.deltaTime;
            gasText.text = "Gas " + currentGas.ToString();
        }
        else if (currentGas <= 0)
        {
            GameOver();
        }

        // Move the character forward continuously in the current move direction if allowed
        if (canMoveForward)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }

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

        // Makes rotation smoother
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void SetTargetRotation(Vector3 newDirection, Quaternion turnAmount)
    {
        moveDirection = newDirection;

        // Sets the target rotation for visual effect
        targetRotation = turnAmount;

 
        StartCoroutine(TurnCharacter());
    }

    IEnumerator TurnCharacter()
    {
        isTurning = true;

        // Wait for a short duration to show the turning animation
        yield return new WaitForSeconds(0.1f);

        // Allows new inputs after the turn animation
        isTurning = false;
    }

    public void updateScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void updateGas(float fuelUpgrade)
    {
        currentGas += fuelUpgrade;
        gasText.text = currentGas.ToString();
    }

    // Functions to control UI gas slider bar
    public void SetMaxGas(float startingGas)
    {
        slider.maxValue = startingGas;
        slider.value = startingGas;
    }

    public void UpdateGasSlider()
    {
        slider.value = currentGas;
    }

    // Handle collisions
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a wall
        if (collision.gameObject.CompareTag("Obstacle"))
        {

            canMoveForward = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the collision is with a wall
        if (collision.gameObject.CompareTag("Obstacle"))
        {

            canMoveForward = true;
        }
    }

    IEnumerator deathAnim(float time)
    {
        explode.Play();
        yield return new WaitForSeconds(time);
    }

    public void GameOver()
    {
        explode.Play();
        moveSpeed = 0;
        Invoke("LoadGameOver", 0.5f);
    }

    public void LoadGameOver()
    {
        SceneManager.LoadScene(sceneName: "GameOver");
    }
}
