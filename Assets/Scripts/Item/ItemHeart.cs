using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeart : Item
{
   public override void Special()
   {
      if (GameManager.Instance.livePlayer < 3)
      {
         GameManager.Instance.livePlayer += 1;
      }
   }
}
