using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetUI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float timer;
    [SerializeField] private Vector3 offset;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        transform.DOMove(target.position + offset, timer).OnStart(() => animator.SetBool("Run", true))
            .OnComplete(() => animator.SetBool("Run", false));
    }
}
