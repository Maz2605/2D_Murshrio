using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class ActorDialogueButton : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private bool onImpactOnce;
    [SerializeField] private CanvasGroup uiPressed;
    [SerializeField] private int lineID;//dùng để bắt đầu vị trí cuộc hội thoại ở chỗ khác
    private int countVisit = 0;
    private bool showUI;
    private void Start()
    {
        uiPressed.alpha = 0f;
        DialogueManager.Instance.SetActionFinishDialogue(() => uiPressed.DOFade(1f, 0.3f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        showUI = true;
        uiPressed.DOFade(1f, 0.3f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        showUI = false;
        uiPressed.DOFade(0f, 0.3f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && showUI)
        {
            if (onImpactOnce && countVisit == 0)
            {
                DialogueManager.Instance.SetDialogue(dialogue, 0);
                DialogueManager.Instance.ShowDialogueUI();
                countVisit += 1;
                uiPressed.DOFade(0f, 0.3f);
                return;
            }
            DialogueManager.Instance.SetDialogue(dialogue, lineID);
            DialogueManager.Instance.ShowDialogueUI();
            countVisit += 1;
            uiPressed.DOFade(0f, 0.3f);
        }
    }
}
