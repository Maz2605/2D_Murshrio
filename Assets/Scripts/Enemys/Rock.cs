using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float speed;
    public float patrolRange;
    public int faceDirection = -1; // 1 for right, -1 for left
    private Vector2 StartPos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartPos = transform.position;
    }

    private void Update()
    {
        Patrol();
    }

    protected virtual void Patrol()
    {
        rb.velocity = new Vector2(faceDirection * speed, rb.velocity.y);
        CheckIfShouldFlip();
    }

    private void Flip()
    {
        faceDirection *= -1;
        rb.transform.Rotate(0f, 180f, 0f);
    }
    
    protected void CheckIfShouldFlip()
    {
        if ((faceDirection == -1 && transform.position.x <= StartPos.x - patrolRange) ||
            (faceDirection == 1 && transform.position.x >= StartPos.x + patrolRange))
        {
            Flip();
        }

        if (IsWallAhead())
        {
            Flip();
        }
    }

    protected bool IsWallAhead()
    {
        Vector2 wallCheckOrigin = transform.position + Vector3.right * faceDirection * 0.5f;
        RaycastHit2D wallHit = Physics2D.Raycast(wallCheckOrigin, Vector2.right * faceDirection, 1f, LayerMask.GetMask("Ground"));
        return wallHit.collider != null;
    }
}
