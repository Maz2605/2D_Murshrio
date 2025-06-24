using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKey:Item
{
    public override void Special()
    {
        GameManager.Instance.isKey = true;
        Destroy(gameObject);
    }
}
