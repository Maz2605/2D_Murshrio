using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyrannosaurous : MonoBehaviour
{
    public Rigidbody2D Rb { private set; get; }
    public Animator Anim { private set; get; }
    public Collider2D Coll { private set; get; }

    public int faceDirection = 1;
    public int moveSpeed = 5;
    public float patrolRange = 5f;
    private float destroyAfter = 2f;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask playerMask;
    public float detectRange = 10f;
    protected float chaseDirection = 0f;
    private Vector2 StartPos;
    public enum State
    {
        Patrol,
        Chasing,
        Dead
    }

    protected State CurrentState = State.Patrol;
    protected Transform Target;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();
        StartPos = transform.position;
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void FixedUpdate()
    {
        if (CurrentState == State.Dead) return;
        HandleState();
    }

    private void HandleState()
    {
        switch (CurrentState)
        {
            case State.Patrol:
                Anim.SetBool("Attack", false);
                Anim.SetBool("Patrol", true);
                Patrol();
                break;
            case State.Chasing:
                Chase();
                Anim.SetBool("Patrol", false);
                break;

        }
    }

    private void Flip()
    {
        faceDirection *= -1;
        Rb.transform.Rotate(0f, 180f, 0f);
    }
    

    protected bool IsWallAhead()
    {
        Vector2 wallCheckOrigin = transform.position + Vector3.right * faceDirection * 0.5f;
        RaycastHit2D wallHit = Physics2D.Raycast(wallCheckOrigin, Vector2.right * faceDirection, 0.3f,
            groundMask);
        return wallHit.collider != null;
    }

    protected virtual void Patrol()
    {
        Rb.velocity = Vector2.zero;
    }

    protected virtual void Chase()
    {
        Rb.velocity = faceDirection == 1 ? Vector2.right * moveSpeed : Vector2.left * moveSpeed;
        if (IsWallAhead())
        {
            Target = null;
            CurrentState = State.Patrol;
            Flip();
        }
    }

    protected void DetectPlayer()
    {
        Vector2 origin = transform.position;
        Vector2 direction = faceDirection == -1 ? Vector2.left : Vector2.right;

        RaycastHit2D hit =
            Physics2D.Raycast(origin, direction, detectRange, playerMask);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            chaseDirection = faceDirection;

            if (faceDirection != chaseDirection)
                Flip();

            CurrentState = State.Chasing;
        }
        Debug.DrawRay(origin, direction * detectRange, Color.red);
    }
    public virtual void Dead()
    {
        if (CurrentState == State.Dead) return;

        CurrentState = State.Dead;
        Coll.enabled = false;
        Destroy(gameObject, destroyAfter);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CurrentState == State.Dead) return;

        if (other.gameObject.CompareTag("Player") )
        {
            var dg = other.gameObject.GetComponent<Player_Controller>();
            
        }
    }
}
