using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FierveEnemy : AI_Enemy
{
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hitDamage = GetComponent<JumpDamage>();
    }
}
