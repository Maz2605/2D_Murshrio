using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    [SerializeField] private CameraFollowPlayer _camFollow;
    public Vector3 newCamPosMax,newCamPosMin, newPlayerPos;
    private void Start()
    {
        _camFollow = Camera.main.GetComponent<CameraFollowPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _camFollow.minLimits += newCamPosMin;
            _camFollow.maxLimits += newCamPosMax;
            other.transform.position += newPlayerPos;
            other.gameObject.GetComponent<PlayerRespawn>().PointCheckPoint(other.transform.position.x,other.transform.position.y);
        }
    }
}
