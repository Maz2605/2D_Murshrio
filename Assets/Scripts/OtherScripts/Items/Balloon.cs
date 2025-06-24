using UnityEngine;
using DG.Tweening;

public class Balloon : MonoBehaviour
{
    [Header("Floating Settings")]
    [SerializeField] private float floatHeight = 2f;
    [SerializeField] private float floatDuration = 4f;

    [Header("Movement")]
    [SerializeField] private float riseSpeed = 2f;

    [Header("Destruction Settings")]
    [SerializeField] private LayerMask destroyLayer;

    private Tween floatTween;
    private Event1 eventScript;

    private void Start()
    {
        floatTween = transform.DOMoveY(transform.position.y + floatHeight, floatDuration)
                              .SetEase(Ease.InOutSine)
                              .SetLoops(-1, LoopType.Yoyo);

        eventScript = GetComponent<Event1>();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * riseSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & destroyLayer) != 0)
        {
            Pop();
        }
    }

    private void Pop()
    {
        floatTween.Kill();

        transform.DOScale(Vector3.zero, 0.2f)
                 .SetEase(Ease.InBack)
                 .OnComplete(() =>
                 {
                     eventScript?.ShowEvent();

                     gameObject.SetActive(false);
                 });
    }
}
