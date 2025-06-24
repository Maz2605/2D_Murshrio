using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEnemy : TrunkEnemy
{
    public float arrangeTimeAttack;
    public float arrangeTimeStopAttack;
    private float timeAttack = 0f;
    private float timeStopAttack = 0f;
    public override void Attack()
    {
        if (timeAttack < arrangeTimeAttack)
        {
            anim.SetBool("Attack",true);
            timeAttack += Time.deltaTime;
            timeStopAttack = 0f;
        }
        else
        {
            if (timeStopAttack < arrangeTimeStopAttack)
            {
                anim.SetBool("Attack",false);
                timeStopAttack += Time.deltaTime;
            }
            else
            {
                timeAttack = 0f;
            }
        }
    }
}
