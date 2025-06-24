using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private float saveX, saveY;

    private void Awake()
    {
        PlayerPrefs.SetFloat("saveX",transform.position.x);
        PlayerPrefs.SetFloat("saveY",transform.position.y);
        
        PlayerPrefs.SetFloat("maxCamX",0f);
        PlayerPrefs.SetFloat("maxCamY",0f);
        PlayerPrefs.SetFloat("minCamX",0f);
        PlayerPrefs.SetFloat("minCamY",0f);
    }

    private void Start()
    {
        /*if (PlayerPrefs.GetFloat("saveX") != 0)
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("saveX"), PlayerPrefs.GetFloat("saveY"));
        }*/
    }

    // Start is called before the first frame update
    public void ReturnCheckPoint()
    {
        transform.position = new Vector2(PlayerPrefs.GetFloat("saveX"),PlayerPrefs.GetFloat("saveY"));
    }
    public void PointCheckPoint(float x,float y)
    {
         PlayerPrefs.SetFloat("saveX",x);
         PlayerPrefs.SetFloat("saveY",y);
    }
}
