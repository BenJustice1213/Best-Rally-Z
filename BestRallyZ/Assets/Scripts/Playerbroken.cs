using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 200f;
    public int score = 0;
    public Text scoreText;

    void Update()
    {
        // Forward
        float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, move, 0);

        // Rotate
        float rotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -rotate);


    }
    
    void OnTriggerExit(Collider other)
    {
        score += 1;
        scoreText.text = score.ToString();
    }
}
