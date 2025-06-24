using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimator : MonoBehaviour
{
   public List<RuntimeAnimatorController> listAnimator;
   public Animator anim;

   private void Awake()
   {
      anim = GetComponent<Animator>();
   }

   private void Start()
   {
      GameManager.Instance.idAnimator = 0;
      anim.runtimeAnimatorController = listAnimator[GameManager.Instance.idAnimator];
   }

   public void Change()
   {
      if (GameManager.Instance.idAnimator < listAnimator.Count - 1)
      {
         GameManager.Instance.idAnimator++;
      }
      else
      {
         GameManager.Instance.idAnimator = 0;
      }
      anim.runtimeAnimatorController = listAnimator[GameManager.Instance.idAnimator];
   }
}
