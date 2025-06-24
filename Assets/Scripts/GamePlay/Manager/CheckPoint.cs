using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool check = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
            }
            other.gameObject.GetComponent<PlayerRespawn>().PointCheckPoint(transform.position.x,transform.position.y);
        }
    }
}
