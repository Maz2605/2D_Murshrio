using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected bool checkTrigger = false;
    public virtual void Special(){}
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !checkTrigger)
        {
            Special();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            checkTrigger = true;
        }
    }
}
