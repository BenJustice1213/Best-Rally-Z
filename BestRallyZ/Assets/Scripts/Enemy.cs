using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Transform Player;

    void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        // Move towards the player
        Vector3 direction = (Player.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}