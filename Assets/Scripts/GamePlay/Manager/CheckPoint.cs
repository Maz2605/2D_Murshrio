using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CameraFollowPlayer _camFollow;
    public Vector3 newCamPosMax, newCamPosMin;

    private bool check = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _camFollow = Camera.main.GetComponent<CameraFollowPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("On",true);
            if (!check)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.checkPoint);
                check = true;
                _camFollow.minLimits = newCamPosMin;
                _camFollow.maxLimits = newCamPosMax;
                other.gameObject.GetComponent<PlayerRespawn>().PointCheckPoint(transform.position.x, transform.position.y);
                SaveAndLoadVector3.SaveVector3("MinCam", _camFollow.minLimits);
                SaveAndLoadVector3.SaveVector3("MaxCam", _camFollow.maxLimits);
            }
        }
    }
}
