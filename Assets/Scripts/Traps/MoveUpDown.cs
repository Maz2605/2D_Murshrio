using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float maxY, minY;
    public float speed;
    private bool movingUp = true;
    public Transform player;
    private bool playerMove = false;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (movingUp)
        {
            if (playerMove)
            {
                player.Translate(Vector2.up * speed * Time.deltaTime);
            }
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            if (transform.position.y >= maxY)
            {
                movingUp = false;
            }
        }
        else
        {
            if (playerMove)
            {
                player.Translate(Vector2.down * speed * Time.deltaTime);
            }
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            if (transform.position.y <= minY)
            {
                movingUp = true;
            }
        }
    }

    private int check = 0;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            playerMove = true;
            check++;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            check--;
        }
        if (check <= 0)
        {
            check = 0;
            playerMove = false;
        }
    }
}