using System.Collections;
using UnityEngine;

public class Enemy_Start : AI_Enemy
{
    
    public bool canAttack = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hitDamage = GetComponent<JumpDamage>();
    }

    // Hàm này được gọi từ animation event khi Idle animation kết thúc
    public void OnIdleAnimationEnd()
    {
        canAttack = true;
        CanRun = true;
        float direction = transform.localScale.x > 0 ? 1 : -1;
        speed = direction*10f;
    }
    public void OnSpeedAnimationEnd()
    {
        float direction = transform.localScale.x > 0 ? 1 : -1;
        speed = direction * 2f;
        CanRun = true;
        canAttack = false;
    }

    public override void  Attack()
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

        if (ray2D.collider != null && !canAttack)
        {
            anim.SetBool("Attack", true);
            CanRun = false;
            canAttack = true;
        }

        if (!canAttack)
        {
            anim.SetBool("Attack", false);
        }
    }
    
}