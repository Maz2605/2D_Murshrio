using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] protected Transform playerTransform; 
    private Vector3 targetPoint, newPoint;
    
    // Limitations for camera movement
    public Vector3 minLimits;         
    public Vector3 maxLimits;
    public float speedMoth;
   
    private void Awake()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.Find("Player").transform;   
        }
    }

    private void LateUpdate()
    {
        Follow();
    }
    
    void Follow()
    {
        if (transform.position != playerTransform.position)
        {
            targetPoint = playerTransform.position;
            Vector3 dir = new Vector3(Mathf.Clamp(targetPoint.x, minLimits.x, maxLimits.x), Mathf.Clamp(targetPoint.y, minLimits.y, maxLimits.y),Mathf.Clamp(targetPoint.z, minLimits.z, maxLimits.z));
            newPoint = Vector3.Lerp(transform.position, dir, speedMoth);
            transform.position = newPoint;
        }
    }
}