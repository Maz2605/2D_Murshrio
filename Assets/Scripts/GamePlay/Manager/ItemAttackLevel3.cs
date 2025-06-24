using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttackLevel3 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boat"))
        {
            gameObject.GetComponent<Cannon>().SpawnBomb();
            gameObject.SetActive(false);
        }
    }
}
