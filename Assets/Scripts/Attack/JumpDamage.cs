using System;
using UnityEngine;

public class JumpDamage : MonoBehaviour
{
    public Animator anim;
    public float jumpForce;
    public int lifes;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ( other.transform.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up*jumpForce;
            }
            LosseLifeAndHit();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.hitEnemy);
            CheckLife();
        }
    }

    public void LosseLifeAndHit()
    {
        if (anim != null)
        {
            anim.SetTrigger("Hit");       
        }
        lifes--;
    }

    public void CheckLife()
    {
        if (lifes == 0)
        {
            if (anim != null)
            {
                anim.SetBool("Dead", true);   
            }
            gameObject.layer = 1;
            
            foreach (Transform child in transform)
            {
                if(child.name != "Ground") child.gameObject.SetActive(false);
            }
        }
    }
}