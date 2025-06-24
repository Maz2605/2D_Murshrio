using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProtect : Item
{
    private GameObject protectObj;
    public float time;
    public GameObject image;
    private float timeUnable;
    private float timeCheck;
    private bool check = false;

    private void Start()
    {
        gameObject.SetActive(true);
        if (protectObj == null)
        {
            protectObj = GameObject.Find("Player");
        }

        if (image == null)
        {
            image = GameObject.Find("Main Camera").transform.GetChild(0).transform.GetChild(4).gameObject;
        }
    }

    private void Update()
    {
        if (timeUnable > 0 && check)
        {
            timeCheck += Time.deltaTime;
            if (timeCheck >= 1)
            {
                timeUnable -= 1;
                timeCheck = 0;
                this.PostEvent(EventID.OnProtect,timeUnable);
            }
        }

        if (timeUnable <= 0 && check)
        {
            this.PostEvent(EventID.OnProtect,timeUnable);
            if (protectObj != null && protectObj.transform.GetChild(3).gameObject != null)
            {
                protectObj.transform.GetChild(3).gameObject.SetActive(false);
                GameManager.Instance.isProtect = false;
                timeUnable = 0;
                image.SetActive(false);
                check = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                timeUnable = time;
                checkTrigger = false;
                PoolingManager.Instance.Despawn(gameObject);
            }
        }
    }
    
    public override void Special()
    {
        if (protectObj != null)
        {
            protectObj.transform.GetChild(3).gameObject.SetActive(true);   
        }
        image.SetActive(true);
        timeUnable = time;
        this.PostEvent(EventID.OnProtect,timeUnable);
        check = true;
        timeCheck = 0;
        GameManager.Instance.isProtect = true;
    }
}
