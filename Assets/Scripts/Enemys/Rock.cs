using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : AI_Enemy
{
    
    public float patrolRange;
    public int faceDirection = -1; // 1 for right, -1 for left
    private Vector2 StartPos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hitDamage = GetComponent<JumpDamage>();
    }

    private void Start()
    {
        StartPos = transform.position;
    }

    private void Update()
    {
        if (Dead())
        {
            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
            return;
        }
        Patrol();
    }

    protected virtual void Patrol()
    {
        rb.velocity = new Vector2(faceDirection * speed, rb.velocity.y);
        CheckIfShouldFlip();
    }

    public override void Flip()
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
    public override void Running()
    {
        return;
    }
    public override void Attack()
    {
        return;
    }
    protected override void CheckGroundAndFlip()
    {
        return;
    }
}
