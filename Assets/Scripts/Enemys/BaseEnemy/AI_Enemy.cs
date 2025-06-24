using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Enemy : MonoBehaviour,IsEnemy
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected JumpDamage hitDamage;
    protected RaycastHit2D ray2D;
    public float speed;
    public float arrangeAttack;
    private bool flip = false;
    private bool wasOnGround = true;
    public bool CanRun
    {
        get
        {
            return anim.GetBool("CanRun");
        }
        set
        {
            anim.SetBool("CanRun",value);
        }
    }
    public void SetFlip(bool Flip)
    {
        flip = Flip;
    }
    private void Update()
    {
        Running();
        Attack();
    }

    public virtual void Running()
    {
        
        if (CanRun && !Dead())
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            CheckGroundAndFlip();
            Flip();
        }
        else if(!CanRun)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public virtual void Attack()
    {
        Vector2 current = transform.position;
        if (speed > 0)
        {
            ray2D = Physics2D.Raycast(current, Vector2.right, arrangeAttack, LayerMask.GetMask("Player"));
            Debug.DrawRay(current,Vector2.right*arrangeAttack,Color.red);
        }
        else
        {
            ray2D =  Physics2D.Raycast(current, Vector2.left, arrangeAttack, LayerMask.GetMask("Player"));
            Debug.DrawRay(current,Vector2.left*arrangeAttack,Color.red);
        }

        if (ray2D.collider != null)
        {
            anim.SetBool("Attack", true);
            CanRun = false;
        }
        else
        {
            anim.SetBool("Attack", false);
            CanRun = true;
        }
    }

    public virtual bool Dead()
    {
        if (hitDamage.lifes <= 0)
        {
            CanRun = false;
            return true;
        }

        return false;
    }

    public void Flip()
    {
        if (flip)
        {
            float x = transform.localScale.x;
            transform.localScale = new Vector2(-x, transform.localScale.y);
            speed = -speed;
            flip = false;
        }
    }private void CheckGroundAndFlip()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 2f, Color.green);
        bool isOnGround = hit.collider != null;
        if (!isOnGround && wasOnGround)
        {
            flip = true;
        }
        wasOnGround = isOnGround;
    }

}