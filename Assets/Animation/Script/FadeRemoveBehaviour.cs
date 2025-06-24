using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    public float fadeTime = 0f;

    private float timeElapsed = 0f;

    private SpriteRenderer _spriteRenderer;

    private GameObject objToRemove;

    private Color startColor;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        _spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = _spriteRenderer.color;
        objToRemove = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime;
        float newAplha = startColor.a*(1 - (timeElapsed / fadeTime));
        _spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAplha);
        if (timeElapsed > fadeTime)
        {
            Destroy(objToRemove);
        }
    }

   
}
