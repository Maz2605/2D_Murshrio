using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
   [SerializeField] private CameraFollowPlayer _camFollow;
   public string nameObject = "Player";
   public Transform enemy;

   private void Awake()
   {
         _camFollow = Camera.main.GetComponent<CameraFollowPlayer>();
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag(nameObject) && !GameManager.Instance.isProtect)
      {
         GameManager.Instance.livePlayer--;
         if (GameManager.Instance.livePlayer > 0)
         {
            if (other.gameObject.GetComponent<PlayerRespawn>() != null)
            {
               other.gameObject.GetComponent<PlayerRespawn>().ReturnCheckPoint();
            }
         }
         else
         {
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            other.gameObject.tag = "Enemy";
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up*6f;
         }
      }
   }
}
