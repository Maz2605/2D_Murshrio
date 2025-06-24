using DG.Tweening;
using UnityEngine;

public class Event1 : MonoBehaviour
{
    private Player_Controller player;
    [SerializeField] private NPCFollower NPC;
    [SerializeField] private Dialogue Dialogue;
    private Camera cam;
    [SerializeField] private CameraFollowPlayer followPlayerScript;

    [Header("Camera Settings")]
    public float zoomSize = 3f;
    public float cameraMoveDuration = 1f;

    [Header("Setting Delay")]
    public float timeDelay = 0;

    [Header("Control Options")]
    public bool controlCamera = true;

    private void Start()
    {
        cam = Camera.main;
        player = FindAnyObjectByType<Player_Controller>();
        followPlayerScript = FindAnyObjectByType<CameraFollowPlayer>();
    }

    public void ShowEvent()
    {
        player.CanControl = false;
        DialogueManager.Instance.SetDialogue(Dialogue, 0);

        NPC.gameObject.SetActive(true);
        NPC.enabled = false;

        if (controlCamera)
        {
            followPlayerScript.enabled = false;

            cam.transform.DOMove(new Vector3(NPC.transform.position.x, NPC.transform.position.y, cam.transform.position.z), cameraMoveDuration)
                .SetEase(Ease.InOutSine);

            cam.DOOrthoSize(zoomSize, cameraMoveDuration).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                followPlayerScript.SetTarget(NPC.transform);
                followPlayerScript.enabled = true;
                NPC.enabled = true;
            });
        }
        else
        {
            NPC.enabled = true;
        }

        DOVirtual.DelayedCall(timeDelay, delegate
        {
            DialogueManager.Instance.AddActionFinishDialogue(ActionFinishDialogue);
            DialogueManager.Instance.ShowDialogueUI();
        });
    }

    private void ActionFinishDialogue()
    {
        NPC.gameObject.SetActive(false);
        if (controlCamera)
        {
            followPlayerScript.SetTarget(player.transform);
            cam.DOOrthoSize(5f, cameraMoveDuration).SetEase(Ease.InOutSine);
        }
    }
}
