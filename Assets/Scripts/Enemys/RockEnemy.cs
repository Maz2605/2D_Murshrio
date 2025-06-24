using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEnemy : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject hitPlayer;
    [SerializeField] private List<Vector3> pointMove;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float baseSpeed = 5f;
    
    private Vector3 target;
    private int targetIndex;
    private bool isSpeedBoosted;
    
    private void Start()
    {
        anim = GetComponent<Animator>();

        if (pointMove.Count > 0)
        { 
            StartCoroutine(MoveAlongPath());
        }
    }

    private IEnumerator MoveAlongPath()
    {
        while (true)
        {
            UpdateTarget();
            while (Vector2.Distance(rb.position, target) > 0.001f)
            {
                MoveTowardsTarget();
                yield return null;
            }
            CompleteMovement();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateTarget()
    {
        targetIndex = (targetIndex + 1) % pointMove.Count;
        target = pointMove[targetIndex];
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = ((Vector2)target - rb.position).normalized;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target, baseSpeed * Time.deltaTime);

        if (!isSpeedBoosted && Vector2.Distance(rb.position, target) <= 0.5f)
        {
            PerformHitAnimation(direction);
            baseSpeed = 6f;
            StartCoroutine(BoostSpeed());
        }

        rb.MovePosition(newPosition);
    }

    private void CompleteMovement()
    {
        rb.position = target;
        isSpeedBoosted = false;
    }

    private void PerformHitAnimation(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.5f, layer);
        if (hit.collider != null)
        {
            string directionTrigger = GetDirectionTrigger(direction);
            anim.SetTrigger(directionTrigger);
            StartCoroutine(HitActive());
        }
    }

    private string GetDirectionTrigger(Vector2 direction)
    {
        if (direction == Vector2.left) return "Left";
        if (direction == Vector2.right) return "Right";
        if (direction == Vector2.up) return "Top";
        return "Bottom";
    }

    private IEnumerator HitActive()
    {
        hitPlayer.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        hitPlayer.SetActive(false);
    }

    private IEnumerator BoostSpeed()
    {
        isSpeedBoosted = true;
        yield return new WaitForSeconds(1.5f);
        baseSpeed = 50f;
    }
}
