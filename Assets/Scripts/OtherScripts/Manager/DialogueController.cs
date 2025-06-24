using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private Player_Controller characterController;
    [SerializeField] private List<Dialogue> dialogues;
    private void OnEnable()
    {
        characterController = FindAnyObjectByType<Player_Controller>();
    }
    private void Start()
    {
        DialogueManager.Instance.SetActionFinishDialogue(() => characterController.CanControl = true);
        DialogueManager.Instance.SetActionShowDialogue(() => characterController.CanControl = false);
        DOVirtual.DelayedCall(1.5f, delegate
        {
            PlayDialougue(0);
        });
    }
    public void PlayDialougue(int id)
    {
        DialogueManager.Instance.SetDialogue(dialogues[id], 0);
        DialogueManager.Instance.ShowDialogueUI();
    }
    public void ShowDiaLouge(int id)
    {
        DialogueManager.Instance.SetDialogue(dialogues[id], 0);
        DialogueManager.Instance.ShowDialogueUI();
    }
}
