
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 50f;
    private Rigidbody2D rb;
    //private Animator animotor;
    private Vector2 direction;

    private float jumpTime = 0f;
    private float time = 3f;
    private bool isJumping = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //animotor = GetComponent<Animator>();
    }

    private void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        //animotor.SetFloat("Speed", Mathf.Abs(direction.x));

        if (direction.x < 0)
        {
            rb.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (direction.x > 0)
        {
            rb.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (Input.GetKeyDown(KeyCode.W) && jumpTime <= 0)
        {
            //animotor.SetBool("Jump", true);
            //animotor.SetBool("2Jump", true);
            jumpTime = time;
            isJumping = true;

        }
        if (jumpTime <= 0 && isJumping == true)
        {
            //animotor.SetBool("Jump", false);
            //animotor.SetBool("2Jump", false);
            isJumping = false;
            jumpTime = 1f;
        }
        else
        {
            jumpTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        direction = direction.normalized;
        if (direction.magnitude > 0)
        {
            rb.velocity = direction * moveSpeed * Time.deltaTime;
        }
    }
}
