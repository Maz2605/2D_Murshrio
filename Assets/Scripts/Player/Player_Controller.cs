using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject particleLeft;
    [SerializeField] private GameObject particleRight;
    private bool doubleJump = false;
    public float forceJump;
    public float speed;
    public bool CanControl { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!CanControl) return;
        if (GameManager.Instance.livePlayer > 0)
        {
            GroundCheck(CheckGround.isGround);
            
            Jump();
            Running();
            SetAnimVelocity();
        }

        if ((Input.GetKeyDown(KeyCode.E) && GameManager.Instance.idAnimator == 1))
        {
            transGravity();
        }
    }
    private void Running()
    {
        float input = 0;
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            input = -1;
        }
        else if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            input = 1;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            input = 0;
        }

        if (input != 0)
        {
            Flip(input);

            if (!CheckGround.isGround)
            {
                particleLeft.SetActive(false);
                particleRight.SetActive(false);
            }
            else
            {
                if (input < 0)
                {
                    particleLeft.SetActive(false);
                    particleRight.SetActive(true);
                }
                else if (input > 0)
                {
                    particleLeft.SetActive(true);
                    particleRight.SetActive(false);
                }
            }
        }

        else
        {
            particleLeft.SetActive(false);
            particleRight.SetActive(false);
        }
    }

    private bool isJump = false;

    public void GroundCheck(bool isGround)
    {
        if (isGround)
        {
            anim.SetBool("Grounded", true);
            anim.SetBool("InAir", false);
        }
        else
        {
            anim.SetBool("Grounded", false);
            anim.SetBool("InAir", true);
        }
    }
    public void SetAnimVelocity()
    {
        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("up") || Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckGround.isGround)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
                rb.velocity = Vector2.up * forceJump * Mathf.Sign(rb.gravityScale);
                isJump = false;
            }
            else if (!CheckGround.isGround && !isJump)
            {
                doubleJump = true;
            }

            if (doubleJump)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
                rb.velocity = Vector2.up * forceJump * Mathf.Sign(rb.gravityScale);
                doubleJump = false;
                isJump = true;
            }
        }

        if (!CheckGround.isGround)
        {
            speed = 2f;
        }
        else
        {
            speed = 5f;
            isJump = false;
        }
    }

    private void Flip(float toward)
    {
        if ((toward > 0 && spriteRenderer.flipX) || (toward < 0 && !spriteRenderer.flipX))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    public bool isGravityInverted = false; // Biến để kiểm tra trạng thái trọng lực

    public void transGravity()
    {
        StartCoroutine(ApplyGravityChange());
    }

    private IEnumerator ApplyGravityChange()
    {
        anim.enabled = false; // Vô hiệu hóa Animator trước khi thay đổi rotation
        yield return new WaitForEndOfFrame(); // Đợi đến cuối frame

        if (!isGravityInverted)
        {
            rb.gravityScale *= -1;
            var scale = transform.localScale;
            scale.y = -5;
            transform.localScale = scale;
        }
        else
        {
            var scale = transform.localScale;
            scale.y = 5;
            transform.localScale = scale;
            rb.gravityScale *= -1;
        }

        isGravityInverted = !isGravityInverted;
        anim.enabled = true; // Kích hoạt lại Animator
    }
}