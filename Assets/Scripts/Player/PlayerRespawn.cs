using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private CameraFollowPlayer _camFollow;

    private float saveX, saveY;

    private void Awake()
    {
        PlayerPrefs.SetFloat("saveX",transform.position.x);
        PlayerPrefs.SetFloat("saveY",transform.position.y);
        
        PlayerPrefs.SetFloat("maxCamX",0f);
        PlayerPrefs.SetFloat("maxCamY",0f);
        PlayerPrefs.SetFloat("minCamX",0f);
        PlayerPrefs.SetFloat("minCamY",0f);
        _camFollow = Camera.main.GetComponent<CameraFollowPlayer>();

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
        Vector3 min = SaveAndLoadVector3.LoadVector3("MinCam", Vector3.zero);
        Vector3 max = SaveAndLoadVector3.LoadVector3("MaxCam", Vector3.zero);
        _camFollow.minLimits = min;
        _camFollow.maxLimits = max;
    }
    public void PointCheckPoint(float x,float y)
    {
         PlayerPrefs.SetFloat("saveX",x);
         PlayerPrefs.SetFloat("saveY",y);
    }
}
public static class SaveAndLoadVector3
{
    public static void SaveVector3(string key, Vector3 value)
    {
        PlayerPrefs.SetFloat(key + "_x", value.x);
        PlayerPrefs.SetFloat(key + "_y", value.y);
        PlayerPrefs.SetFloat(key + "_z", value.z);
    }

    public static Vector3 LoadVector3(string key, Vector3 defaultValue)
    {
        float x = PlayerPrefs.GetFloat(key + "_x", defaultValue.x);
        float y = PlayerPrefs.GetFloat(key + "_y", defaultValue.y);
        float z = PlayerPrefs.GetFloat(key + "_z", defaultValue.z);
        return new Vector3(x, y, z);
    }
}