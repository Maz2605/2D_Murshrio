using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField] private ParticleSystem ParticleSystem;
    public NPCFollower npc;
    private Event1 ev1;
  /*  protected override void OnCollisionEnter2D(Collision2D other)
    {
        
    }
    public override void LosseLifeAndHit()
    {
        lifes--;
        if(lifes <= 0)
        {
            gameObject.SetActive(false);
            ParticleSystem.Play();
            npc.enabled = true;
            npc.GetComponent<Event1>().ShowEvent();
            DOVirtual.DelayedCall(0.5f, delegate
            {
                DialogueController.Instance.ShowDiaLouge(1);
            });
        }
        CheckLife();
    }*/

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}
