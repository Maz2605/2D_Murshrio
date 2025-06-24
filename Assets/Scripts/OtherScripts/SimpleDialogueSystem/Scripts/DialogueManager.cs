using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;

public class DialogueManager : Singleton<DialogueManager>
{
    [Header("______________Setting UI_____________")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button nextButton;
    [SerializeField] private CanvasGroup dialoguePanel;

    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private Image playerAvatar;
    [SerializeField] private TextMeshProUGUI NPCName;
    [SerializeField] private Image NPCAvatar;

    [Header("______________Setting Animation________")]
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private float textSpeed = 0.05f;
    [SerializeField] private float uiAnimationDuration = 0.3f;
    [SerializeField] private float uiScaleFrom = 0.8f;
    [SerializeField] private Ease uiEaseTypeShow = Ease.OutQuad;
    [SerializeField] private Ease uiEaseTypeClose = Ease.OutQuad;

    [Header("Controll")]
    private bool isShowing;
    public bool IsShowing => isShowing;

    private DialogueState currentState;
    private int currentLineIndex = 0;
    private int currentSentenceIndex = 0;
    private bool isTextRevealing;
    private Coroutine textRevealCoroutine;

    private DialogueLine CurrentLine => currentState?.LinesInState != null && currentLineIndex < currentState.LinesInState.Count ? currentState.LinesInState[currentLineIndex] : null;

    private bool canRunActionStart;
    private Action callbackOnStart;
    private Action callbackOnFinish;

    protected override void Awake()
    {
        base.KeepAlive(false);
        base.Awake();
        dialoguePanel.alpha = 0f;
        dialoguePanel.transform.localScale = Vector3.one * uiScaleFrom;

        currentState = dialogue.GetState(dialogue.InitialStateID);
        currentLineIndex = 0;
        currentSentenceIndex = 0;
    }

    public void SetDialogue(Dialogue dialogue, int currentSentenceIndex)
    {
        this.dialogue = dialogue;
        currentLineIndex = 0;
        this.currentSentenceIndex = currentSentenceIndex;
        currentState = dialogue.GetState(this.dialogue.InitialStateID);
        nextButton.gameObject.SetActive(true);
        canRunActionStart = true;
    }

    public void ShowDialogueUI()
    {
        if (currentState == null || CurrentLine == null || currentSentenceIndex >= CurrentLine.lines.Count)
        {
            StartCoroutine(HideUI());
            return;
        }

        if (canRunActionStart)
        {
            canRunActionStart = false;
            callbackOnStart?.Invoke();
        }

        bool isPlayer = CurrentLine.isPlayer;
        string sentence = CurrentLine.lines[currentSentenceIndex];

        playerName.text = dialogue.Player.Name;
        playerAvatar.sprite = dialogue.Player.Avatar;
        NPCName.text = dialogue.NPC.Name;
        NPCAvatar.sprite = dialogue.NPC.Avatar;

        if (isPlayer)
        {
            playerName.transform.parent.gameObject.SetActive(true);
            playerAvatar.transform.parent.gameObject.SetActive(true);
            NPCName.transform.parent.gameObject.SetActive(false);
            NPCAvatar.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            playerName.transform.parent.gameObject.SetActive(false);
            playerAvatar.transform.parent.gameObject.SetActive(false);
            NPCName.transform.parent.gameObject.SetActive(true);
            NPCAvatar.transform.parent.gameObject.SetActive(true);
        }

        if (textRevealCoroutine != null)
            StopCoroutine(textRevealCoroutine);

        textRevealCoroutine = StartCoroutine(ShowUIAndText(sentence));
    }

    IEnumerator ShowUIAndText(string fullText)
    {
        isShowing = true;
        dialoguePanel.DOKill();
        dialogueText.text = "";

        if (((currentLineIndex > 0 && CurrentLine.isPlayer != currentState?.LinesInState?[currentLineIndex - 1].isPlayer) || currentLineIndex == 0) && currentSentenceIndex == 0)
        {
            dialoguePanel.alpha = 0f;
            dialoguePanel.transform.localScale = Vector3.one * uiScaleFrom;

            dialoguePanel.DOFade(1f, uiAnimationDuration);
            dialoguePanel.transform.DOScale(Vector3.one, uiAnimationDuration).SetEase(uiEaseTypeShow, 1.1f);
            yield return new WaitForSeconds(uiAnimationDuration);
        }

        isTextRevealing = true;

        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTextRevealing = false;
    }

    IEnumerator HideUI()
    {
        dialoguePanel.DOKill();
        isShowing = false;

        dialoguePanel.DOFade(0f, uiAnimationDuration).SetEase(uiEaseTypeClose).OnComplete(() =>
        {
            dialoguePanel.transform.localScale = Vector3.zero;
        });
        yield return new WaitForSeconds(uiAnimationDuration);

        dialogueText.text = "";
        playerName.text = "";
        playerAvatar.sprite = null;
        NPCName.text = "";
        NPCAvatar.sprite = null;
        nextButton.gameObject.SetActive(false);
        callbackOnFinish?.Invoke();
    }

    public void OnNextButtonClicked()
    {
        if (isTextRevealing)
        {
            if (textRevealCoroutine != null)
                StopCoroutine(textRevealCoroutine);

            dialogueText.text = CurrentLine?.lines != null && currentSentenceIndex < CurrentLine.lines.Count ? CurrentLine.lines[currentSentenceIndex] : "";
            isTextRevealing = false;
            return;
        }

        currentSentenceIndex++;

        if (CurrentLine != null && currentSentenceIndex < CurrentLine.lines.Count)
        {
            ShowDialogueUI();
        }
        else
        {
            currentLineIndex++;
            currentSentenceIndex = 0;

            if (currentState != null && currentLineIndex < currentState.LinesInState.Count)
            {
                ShowDialogueUI();
            }
            else
            {
                if (dialogue.GetState(currentState.NextStateID) != null) {
                    currentState = dialogue.GetState(currentState.NextStateID);
                    currentLineIndex = 0;
                    currentSentenceIndex = 0;
                }
                if (currentState != null && currentState.LinesInState.Count > 0)
                {
                    ShowDialogueUI();
                }
                else
                {
                    StartCoroutine(HideUI());
                }
            }
        }
    }

    public void SetActionFinishDialogue(Action callback)
    {
        this.callbackOnFinish = null;
        this.callbackOnFinish = callback;
    }

    public void SetActionShowDialogue(Action callback)
    {
        this.callbackOnStart = null;
        this.callbackOnStart = callback;
    }

    public void AddActionFinishDialogue(Action callback)
    {
        this.callbackOnFinish += callback;
    }

    public void AddActionStartDialogue(Action callback)
    {
        this.callbackOnStart += callback;
    }
}