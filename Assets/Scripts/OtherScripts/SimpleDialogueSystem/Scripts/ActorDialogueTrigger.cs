using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ActorDialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private bool onImpactOnce;
    [SerializeField] private bool canHide;
    private int countVisit = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (canHide)
        {
            gameObject.SetActive(false);
        }
        if (onImpactOnce && countVisit == 0)
        {
            DialogueManager.Instance.SetDialogue(dialogue, 0);
            DialogueManager.Instance.ShowDialogueUI();
            countVisit += 1;
            return;
        }
        if (onImpactOnce && countVisit == 1) return;
        DialogueManager.Instance.SetDialogue(dialogue, dialogue.States[0].LinesInState.Count - 1);
        DialogueManager.Instance.ShowDialogueUI();
        countVisit += 1;
    }
}
