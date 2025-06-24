using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlayer : Item
{
    public float time;
    private GameObject image;
    
    private GameObject obj;
    private float timeUnable;
    private float timeCheck;
    private bool check = false;

    private void Start()
    {
        gameObject.SetActive(true);
        obj = GameObject.Find("Player");
        image = GameObject.Find("Main Camera").transform.GetChild(0).transform.GetChild(3).gameObject;
    }
    
    private void Update()
    {
        if (timeUnable > 0)
        {
            timeCheck += Time.deltaTime;
            if (timeCheck >= 1)
            {
                timeUnable -= 1;
                timeCheck = 0f;
                this.PostEvent(EventID.OnChangePlayer,timeUnable);
                GameManager.Instance.idAnimator = 1;
            }
            
        }

        if (timeUnable <= 0 && check)
        {
            this.PostEvent(EventID.OnChangePlayer,timeUnable);
            image.SetActive(false);
            if (obj.GetComponent<Player_Controller>().isGravityInverted)
            {
                obj.GetComponent<Player_Controller>().transGravity();
                GameManager.Instance.idAnimator = 0;
            }
            check = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            timeUnable = time;
            checkTrigger = false;
            PoolingManager.Instance.Despawn(gameObject);
        }
    }

    public override void Special()
    {
        image.SetActive(true);
        timeUnable = time;
        timeCheck = 0f;
        check = true;
        this.PostEvent(EventID.OnChangePlayer,timeUnable);
    }
}